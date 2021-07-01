using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_XBGM
{
    public static void LoadAsset(string assetPath, string bmp)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar1;
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            VigTerrain terrain = GameObject.Find("Terrain").GetComponent<VigTerrain>();

            iVar1 = reader.ReadInt32BE();
            IMP_TIM.LoadTIM(reader, bmp);
            levelManager.DAT_DD0 = Resources.Load<Texture2D>(bmp);
            levelManager.DAT_6398A = (short)(iVar1 << 4);
            levelManager.DAT_63972 = (short)((iVar1 - levelManager.DAT_DD0.height) * 16);
            levelManager.DAT_6397A = levelManager.DAT_63972;
            levelManager.DAT_63982 = levelManager.DAT_63972;
            levelManager.DAT_63992 = levelManager.DAT_6398A;
            levelManager.DAT_6399A = levelManager.DAT_6398A;

            if (levelManager.DAT_E48 == null)
                levelManager.DAT_E48 = levelManager.DAT_DD0;

            levelManager.DAT_E58 = Resources.Load<Texture2D>("SHELL/Lens.bmp");
        }
    }
}
