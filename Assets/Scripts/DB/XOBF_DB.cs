using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOBF_DB : MonoBehaviour
{
    //FUN_32F40
    public static void LoadDB(string assetPath, int param2)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader != null)
            {
                string headerString = new string(reader.ReadChars(4));

                if (headerString == "XOBF")
                {
                    do
                    {

                    }
                }
            }
        }
    }
}
