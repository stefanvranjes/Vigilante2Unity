using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigMesh : MonoBehaviour
{
    public byte DAT_00; //0x00
    public byte DAT_01; //0x01
    public short DAT_02; //0x02
    public ushort vertices; //0x04
    public ushort faces; //0x06
    public byte[] vertexStream; //0x08
    public byte[] normalStream; //0x0C
    public byte[] faceStream; //0x10
    public byte[] DAT_14; //0x14
    public uint DAT_18; //0x18
    public Texture2D[] DAT_1C; //0x1C

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FUN_39A8 (LOAD.DLL)
    public void FUN_39A8(DELEGATE_79A0 param1)
    {
        byte bVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        MemoryStream msVar4;
        MemoryStream msVar5;
        MemoryStream msVar6;
        long lVar7;
        long lVar8;
        long lVar9;
        MemoryStream msVar7;
        MemoryStream msVar8;

        iVar4 = 0;
        msVar6 = new MemoryStream(faceStream);

        if (0 < faces)
        {
            using (BinaryReader reader = new BinaryReader(msVar6, Encoding.Default, true))
            {
                for (int i = 0; i < faces; i++)
                {
                    lVar7 = reader.BaseStream.Position;
                    reader.BaseStream.Seek(3, SeekOrigin.Current);
                    bVar1 = reader.ReadByte();

                    switch((uint)bVar1 >> 2 & 15)
                    {
                        case 4:
                        case 5:
                        case 7:
                            iVar4++;
                            break;
                        case 8:
                        case 9:
                        case 11:
                            iVar4 += 3;
                            break;
                        case 10:
                            reader.BaseStream.Seek(6, SeekOrigin.Current);
                            lVar7 += reader.ReadUInt16() * 8;
                            break;
                    }

                    reader.BaseStream.Seek(lVar7 + GameManager.DAT_854[(uint)bVar1 >> 2 & 15], SeekOrigin.Begin);
                }

                reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }

        msVar4 = new MemoryStream(new byte[iVar4 << 2]);
        DAT_14 = msVar4.GetBuffer();

        if (0 < faces)
        {
            using (BinaryReader brVar6 = new BinaryReader(msVar6, Encoding.Default, true))
            {
                using (BinaryWriter bwVar4 = new BinaryWriter(msVar4, Encoding.Default, true))
                {
                    for (int i = 0; i < faces; i++)
                    {
                        lVar7 = brVar6.BaseStream.Position;

                        switch((uint)brVar6.ReadByte(3) >> 2 & 15)
                        {
                            case 4:
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x20), 3);
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            case 5:
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x24), 3);
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            default:
                                goto switchD_00003ae4_caseD_6;
                            case 7:
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x24), 3);
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            case 8:
                                bVar1 = (byte)(brVar6.ReadByte(3) & 3 | 0x30);
                                goto LAB_00003bb8;
                            case 9:
                            case 11:
                                bVar1 = (byte)(brVar6.ReadByte(3) & 3 | 0x34);
                                LAB_00003bb8:
                                bwVar4.Write(bVar1, 11);
                                bwVar4.Write(bVar1, 7);
                                bwVar4.Write(bVar1, 3);
                                msVar7 = new MemoryStream(vertexStream);
                                msVar7.Seek(brVar6.ReadUInt16(4), SeekOrigin.Begin);
                                msVar8 = new MemoryStream(normalStream);
                                msVar8.Seek(brVar6.ReadUInt16(10), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.BaseStream.Seek(4, SeekOrigin.Current);
                                msVar7.Seek(brVar6.ReadUInt16(6), SeekOrigin.Begin);
                                msVar8.Seek(brVar6.ReadUInt16(12), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.BaseStream.Seek(-4, SeekOrigin.Current);
                                lVar8 = bwVar4.BaseStream.Position + 12;
                                uVar3 = brVar6.ReadUInt16(8);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(14);
                                bwVar4.BaseStream.Seek(8, SeekOrigin.Current);
                                break;
                            case 10:
                                lVar7 += brVar6.ReadUInt16(10) * 8;
                                goto switchD_00003ae4_caseD_6;
                        }

                        msVar5.Seek(uVar3, SeekOrigin.Begin);
                        msVar7 = new MemoryStream(normalStream);
                        msVar7.Seek(uVar2, SeekOrigin.Begin);
                        param1(msVar4, msVar6, msVar5, msVar7);
                        bwVar4.BaseStream.Seek(lVar8, SeekOrigin.Begin);
                        switchD_00003ae4_caseD_6:
                        brVar6.BaseStream.Seek(lVar7 + GameManager.DAT_854[brVar6.ReadByte(3) >> 2 & 15], SeekOrigin.Begin);
                    }
                }
            }
        }
    }
}
