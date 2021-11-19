using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VigTerrain))]
public class TerrainEditor : Editor
{
    VigTerrain terrain;
    GameManager gameManager;
    
    SerializedProperty bitmap;
    SerializedProperty defaultVertex;
    SerializedProperty defaultTile;
    SerializedProperty chunkNum;
    SerializedProperty tileXZ;
    SerializedProperty tileY;

    private void OnEnable()
    {
        terrain = GameObject.FindObjectOfType<VigTerrain>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        bitmap = serializedObject.FindProperty("bitmapID");
        defaultVertex = serializedObject.FindProperty("defaultVertex");
        defaultTile = serializedObject.FindProperty("defaultTile");
        chunkNum = serializedObject.FindProperty("zoneCount");
        tileXZ = serializedObject.FindProperty("tileXZ");
        tileY = serializedObject.FindProperty("tileY");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(bitmap);
        EditorGUILayout.PropertyField(defaultVertex);
        EditorGUILayout.PropertyField(defaultTile);
        EditorGUILayout.PropertyField(tileXZ);
        EditorGUILayout.PropertyField(tileY);
        EditorGUILayout.PropertyField(chunkNum);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Set # Chunks"))
        {
            terrain.SetNumberOfZones();
            EditorUtility.SetDirty(terrain);
        }

        EditorGUILayout.LabelField("Import Asset");

        if (GUILayout.Button("TIN"))
        {
            string file = EditorUtility.OpenFilePanel("Open TIN file to load asset", "", "");
            IMP_TIN.LoadAsset(file);
            EditorUtility.SetDirty(terrain);
        }

        if (GUILayout.Button("ZONE"))
        {
            string file = EditorUtility.OpenFilePanel("Open ZONE file to load asset", "", "");
            IMP_ZONE.LoadAsset(file);
            EditorUtility.SetDirty(terrain);
        }

        if (GUILayout.Button("ZMAP"))
        {
            string file = EditorUtility.OpenFilePanel("Open ZMAP file to load asset", "", "");
            IMP_ZMAP.LoadAsset(file);
            EditorUtility.SetDirty(terrain);
        }

        if (GUILayout.Button("Generate Mesh"))
        {
            Mesh levelMesh = GenerateMesh();
            terrain.GetComponent<MeshFilter>().sharedMesh = levelMesh;

            levelMesh.name = "newGeo";
            Debug.Log("New Level Geometry Generated Successfuly! Polys: " + levelMesh.triangles.Length);
        }
    }

