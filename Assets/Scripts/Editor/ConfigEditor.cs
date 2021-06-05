using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConfigEditor
{
    private static string previousPath = "";

    [MenuItem("CONTEXT/VehicleConfig/Import Config")]
    public static void ImportConfig(MenuCommand menuCommand)
    {
        VigConfig config = menuCommand.context as VigConfig;
        string path = EditorUtility.OpenFilePanel("Open .ini file to import vehicle data", previousPath, "ini");
        previousPath = Path.GetDirectoryName(path);

        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            int elementCount = reader.ReadInt32();
            config.configContainers = new List<ConfigContainer>();

            for (int i = 0; i < elementCount; i++)
            {
                config.configContainers.Add(new ConfigContainer());
                config.configContainers[i] = new ConfigContainer();
                config.configContainers[i].flag = reader.ReadInt16();
                config.configContainers[i].colliderID = reader.ReadInt16();
                config.configContainers[i].v3_1 = new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                config.configContainers[i].v3_2 = new Vector3Int(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
                config.configContainers[i].objID = reader.ReadInt16();
                config.configContainers[i].previous = reader.ReadInt16();
                config.configContainers[i].next = reader.ReadInt16();
            }
        }
    }
}
