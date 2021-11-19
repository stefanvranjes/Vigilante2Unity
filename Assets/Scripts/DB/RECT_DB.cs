using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RECT_DB : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        VigTuple2 newTuple = new VigTuple2(type, id, array);
        GameManager.instance.DAT_10D8.Add(newTuple);
    }

    public short type;
    public short id;
    public short[] array;

    //FUN_2C3C (LOAD.DLL)
    public bool LoadDB(string assetPath)
    {
        short sVar3;
        short sVar4;

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader == null)
                return false;

            array = new short[4];
            sVar3 = reader.ReadInt16BE();
            array[0] = sVar3;
            sVar3 = reader.ReadInt16BE();
            array[1] = sVar3;
            sVar4 = reader.ReadInt16BE();
            array[2] = (short)((sVar4 - array[0]) + 1);
            sVar4 = reader.ReadInt16BE();
            array[3] = (short)((sVar4 - array[1]) + 1);
            reader.ReadInt16BE();
            sVar3 = reader.ReadInt16BE();
            type = sVar3;
            sVar3 = reader.ReadInt16BE();
            id = sVar3;
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
        EditorUtility.SetDirty(levelManager.gameObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
        return true;
    }
}