    private Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUV = new List<Vector2>();
        List<int> newTriangles = new List<int>();
        terrain = GameObject.FindObjectOfType<VigTerrain>();
        Material mat = terrain.GetComponent<MeshRenderer>().sharedMaterial;
        int vertIndex = 0;

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        for (int i = 0; i < terrain.chunks.Length; i++)
        {
            int tFactor = GameManager.instance.translateFactor;
            int tileXZ = terrain.tileXZ;
            int tileY = terrain.tileY;

            if (terrain.chunks[i] == 0 || terrain.zoneCount <= terrain.chunks[i])
                continue;

            int zone = terrain.chunks[i];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    float vert1_x = Utilities.MoveDecimal((long)x * tileXZ + (long)tileXZ * 64 * (i / 32), tFactor);
                    float vert1_y = -Utilities.MoveDecimal((long)((int)((short)terrain.vertices[zone * 4096 + x * 64 + y] & 0x7FF)) * tileY, tFactor);
                    float vert1_z = Utilities.MoveDecimal((long)y * tileXZ + (long)tileXZ * 64 * (i % 32), tFactor);
                    newVertices.Add(new Vector3(vert1_x, vert1_y, vert1_z));

                    float vert2_x = Utilities.MoveDecimal((long)(x + 1) * tileXZ + (long)tileXZ * 64 * (i / 32), tFactor);
                    int nextZone = x + 1 < 64 ? zone : terrain.chunks[i + 32];
                    int nextX = x + 1 < 64 ? x + 1 : 0;
                    float vert2_y = -Utilities.MoveDecimal((long)((int)((short)terrain.vertices[nextZone * 4096 + nextX * 64 + y] & 0x7FF)) * tileY, tFactor);
                    float vert2_z = Utilities.MoveDecimal((long)y * tileXZ + (long)tileXZ * 64 * (i % 32), tFactor);
                    newVertices.Add(new Vector3(vert2_x, vert2_y, vert2_z));

                    float vert3_x = Utilities.MoveDecimal((long)x * tileXZ + (long)tileXZ * 64 * (i / 32), tFactor);
                    nextZone = y + 1 < 64 ? zone : terrain.chunks[i + 1];
                    int nextY = y + 1 < 64 ? y + 1 : 0;
                    float vert3_y = -Utilities.MoveDecimal((long)((int)((short)terrain.vertices[nextZone * 4096 + x * 64 + nextY] & 0x7FF)) * tileY, tFactor);
                    float vert3_z = Utilities.MoveDecimal((long)(y + 1) * tileXZ + (long)tileXZ * 64 * (i % 32), tFactor);
                    newVertices.Add(new Vector3(vert3_x, vert3_y, vert3_z));

                    float vert4_x = Utilities.MoveDecimal((long)(x + 1) * tileXZ + (long)tileXZ * 64 * (i / 32), tFactor);
                    if (x + 1 >= 64 && y + 1 >= 64)
                    {
                        nextZone = terrain.chunks[i + 33];
                        nextX = 0;
                        nextY = 0;
                    }
                    else if (x + 1 >= 64)
                    {
                        nextZone = terrain.chunks[i + 32];
                        nextX = 0;
                        nextY = y + 1;
                    }
                    else if (y + 1 >= 64)
                    {
                        nextZone = terrain.chunks[i + 1];
                        nextX = x + 1;
                        nextY = 0;
                    }
                    else
                    {
                        nextZone = zone;
                        nextX = x + 1;
                        nextY = y + 1;
                    }
                    float vert4_y = -Utilities.MoveDecimal((long)((int)((short)terrain.vertices[nextZone * 4096 + nextX * 64 + nextY] & 0x7FF)) * tileY, tFactor);
                    float vert4_z = Utilities.MoveDecimal((long)(y + 1) * tileXZ + (long)tileXZ * 64 * (i % 32), tFactor);
                    newVertices.Add(new Vector3(vert4_x, vert4_y, vert4_z));

                    int tileIndex = zone * 4096 + x * 64 + y;

                    float uv1_x = (float)terrain.tileData[terrain.tiles[tileIndex]].uv1_x / (mat.mainTexture.width - 1);
                    float uv1_y = 1.0f - (float)terrain.tileData[terrain.tiles[tileIndex]].uv1_y / (mat.mainTexture.height - 1);

                    float uv2_x = (float)terrain.tileData[terrain.tiles[tileIndex]].uv2_x / (mat.mainTexture.width - 1);
                    float uv2_y = 1.0f - (float)terrain.tileData[terrain.tiles[tileIndex]].uv2_y / (mat.mainTexture.height - 1);

                    float uv3_x = (float)terrain.tileData[terrain.tiles[tileIndex]].uv3_x / (mat.mainTexture.width - 1);
                    float uv3_y = 1.0f - (float)terrain.tileData[terrain.tiles[tileIndex]].uv3_y / (mat.mainTexture.height - 1);

                    float uv4_x = (float)terrain.tileData[terrain.tiles[tileIndex]].uv4_x / (mat.mainTexture.width - 1);
                    float uv4_y = 1.0f - (float)terrain.tileData[terrain.tiles[tileIndex]].uv4_y / (mat.mainTexture.height - 1);

                    newUV.Add(new Vector2(uv1_x, uv1_y));
                    newUV.Add(new Vector2(uv2_x, uv2_y));
                    newUV.Add(new Vector2(uv3_x, uv3_y));
                    newUV.Add(new Vector2(uv4_x, uv4_y));

                    newTriangles.Add(vertIndex + 2);
                    newTriangles.Add(vertIndex + 1);
                    newTriangles.Add(vertIndex + 0);
                    newTriangles.Add(vertIndex + 3);
                    newTriangles.Add(vertIndex + 1);
                    newTriangles.Add(vertIndex + 2);

                    vertIndex += 4;
                }
            }
        }

        mesh.SetVertices(newVertices);
        mesh.SetUVs(0, newUV);
        mesh.SetTriangles(newTriangles, 0);

        return mesh;
    }
}
