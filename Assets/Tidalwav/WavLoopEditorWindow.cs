using System;
using System.Globalization;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tidalwav.Editor
{
    /// <summary>
    /// Creates WAV loop data readable by Unity.
    /// </summary>
    public class WavLoopEditorWindow : EditorWindow
    {
        [SerializeField] private AudioClip _wavClip;

        private bool _useSeconds;
        private UnityEditor.Editor _wavClipEditor;
        private float _zoomLevel = 1;
        private WavData _wavData;

        private GUIStyle _clipEditorBg;
        private Vector2 _previewScrollPos;
        private int _lastPreviewSample;

        [MenuItem("Window/Audio/WAV Loop Editor")]
        public static void Open()
        {
            GetWindow<WavLoopEditorWindow>().Show();
        }

        private void OnEnable()
        {
            titleContent = new GUIContent("WAV Loop Editor");
        }

        private void OnDisable()
        {
            Cleanup();
        }

        #region GUI

        private void OnGUI()
        {
            DrawLoadGUI();

            if (_wavClip == null || _wavData == null)
            {
                EditorGUILayout.HelpBox("Please load a WAV file.", MessageType.Info);
                return;
            }

            DrawEditGUI();

            EditorGUILayout.Separator();

            DrawSaveGUI();

            DrawPreviewGUI();
        }

        private void DrawLoadGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                var oldClip = _wavClip;
                _wavClip = EditorGUILayout.ObjectField("Wav File", _wavClip, typeof(AudioClip),
                    false) as AudioClip;

                if (oldClip != null && _wavClip != null && oldClip != _wavClip)
                {
                    // Make sure to clean the stream up since we're loading another file
                    Cleanup();
                }

                using (new EditorGUI.DisabledScope(
                    _wavClip == null || !AssetDatabase.GetAssetPath(_wavClip).ToLower().EndsWith(".wav")))
                {
                    if (GUILayout.Button("Load", GUILayout.Width(70)))
                    {
                        try
                        {
                            // ReSharper disable once PossibleNullReferenceException
                            _wavData = new WavData(AssetDatabase.GetAssetPath(_wavClip), _wavClip.samples);
                        }
                        catch (Exception e)
                        {
                            EditorUtility.DisplayDialog("Error", $"Error while loading file: {e.Message}", "OK");
                            Cleanup();
                        }
                        finally
                        {
                            EditorUtility.ClearProgressBar();
                        }
                    }
                }
            }
        }

        private void DrawEditGUI()
        {
            EditorGUILayout.LabelField("Sample Rate", _wavClip.frequency + " Hz");
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("Display time in", GUILayout.Width(145));
                    _useSeconds = GUILayout.Toolbar(_useSeconds ? 1 : 0, new[] {"Samples", "Minutes:Seconds"},
                                      GUILayout.MaxWidth(250)) == 1;
                }

                if (changeCheck.changed)
                {
                    // Avoid that a text field is focused when switching since it wouldn't update 
                    GUI.FocusControl(null);
                }
            }

            EditorGUILayout.Separator();

            if (_wavData.Loops.Count > 1)
            {
                EditorGUILayout.HelpBox(
                    "The loaded WAV file contains multiple loops. Unity seems to ignore every loop except the first one. " +
                    "Reverse or ping-pong loops are also treated like forward loops.",
                    MessageType.Warning);
            }

            if (_wavData.Loops.Count > 0)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("Start", GUILayout.MaxWidth(180));
                    GUILayout.Label("End", GUILayout.MaxWidth(180));
                    GUILayout.Label(" ", GUILayout.Width(70));
                }
            }
            else
            {
                GUILayout.Label("This file does not contain any custom loops, so it will loop from start to finish.");
            }

            Loop loopToRemove = null;
            foreach (var loop in _wavData.Loops)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (_useSeconds)
                    {
                        float startSeconds = SamplesToSeconds(loop.StartSample);
                        float endSeconds = SamplesToSeconds(loop.EndSample);

                        string startTime = SecondsToDurationString(startSeconds);
                        string endTime = SecondsToDurationString(endSeconds);

                        startTime = EditorGUILayout.TextField(startTime, GUILayout.MaxWidth(180));
                        endTime = EditorGUILayout.TextField(endTime, GUILayout.MaxWidth(180));

                        startSeconds = DurationStringToSeconds(startTime, startSeconds);
                        endSeconds = DurationStringToSeconds(endTime, endSeconds);

                        loop.StartSample = SecondsToSamples(startSeconds);
                        loop.EndSample = SecondsToSamples(endSeconds);
                    }
                    else
                    {
                        loop.StartSample = EditorGUILayout.LongField(loop.StartSample, GUILayout.MaxWidth(180));
                        loop.EndSample = EditorGUILayout.LongField(loop.EndSample, GUILayout.MaxWidth(180));
                    }

                    loop.StartSample = Math.Min(_wavClip.samples, loop.StartSample);
                    loop.EndSample = Math.Min(_wavClip.samples, loop.EndSample);

                    if (GUILayout.Button("Remove Loop", GUILayout.Width(110)))
                    {
                        loopToRemove = loop;
                    }
                }
            }

            if (loopToRemove != null)
            {
                _wavData.RemoveLoop(loopToRemove);
            }

            if (_wavData.Loops.Count <= 0 && GUILayout.Button("Add Custom Loop", GUILayout.Width(140)))
            {
                _wavData.AddLoop(new Loop
                {
                    StartSample = 0,
                    EndSample = _wavClip.samples
                });
            }
        }

        private void DrawSaveGUI()
        {
            if (GUILayout.Button("Save"))
            {
                try
                {
                    _wavData.Save();
                }
                catch (Exception e)
                {
                    EditorUtility.DisplayDialog("Error", $"Error while saving file: {e.Message}", "OK");
                    Cleanup();
                }
                finally
                {
                    EditorUtility.ClearProgressBar();
                }
            }

            EditorGUILayout.HelpBox(
                "The original file will be overwritten. Saving is required to update the preview below.",
                MessageType.Warning);
        }

        private void DrawPreviewGUI()
        {
            GUILayout.FlexibleSpace();

            if (_wavClipEditor == null)
            {
                _wavClipEditor = UnityEditor.Editor.CreateEditor(_wavClip);
            }

            if (_clipEditorBg == null)
            {
                _clipEditorBg = new GUIStyle();
                _clipEditorBg.normal.background = Texture2D.blackTexture;
            }

            int currentSample = GetSamplePosition();
            if (currentSample > 0)
            {
                // Playback position gets reset to zero after pausing. Save the last one to make
                // finding the desired loops easier.
                _lastPreviewSample = currentSample;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Preview");
                using (new EditorGUI.DisabledScope(_zoomLevel / 1.5f < 1.0f))
                {
                    if (GUILayout.Button("-"))
                    {
                        _zoomLevel /= 1.5f;
                    }
                }

                using (new EditorGUI.DisabledScope(_zoomLevel * 1.5f > 20.0f))
                {
                    if (GUILayout.Button("+"))
                    {
                        _zoomLevel *= 1.5f;
                    }
                }

                GUILayout.FlexibleSpace();
                _wavClipEditor.OnPreviewSettings();
            }

            using (var scrollView = new EditorGUILayout.ScrollViewScope(_previewScrollPos))
            {
                _previewScrollPos = scrollView.scrollPosition;
                _wavClipEditor.OnPreviewGUI(
                    GUILayoutUtility.GetRect(position.width * _zoomLevel, 100, GUILayout.ExpandWidth(false)),
                    _clipEditorBg);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Set Loop Point to Current Preview Position: ", GUILayout.ExpandWidth(false));
                GUILayout.Label(_useSeconds 
                    ? SecondsToDurationString(SamplesToSeconds(_lastPreviewSample))
                    : _lastPreviewSample.ToString(),
                    GUILayout.Width(80));
                
                if (GUILayout.Button("Start", GUILayout.Width(80)))
                {
                    _wavData.GetOrAddFirstLoop().StartSample = _lastPreviewSample;
                }

                if (GUILayout.Button("End", GUILayout.Width(80)))
                {
                    _wavData.GetOrAddFirstLoop().EndSample = _lastPreviewSample;
                }
            }

            // Enable looping by default
            _wavClipEditor.GetType()
                .GetField("s_Loop", BindingFlags.Static | BindingFlags.NonPublic)?
                .SetValue(null, true);
        }

        #endregion

        #region Helpers

        private void Cleanup()
        {
            _wavData = null;
            if (_wavClipEditor != null)
            {
                DestroyImmediate(_wavClipEditor);
            }
        }

        private int GetSamplePosition()
        {
            var audioUtil = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.AudioUtil");
            // ReSharper disable once PossibleNullReferenceException
            return (int) audioUtil?.GetMethod("GetClipSamplePosition")?
                .Invoke(null, new object[] {_wavClip});
        }

        private long SecondsToSamples(float seconds)
        {
            return (long) Mathf.Round(seconds * _wavClip.frequency);
        }

        private float SamplesToSeconds(long samples)
        {
            return (float) (samples / (double) _wavClip.frequency);
        }

        private string SecondsToDurationString(float inputSeconds)
        {
            var timeSpan = TimeSpan.FromSeconds(inputSeconds);

            return $"{Math.Floor(timeSpan.TotalMinutes).ToString(CultureInfo.CurrentCulture).PadLeft(2, '0')}" +
                   $":{timeSpan.Seconds.ToString().PadLeft(2, '0')}" +
                   $".{timeSpan.Milliseconds.ToString().PadLeft(3, '0')}";
        }

        private float DurationStringToSeconds(string time, float previousValue)
        {
            if (time.Contains(":"))
            {
                string[] minSecSplit = time.Split(':');
                if (minSecSplit.Length > 2)
                {
                    // Only support mm:ss.ms strings and bail out if another format is used
                    return previousValue;
                }

                // Parse as int to avoid weird minute fractions
                if (!int.TryParse(minSecSplit[0], out int minutes))
                    return previousValue;

                // Parse as float to support milliseconds
                if (!float.TryParse(minSecSplit[1], out float seconds))
                    return previousValue;

                return minutes * 60 + seconds;
            }
            else
            {
                return float.TryParse(time, out float seconds) ? seconds : previousValue;
            }
        }

        #endregion
    }
}