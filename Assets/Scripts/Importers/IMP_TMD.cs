using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_TMD
{
    public static Mesh LoadAsset(string assetPath, Material[] collection, MeshRenderer renderer)
    {
        Mesh mesh = new Mesh();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int vertElements = reader.ReadInt32();
            int vertOffset = reader.ReadInt32();
            int normalElements = reader.ReadInt32();
            int normalOffset = reader.ReadInt32();
            int faceElements = reader.ReadInt32();
            int faceOffset = reader.ReadInt32();
            short scale = reader.ReadInt16();
            short unknown1 = reader.ReadInt16();

            List<Vector3> verticesList = new List<Vector3>();
            List<Vector3> normalsList = new List<Vector3>();
            List<Vector2> uvsList = new List<Vector2>();
            List<Color32> colorsList = new List<Color32>();
            List<List<int>> indicesList = new List<List<int>>();
            List<int> materialIDs = new List<int>();

            indicesList.Add(new List<int>());
            materialIDs.Add(0);

            for (int i = 0, j = 0; i < faceElements; i++)
            {
                byte R8 = reader.ReadByte();
                byte G8 = reader.ReadByte();
                byte B8 = reader.ReadByte();
                byte A = 0;
                int header = reader.ReadByte();

                if (R8 == 255 && G8 == 255 && B8 == 255)
                    A = 255;

                if (R8 == 128 && G8 == 128 && B8 == 128)
                    A = 255;

                if (header == 4)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormalsByIndex(reader, 3, normalOffset, reader.ReadInt16()));
                    uvsList.AddRange(GetEmptyUVs(3));
                    indicesList[0].Add(j * 3 + 0);
                    indicesList[0].Add(j * 3 + 1);
                    indicesList[0].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));

                    //reader.BaseStream.Seek(8, SeekOrigin.Current);
                }
                else if (header == 5)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetEmptyNormals(3));
                    reader.ReadInt16(); //unknown
                    uvsList.AddRange(GetUVs(reader, 3, collection));
                    int submashID = reader.ReadInt16() + 1;

                    if (!materialIDs.Contains(submashID))
                    {
                        materialIDs.Add(submashID);
                        indicesList.Add(new List<int>());
                        mesh.subMeshCount++;
                    }

                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 0);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 1);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                }
                else if (header == 6)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetEmptyNormals(3));
                    uvsList.AddRange(GetEmptyUVs(3));
                    reader.ReadInt16(); //always 0x3F00?
                    indicesList[0].Add(j * 3 + 0);
                    indicesList[0].Add(j * 3 + 1);
                    indicesList[0].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));

                    //reader.BaseStream.Seek(8, SeekOrigin.Current);
                }
                else if (header == 8)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormals(reader, 3, normalOffset));
                    uvsList.AddRange(GetEmptyUVs(3));
                    indicesList[0].Add(j * 3 + 0);
                    indicesList[0].Add(j * 3 + 1);
                    indicesList[0].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));

                    //reader.BaseStream.Seek(12, SeekOrigin.Current);
                }
                else if (header == 9)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormals(reader, 3, normalOffset));
                    uvsList.AddRange(GetUVs(reader, 3, collection));
                    int submashID = reader.ReadInt16() + 1;

                    if (!materialIDs.Contains(submashID))
                    {
                        materialIDs.Add(submashID);
                        indicesList.Add(new List<int>());
                        mesh.subMeshCount++;
                    }
                    
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 0);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 1);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                }
                else if (header == 11)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormals(reader, 3, normalOffset));
                    uvsList.AddRange(GetUVsByIndex(reader, 3, collection, 3));
                    int submashID = 3 + 1;

                    if (!materialIDs.Contains(submashID))
                    {
                        materialIDs.Add(submashID);
                        indicesList.Add(new List<int>());
                        mesh.subMeshCount++;
                    }

                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 0);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 1);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                }
                else if (header == 12)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormals(reader, 3, normalOffset));
                    uvsList.AddRange(GetEmptyUVs(3));
                    reader.ReadInt16(); //unknown
                    reader.ReadInt16(); //always 0x8080 (changes in game)
                    reader.ReadInt32(); //always 0?
                    indicesList[0].Add(j * 3 + 0);
                    indicesList[0].Add(j * 3 + 1);
                    indicesList[0].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    
                    //reader.BaseStream.Seek(20, SeekOrigin.Current);
                }
                else if (header == 28)
                {
                    /*verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetNormals(reader, 3, normalOffset));
                    uvsList.AddRange(GetEmptyUVs(3));
                    reader.ReadInt16(); //unknown
                    reader.ReadInt16(); //always 0x8080 (changes in game)
                    reader.ReadInt32(); //always 0?
                    indicesList[0].Add(j * 3 + 0);
                    indicesList[0].Add(j * 3 + 1);
                    indicesList[0].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));*/

                    reader.BaseStream.Seek(20, SeekOrigin.Current);
                }
                else if (header == 31)
                {
                    verticesList.AddRange(GetVertices(reader, 3, vertOffset));
                    normalsList.AddRange(GetEmptyNormals(3));
                    reader.ReadInt16(); //unknown
                    uvsList.AddRange(GetUVs(reader, 3, collection));
                    int submashID = reader.ReadByte() + 1;
                    reader.ReadByte(); //always 0x40?

                    if (!materialIDs.Contains(submashID))
                    {
                        materialIDs.Add(submashID);
                        indicesList.Add(new List<int>());
                        mesh.subMeshCount++;
                    }

                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 0);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 1);
                    indicesList[materialIDs.IndexOf(submashID)].Add(j * 3 + 2);
                    j++;

                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));
                    colorsList.Add(new Color32(R8, G8, B8, A));

                    //reader.BaseStream.Seek(20, SeekOrigin.Current);
                }
                else
                {
                    Debug.LogError("Unkown header detected! Stream Position: " + reader.BaseStream.Position);
                    return null;
                }
            }

            mesh.SetVertices(verticesList);
            mesh.SetColors(colorsList);
            mesh.SetNormals(normalsList);
            mesh.SetUVs(0, uvsList);

            for (int i = 0; i < indicesList.Count; i++)
                 mesh.SetTriangles(indicesList[i], i);

            Material[] materials = new Material[materialIDs.Count];

            for (int i = 0; i < materialIDs.Count; i++)
                materials[i] = collection[materialIDs[i]];

            renderer.sharedMaterials = materials;

            Debug.Log("Generated polys: " + mesh.triangles.Length);
        }

        return mesh;
    }

    private static List<Vector3> GetVertices(BinaryReader reader, int numIndices, long vertPosition)
    {
        List<Vector3> output = new List<Vector3>();
        List<int> indices = new List<int>();
        int tFactor = GameManager.translateFactor;

        for (int i = 0; i < numIndices; i++)
            indices.Add(reader.ReadInt16());

        long returnPosition = reader.BaseStream.Position;

        for (int v = 0; v < numIndices; v++)
        {
            reader.BaseStream.Seek(vertPosition, SeekOrigin.Begin);
            reader.BaseStream.Seek(indices[v] * 8, SeekOrigin.Current);

            float vert_x = (float)(reader.ReadInt16() << 8) / tFactor;
            float vert_y = -(float)(reader.ReadInt16() << 8) / tFactor;
            float vert_z = (float)(reader.ReadInt16() << 8) / tFactor;

            output.Add(new Vector3(vert_x, vert_y, vert_z));
        }

        reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);
        return output;
    }

    private static List<Vector3> GetNormals(BinaryReader reader, int numIndices, long normalPosition)
    {
        List<Vector3> output = new List<Vector3>();
        List<int> indices = new List<int>();
        int tFactor = GameManager.translateFactor;

        for (int i = 0; i < numIndices; i++)
            indices.Add(reader.ReadInt16());

        long returnPosition = reader.BaseStream.Position;

        for (int n = 0; n < numIndices; n++)
        {
            reader.BaseStream.Seek(normalPosition, SeekOrigin.Begin);
            reader.BaseStream.Seek(indices[n] * 8, SeekOrigin.Current);

            float normal_x = (float)(reader.ReadInt16() << 8) / tFactor;
            float normal_y = -(float)(reader.ReadInt16() << 8) / tFactor;
            float normal_z = (float)(reader.ReadInt16() << 8) / tFactor;

            output.Add(new Vector3(normal_x, normal_y, normal_z));
        }

        reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);
        return output;
    }

    private static List<Vector3> GetNormalsByIndex(BinaryReader reader, int numIndices, long normalPosition, int index)
    {
        List<Vector3> output = new List<Vector3>();
        List<int> indices = new List<int>();
        int tFactor = GameManager.translateFactor;

        for (int i = 0; i < numIndices; i++)
            indices.Add(index);

        long returnPosition = reader.BaseStream.Position;

        for (int n = 0; n < numIndices; n++)
        {
            reader.BaseStream.Seek(normalPosition, SeekOrigin.Begin);
            reader.BaseStream.Seek(indices[n] * 8, SeekOrigin.Current);

            float normal_x = (float)(reader.ReadInt16() << 8) / tFactor;
            float normal_y = -(float)(reader.ReadInt16() << 8) / tFactor;
            float normal_z = (float)(reader.ReadInt16() << 8) / tFactor;

            output.Add(new Vector3(normal_x, normal_y, normal_z));
        }

        reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);
        return output;
    }

    private static List<Vector2> GetUVs(BinaryReader reader, int numIndices, Material[] materials)
    {
        List<Vector2> output = new List<Vector2>();
        long returnPosition = reader.BaseStream.Position;

        reader.BaseStream.Seek(numIndices * 4 - 2, SeekOrigin.Current);
        int materialID = reader.ReadByte() + 1;
        reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);

        for (int i = 0; i < numIndices; i++)
        {
            float uv_x = (float)reader.ReadByte() / (materials[materialID].mainTexture.width - 1);
            float uv_y = 1f - (float)reader.ReadByte() / (materials[materialID].mainTexture.height - 1);

            output.Add(new Vector2(uv_x, uv_y));
            reader.ReadInt16(); //skip
        }

        reader.BaseStream.Seek(-2, SeekOrigin.Current);
        return output;
    }

    private static List<Vector2> GetUVsByIndex(BinaryReader reader, int numIndices, Material[] materials, int index)
    {
        List<Vector2> output = new List<Vector2>();
        long returnPosition = reader.BaseStream.Position;

        int materialID = index + 1;
        reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);

        for (int i = 0; i < numIndices; i++)
        {
            float uv_x = (float)reader.ReadByte() / (materials[materialID].mainTexture.width - 1);
            float uv_y = 1f - (float)reader.ReadByte() / (materials[materialID].mainTexture.height - 1);

            output.Add(new Vector2(uv_x, uv_y));
            reader.ReadInt16(); //skip
        }
        
        return output;
    }

    private static List<Vector3> GetEmptyNormals(int numIndices)
    {
        List<Vector3> output = new List<Vector3>();
        Vector3 empty = new Vector3(0, 0, 0);

        for (int i = 0; i < numIndices; i++)
            output.Add(empty);

        return output;
    }

    private static List<Vector2> GetEmptyUVs(int numIndices)
    {
        List<Vector2> output = new List<Vector2>();
        Vector2 empty = new Vector2(0, 0);

        for (int i = 0; i < numIndices; i++)
            output.Add(empty);

        return output;
    }
}
