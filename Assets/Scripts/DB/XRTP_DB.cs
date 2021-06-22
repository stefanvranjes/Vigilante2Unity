using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRTP_DB : MonoBehaviour
{
    public List<Texture2D> timFarList;
    public Vector3[] V3_DAT_0C; //0x0C
    public Color32[] C32_DAT_0C; //0x0C
    public Vector3[] V3_DAT_10; //0x10
    public Color32[] C32_DAT_10; //0x10
    public int DAT_14; //0x14
    public int DAT_18; //0x18
    public int DAT_1C; //0x1C
    public int DAT_20; //0x20
    public int DAT_24; //0x24
    public int DAT_28; //0x28
    public ushort DAT_2C; //0x2C
    public short DAT_2E; //0x2E
    public short DAT_30; //0x30

    private string prefabPath;
    private string prefabName;

    //76B8 (LOAD.DLL)
    public void LoadDB(string assetPath)
    {
        ushort uVar2;
        short sVar3;
        int iVar4;
        int uVar5;
        int iVar6;
        short local_21c;
        MemoryStream auStack536;

        LevelManager levelManager = GameObject.Find("GameControl").GetComponent<LevelManager>();
        GameManager gameManager = GameObject.Find("GameControl").GetComponent<GameManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            levelManager.xrtpList.Add(this);
            uVar5 = reader.ReadInt32BE();
            DAT_24 = uVar5;
            uVar5 = reader.ReadInt32BE();
            DAT_28 = uVar5;
            uVar5 = reader.ReadUInt16BE();
            DAT_1C = uVar5;
            uVar2 = reader.ReadUInt16BE();
            DAT_2C = uVar2;
            DAT_14 = 0;
            V3_DAT_0C = null;
            V3_DAT_10 = null;

            if (DAT_1C < 16)
                DAT_1C = 48;

            iVar4 = DAT_28 >> 8;
            iVar6 = DAT_24 >> 8;
            sVar3 = (short)(int)Utilities.SquareRoot(iVar4 * iVar4 + iVar6 * iVar6);
            iVar4 = DAT_24;
            DAT_30 = (short)(sVar3 - 128);

            if (iVar4 < 0)
                iVar4 += 255;

            iVar6 = DAT_28;

            if (iVar6 < 0)
                iVar6 += 255;

            iVar4 = (iVar4 >> 8) * (iVar6 >> 8);

            if (iVar4 < 0)
                iVar4 += 127;

            DAT_2E = (short)(iVar4 >> 7);

            if (12 < reader.BaseStream.Length)
            {
                long begin = reader.BaseStream.Position;
                iVar6 = 0;
                timFarList = new List<Texture2D>();
                Utilities.SetFarColor(gameManager.DAT_DDC.r, gameManager.DAT_DDC.g, gameManager.DAT_DDC.b);

                for (int i = 0; i < 16; i++)
                {
                    reader.BaseStream.Seek(begin, SeekOrigin.Begin);
                    auStack536 = new MemoryStream();

                    using (BinaryWriter writer = new BinaryWriter(auStack536, Encoding.Default, true))
                    {
                        writer.Write(reader.ReadBytes(16));
                        local_21c = reader.ReadInt16();
                        writer.Write(local_21c);
                        writer.Write(reader.ReadInt16());
                        Utilities.FUN_18C54(reader, local_21c, writer, iVar6);
                        writer.Write(reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position)));
                        writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    }

                    using (BinaryReader reader2 = new BinaryReader(auStack536, Encoding.Default, true))
                    {
                        string bmpPath = Application.dataPath + "/Resources/" + prefabPath + prefabName + "_FAR" + i.ToString().PadLeft(2, '0') + ".bmp";
                        IMP_TIM.LoadTIM(reader2, bmpPath);
                        timFarList.Add(Resources.Load<Texture2D>(bmpPath.Substring(Application.dataPath.Length + 16)));
                    }

                    iVar6 += 0x100;
                }
            }
        }
    }
}
