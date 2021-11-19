using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        int iVar4;
        Vehicle vVar6;

        if (hit.self.type == 2)
        {
            vVar6 = (Vehicle)hit.self;
            vVar6.physics1.Y -= 0x5f580;
            flags |= 0x20;
            LevelManager.instance.FUN_39AF8(vVar6);
            vVar6.FUN_3A064(-25, vTransform.position, true);
            UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x00, 0x00, 8));
            iVar4 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 69, vTransform.position);
            LevelManager.instance.FUN_4E8C8(vTransform.position, 48);
            return 0;
        }

        return 0;
    }

    public int DAT_90; //0x90
    public Mud3 DAT_94; //0x94

    //FUN_190 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        Mud3 mVar2;
        int iVar3;
        int iVar6;
        TileData tVar6;
        Vector3Int auStack16;
        Vector3Int auStack8;

        if (arg1 < 4)
        {
            if (arg1 != 0)
                return 0;

            if ((flags & 0x20) == 0)
            {
                vTransform.position.x += physics1.X;
                iVar6 = GameManager.instance.DAT_DB0;
                vTransform.position.z += physics1.Z;
                iVar2 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (iVar6 - 0xa000 < iVar2)
                    iVar2 = iVar6 - 0xa000;

                vTransform.position.y = iVar2;
                auStack16 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                auStack16 = Utilities.VectorNormal(auStack16);
                child2.vTransform.rotation = Utilities.FUN_2A5EC(auStack16);

                if (tags == 0)
                {
                    tVar6 = GameManager.instance.terrain.GetTileByPosition((uint)vTransform.position.x, (uint)vTransform.position.z);

                    if (tVar6.DAT_10[3] == 1)
                        return 0;
                }
                else
                {
                    if (tags != 1)
                        return 0;

                    mVar2 = DAT_94;
                    iVar6 = DAT_90;

                    if (iVar6 < mVar2.DAT_8C)
                    {
                        iVar3 = vTransform.position.x - mVar2.DAT_90[iVar6].x;

                        if (iVar3 < 0)
                            iVar3 = -iVar3;

                        if (0xefff < iVar3)
                            return 0;

                        iVar3 = vTransform.position.z - mVar2.DAT_90[iVar6].z;

                        if (iVar3 < 0)
                            iVar3 = -iVar3;

                        if (iVar3 < 0xf000)
                        {
                            iVar6++;
                            DAT_90 = iVar6;

                            if (mVar2.DAT_8C <= iVar6)
                                return 0;

                            Utilities.FUN_2A168(out auStack8, vTransform.position, mVar2.DAT_90[iVar6]);
                            iVar6 = auStack8.x * 9155;

                            if (iVar6 < 0)
                                iVar6 += 4095;

                            physics1.X = iVar6 >> 12;
                            physics1.Y = 0;
                            iVar6 = auStack8.z * 9155;

                            if (iVar6 < 0)
                                iVar6 += 4095;

                            physics1.Z = iVar6 >> 12;
                            return 0;
                        }

                        return 0;
                    }
                }

                flags |= 0x20;
            }
            else
            {
                sVar1 = physics1.M6;
                physics1.M6 = (short)(sVar1 - 136);
                vTransform.rotation.V22 = sVar1;
                vTransform.rotation.V11 = sVar1;
                vTransform.rotation.V00 = sVar1;

                if (sVar1 - 136 < 205)
                {
                    GameManager.instance.FUN_309A0(this);
                    return 0xffffffff;
                }
            }
        }
        else
        {
            if (arg1 != 4)
                return 0;

            GameManager.instance.FUN_1DE78(DAT_18);
        }

        return 0;
    }
}
