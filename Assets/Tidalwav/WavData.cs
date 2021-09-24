using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tidalwav.Editor
{
    /// <summary>
    /// Allows loop editing on raw WAV data.
    /// See https://sites.google.com/site/musicgapi/technical-documents/wav-file-format
    /// </summary>
    internal class WavData
    {
        private byte[] _rawData;
        private List<Loop> _loops;
        
        public string Path { get; }
        public int SampleCount { get; }
        
        public IReadOnlyList<Loop> Loops => _loops;

        internal WavData(string path, int sampleCount)
        {
            Path = path;
            SampleCount = sampleCount;
            Load();
        }
        
        #region IO

        internal void Save()
        {
            EditorUtility.DisplayProgressBar("WAV Loop Editor", "Writing loops...", 0.0f);
            int afterHeaderOffset = FindFmtChunkEnd();
            if (afterHeaderOffset < 0)
            {
                throw new Exception("Could not find fmt chunk.");
            }
            
            byte[] customSmpl = CreateLoopSmpl();

            // Inject custom smpl data after the header
            var writeStream = new MemoryStream();
            writeStream.Write(_rawData, 0, afterHeaderOffset);
            writeStream.Write(customSmpl, 0, customSmpl.Length);
            writeStream.Write(_rawData, afterHeaderOffset, _rawData.Length - afterHeaderOffset);

            EditorUtility.DisplayProgressBar("WAV Loop Editor", "Writing metadata...", 0.4f);
            var writer = new BinaryWriter(writeStream);
            writer.Seek(4, SeekOrigin.Begin); // Contains file size
            writer.Write((uint) writer.BaseStream.Length - 8); // Write the new file size minus the RIFF header

            EditorUtility.DisplayProgressBar("WAV Loop Editor", "Writing to file...", 0.6f);
            File.WriteAllBytes(Path, writeStream.GetBuffer());

            EditorUtility.DisplayProgressBar("WAV Loop Editor", "Importing...", 0.8f);
            AssetDatabase.ImportAsset(Path, ImportAssetOptions.ForceUpdate);
        }
        
        private void Load()
        {
            var bytes = File.ReadAllBytes(Path);
            var wavStream = new MemoryStream(bytes, 0, bytes.Length, true, true);
            try
            {
                var reader = new BinaryReader(wavStream);

                EditorUtility.DisplayProgressBar("WAV Loop Editor", "Reading loops....", 0.5f);
                _loops = ReadLoops(reader, out long smplStart, out long smplEnd);

                EditorUtility.DisplayProgressBar("WAV Loop Editor", "Removing smpl....", 0.9f);
                RemoveSmplBlock(smplStart, smplEnd, wavStream, bytes);
            }
            finally
            {
                wavStream.Dispose();
            }
        }

        /// <summary>
        /// Removes the "smpl" block from the WAV data after loading it to add a custom one later.
        /// </summary>
        private void RemoveSmplBlock(long smplStart, long smplEnd, MemoryStream wavStream, byte[] bytes)
        {
            if (smplStart > -1 && smplEnd > 1)
            {
                long smplLength = smplEnd - smplStart;

                wavStream.Dispose();

                // Create a copy and skip the smpl block
                byte[] newBytes = new byte[bytes.LongLength - smplLength];
                Array.Copy(bytes, 0, newBytes, 0, smplStart);
                Array.Copy(bytes, smplEnd, newBytes, smplStart, bytes.Length - smplEnd);

                bytes = newBytes;
            }

            _rawData = bytes;
        }

        private List<Loop> ReadLoops(BinaryReader reader, out long smplStart, out long smplEnd)
        {
            var buffer = new byte[4];
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            while (reader.BaseStream.Position < reader.BaseStream.Length - 4)
            {
                reader.BaseStream.Read(buffer, 0, 4);
                if (Encoding.ASCII.GetString(buffer) == "smpl") // smpl chunk contains looping information
                {
                    var loops = new List<Loop>();

                    smplStart = reader.BaseStream.Position - 4;

                    long length = reader.ReadUInt32();
                    reader.ReadUInt32(); // Manufacturer
                    reader.ReadUInt32(); // Product
                    reader.ReadUInt32(); // Sample Period
                    reader.ReadUInt32(); // MIDI Unity Node
                    reader.ReadUInt32(); // MIDI Pitch Fraction
                    reader.ReadUInt32(); // SMPTE Format
                    reader.ReadUInt32(); // SMPTE Offset

                    long numberOfLoops = reader.ReadUInt32();
                    reader.ReadUInt32(); // Sampler Data

                    smplEnd = smplStart + length + 8;

                    for (int i = 0; i < numberOfLoops; i++)
                    {
                        // TODO: handle cue points correctly
                        reader.ReadUInt32(); // Cue point ID
                        reader.ReadUInt32(); // Type. Unity seems to treat all loops as forward loops, so ignore it
                        long start = reader.ReadUInt32();
                        long end = reader.ReadUInt32();
                        reader.ReadUInt32(); // Fraction
                        reader.ReadUInt32(); // Play Count. Seems to be ignored by Unity.

                        loops.Add(new Loop
                        {
                            StartSample = start,
                            EndSample = end
                        });
                    }

                    return loops;
                }
                else
                {
                    // Check smpl chunk for every byte instead of every 4 bytes.
                    reader.BaseStream.Seek(-3, SeekOrigin.Current);
                }
            }

            smplStart = -1;
            smplEnd = -1;
            return new List<Loop>();
        }
        
        private byte[] CreateLoopSmpl()
        {
            using (var stream = new MemoryStream())
            {
                byte[] chunkId = Encoding.ASCII.GetBytes("smpl");

                var writer = new BinaryWriter(stream);
                writer.Write(chunkId);
                writer.Write((uint) (36 + _loops.Count * 24)); // Chunk data sie
                writer.Write(0); // Manufacturer
                writer.Write(0); // Product
                writer.Write(0); // Sample Period
                writer.Write((uint) 60); // Midi Unity Note. 60 is C (shouldn't matter?)
                writer.Write(0); // Midi Pitch Fraction
                writer.Write(0); // SMPTE Format
                writer.Write(0); // SMPTE Offset
                writer.Write((uint) _loops.Count); // Num Sample Loops
                writer.Write(0); // Sampler Data

                foreach (var loop in _loops)
                {
                    writer.Write(
                        (uint) 1); // Cue point ID. It seems like there's always one at the beginning of the file.
                    writer.Write((uint) 1);
                    writer.Write((uint) loop.StartSample);
                    writer.Write((uint) loop.EndSample);
                    writer.Write(0); // Fraction
                    writer.Write(0); // Play Count
                }

                return stream.ToArray();
            }
        }

        private int FindFmtChunkEnd()
        {
            using (var stream = new MemoryStream(_rawData))
            {
                var reader = new BinaryReader(stream);
                var buffer = new byte[4];
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (reader.BaseStream.Position < reader.BaseStream.Length - 4)
                {
                    reader.Read(buffer, 0, 4);
                    if (Encoding.ASCII.GetString(buffer) == "fmt ") // smpl chunk contains looping information
                    {
                        long chunkDataSize = reader.ReadUInt32();
                        return (int) (chunkDataSize + reader.BaseStream.Position);
                    }
                }
            }

            return -1;
        }
        
        #endregion
        
        #region Utility

        internal void RemoveLoop(Loop loop)
        {
            _loops.Remove(loop);
        }

        internal void AddLoop(Loop loop)
        {
            _loops.Add(loop);
        }

        internal Loop GetOrAddFirstLoop()
        {
            if (_loops.Count < 1)
            {
                _loops.Add(new Loop
                {
                    StartSample = 0,
                    EndSample = SampleCount
                });
            }

            return _loops[0];
        }

        #endregion
    }
}