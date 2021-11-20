using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRocket3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public static Vector3Int DAT_C4 = new Vector3Int(0, -0x800, 0x1000); //0xC4 (LAUNCH.DLL)
    public static Vector3Int DAT_D4 = new Vector3Int(0, 0, 0); //0xD4 (LAUNCH.DLL)

    public override uint OnCollision(HitDetection hit)
    {
        int iVar3;
        uint uVar3;
        VigObject oVar10;
        Vehicle vVar10;
        Vector3Int local_10;

        oVar10 = hit.self;
        uVar3 = 0;

        if (oVar10.type == 2)
        {
            vVar10 = (Vehicle)oVar10;
            local_10 = Utilities.FUN_24094(vTransform.rotation, DAT_C4);
            vVar10.FUN_2B370(local_10, vTransform.position);
            iVar3 = -6;

            if (vVar10.id < 0)
                iVar3 = -12;

            vVar10.FUN_3A064(iVar3, DAT_D4, true);
            LevelManager.instance.FUN_39AF8(vVar10);
            uVar3 = 0;
        }

        return uVar3;
    }

    //FUN_15F0 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        short sVar2;
        uint uVar3;
        uint uVar4;
        LaunchRocket4 ppcVar5;
        LaunchRocketChild puVar6;
        Vector3Int local_10;
        Vector3Int local_8;

        if (arg1 == 2)
        {
            GameManager.instance.FUN_309A0(this);
            uVar3 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                uVar3 = 0;

                if (arg1 == 0)
                {
                    uVar4 = GameManager.FUN_2AC5C();
                    uVar3 = 0;

                    if ((uVar4 & 7) == 0)
                    {
                        Dictionary<int, Type> dict = new Dictionary<int, Type>();
                        dict.Add(115, typeof(LaunchRocketChild));
                        ppcVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(114, typeof(LaunchRocket4), 8, dict) as LaunchRocket4;
                        Utilities.ParentChildren(ppcVar5, ppcVar5);
                        local_8 = new Vector3Int(0, 0, 0);
                        iVar1 = (int)GameManager.FUN_2AC5C();
                        local_8.x = (iVar1 & 0xff) - 128 << 4;
                        iVar1 = (int)GameManager.FUN_2AC5C();
                        local_8.y = (iVar1 & 0xff) - 128 << 4;
                        iVar1 = (int)GameManager.FUN_2AC5C();
                        local_8.z = 0x600 - (iVar1 & 0xff) << 4;
                        local_10 = local_8;
                        ppcVar5.flags = 0xb4;
                        ppcVar5.type = 3;
                        ppcVar5.maxHalfHealth = 5;
                        sVar2 = (short)GameManager.FUN_2AC5C();
                        ppcVar5.vr.z = sVar2;
                        ppcVar5.vTransform = vTransform;
                        local_10 = Utilities.ApplyMatrixSV(ppcVar5.vTransform.rotation, local_10);
                        ppcVar5.screen = local_10;
                        ppcVar5.FUN_305FC();
                        puVar6 = ppcVar5.child2 as LaunchRocketChild;

                        if (puVar6 == null)
                            uVar3 = 0;
                        else
                        {
                            do
                            {
                                puVar6.euler = new Vector3(0, -ppcVar5.vTransform.rotation.Matrix2Quaternion.eulerAngles.y, 0);
                                puVar6 = puVar6.child as LaunchRocketChild;
                            } while (puVar6 != null);

                            uVar3 = 0;
                        }
                    }
                }
            }
            else
                uVar3 = 0;
        }

        return uVar3;
    }
}
