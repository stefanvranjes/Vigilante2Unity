using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : VigObject
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
        uint uVar7;
        int iVar8;
        Vehicle vVar12;

        if (hit.self.type != 2)
            return 0;

        vVar12 = (Vehicle)hit.self;
        vVar12.physics1.Y = -195200;
        vVar12.physics2.Y += 4000;
        uVar7 = GameManager.FUN_2AC5C();

        if ((uVar7 & 31) != 0)
            return 0;

        iVar8 = vVar12.FUN_3B078(DAT_80, (ushort)DAT_1A, -30, 1);
        vVar12.FUN_3A020(iVar8, DAT_20, true);
        return 0;
    }

    private static Vector3Int DAT_20 = new Vector3Int(0, 0, 0);

    //FUN_70 (FRONTIER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        sbyte sVar2;
        short sVar3;
        short sVar4;
        Ballistic puVar5;
        int iVar6;
        uint uVar7;
        int iVar8;
        int iVar11;
        int iVar12;
        Throwaway puVar13;
        Vector3Int auStack16;
        Vector3Int sStack8;

        if (arg1 != 1)
        {
            if (arg1 == 0)
            {
                iVar11 = physics1.W;
                Utilities.FUN_2A168(out auStack16, vTransform.position, DAT_84.screen);
                iVar12 = auStack16.x * iVar11;

                if (iVar12 < 0)
                    iVar12 += 4095;

                vTransform.position.x += iVar12 >> 12;
                iVar12 = auStack16.z * iVar11;

                if (iVar12 < 0)
                    iVar12 += 4095;

                vTransform.position.z += iVar12 >> 12;
                iVar8 = FUN_2CFBC(vTransform.position);
                vTransform.position.y = iVar8;
                physics1.W = iVar11 - 33;

                if (tags == 0)
                {
                    if ((GameManager.instance.DAT_28 - DAT_19 & 15) == 0)
                    {
                        uVar7 = GameManager.FUN_2AC5C();
                        iVar8 = 151;

                        if ((uVar7 & 1) != 0)
                            iVar8 = 150;

                        puVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)iVar8, typeof(Ballistic), 8) as Ballistic;
                        puVar5.type = 7;
                        puVar5.flags = 0x434;
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        puVar5.screen.x = vTransform.position.x + (iVar12 * 0x5000 >> 15) - 0x2800;
                        puVar5.screen.y = vTransform.position.y;
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        puVar5.screen.z = vTransform.position.z + (iVar12 * 0x5000 >> 15) - 0x2800;
                        sVar3 = (short)GameManager.FUN_2AC5C();
                        puVar5.vr.z = sVar3;
                        puVar5.FUN_3066C();
                        return 0;
                    }

                    uVar7 = GameManager.FUN_2AC5C();

                    if ((uVar7 & 31) != 0)
                        return 0;

                    uVar7 = GameManager.FUN_2AC5C();
                    iVar8 = 5;

                    if ((uVar7 & 1) != 0)
                        iVar8 = 4;

                    puVar13 = vData.ini.FUN_2C17C((ushort)iVar8, typeof(Throwaway), 0) as Throwaway;
                    iVar11 = (int)GameManager.FUN_2AC5C();
                    uVar7 = GameManager.FUN_2AC5C();
                    iVar6 = (int)(uVar7 & 0xfff) * 2;
                    iVar12 = GameManager.DAT_65C90[iVar6] * 4577;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    puVar13.physics1.Z = iVar12 >> 12;
                    iVar12 = GameManager.DAT_65C90[(((iVar11 << 8) >> 15) + 0x100 & 0xfff) * 2] * 4577;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    puVar13.physics1.W = iVar12 >> 12;
                    iVar12 = GameManager.DAT_65C90[iVar6 + 1] * 4577;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    puVar13.physics2.X = iVar12 >> 12;
                    sVar4 = (short)GameManager.FUN_2AC5C();
                    puVar13.physics1.M0 = (short)(sVar4 & 0xff);
                    sVar4 = (short)GameManager.FUN_2AC5C();
                    puVar13.physics1.M1 = (short)(sVar4 & 0xff);
                    sVar4 = (short)GameManager.FUN_2AC5C();
                    puVar13.physics1.M2 = (short)(sVar4 & 0xff);
                    sVar3 = id;
                    puVar13.type = 7;
                    puVar13.flags = 0x180;
                    puVar13.state = _THROWAWAY_TYPE.Unspawnable;
                    puVar13.id = sVar3;
                    puVar13.vTransform = vTransform;
                    puVar13.DAT_87 = 2;
                    puVar13.FUN_2D1DC();
                    puVar13.FUN_305FC();
                    return 0;
                }

                sVar1 = physics2.M2;
                sStack8 = new Vector3Int(sVar1, sVar1, sVar1);
                ApplyRotationMatrix();
                Utilities.FUN_245AC(ref vTransform.rotation, sStack8);
                physics2.M2 = (short)(sVar1 - 136);
            }
            else
            {
                if (arg1 == 2)
                {
                    sVar2 = (sbyte)(tags + 1);
                    tags = sVar2;

                    if (sVar2 != 1)
                    {
                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }

                    physics2.M2 = 0x1000;
                    GameManager.instance.FUN_30CB0(this, 30);
                }
            }
        }

        return 0;
    }
}
