using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRocket : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private static Vector3Int DAT_E0 = new Vector3Int(0x3000, 0x3000, 0x3000); //0xE0 (LAUNCH.DLL)

    //FUN_18D8
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        uint uVar1;
        Particle1 ppcVar2;
        int iVar3;
        int iVar4;
        Vector3Int local_20;
        Vector3Int local_10;

        if (arg1 == 2)
            tags++;
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                switch (tags)
                {
                    case 0:
                    case 10:
                        uVar1 = GameManager.FUN_2AC5C();

                        if ((uVar1 & 7) != 0)
                            return 0;

                        local_20 = vTransform.position;
                        ppcVar2 = LevelManager.instance.FUN_4DE54(local_20, 5);
                        ppcVar2.state = _PARTICLE1_TYPE.LaunchRocket;
                        ppcVar2.flags |= 0x400;
                        iVar1 = (int)GameManager.FUN_2AC5C();
                        ppcVar2.screen.x = (iVar1 & 0x7ff) - 0x400;
                        ppcVar2.screen.y = -0x70;
                        iVar1 = (int)GameManager.FUN_2AC5C();
                        ppcVar2.screen.z = (iVar1 & 0x7ff) - 0x400;
                        Launch.FUN_5730(ref ppcVar2.vTransform.rotation, DAT_E0);
                        ppcVar2.FUN_30B78();
                        return 0;
                    case 1:
                    case 11:
                        uVar1 = GameManager.FUN_2AC5C();

                        if ((uVar1 & 7) == 0)
                        {
                            local_10 = new Vector3Int();
                            local_10.x = vTransform.position.x;
                            local_10.y = vTransform.position.y + 0x4b000;
                            local_10.z = vTransform.position.z;
                            ppcVar2 = LevelManager.instance.FUN_4DE54(local_10, 5);
                            ppcVar2.state = _PARTICLE1_TYPE.LaunchRocket;
                            ppcVar2.flags |= 0x400;
                            iVar1 = (int)GameManager.FUN_2AC5C();
                            ppcVar2.screen.x = (iVar1 & 0x7ff) - 0x400;
                            ppcVar2.screen.y = -0x70;
                            iVar1 = (int)GameManager.FUN_2AC5C();
                            ppcVar2.screen.z = (iVar1 & 0x7ff) - 0x400;
                            Launch.FUN_5730(ref ppcVar2.vTransform.rotation, DAT_E0);
                            ppcVar2.FUN_30B78();
                        }

                        iVar4 = physics1.X - 42;
                        physics1.X = iVar4;
                        iVar3 = -0x12a06;

                        if (-0x12a06 < iVar4)
                            iVar3 = iVar4;

                        physics1.X = iVar3;

                        if (tags == 1)
                        {
                            iVar3 = vTransform.position.y + iVar3;
                            vTransform.position.y = iVar3;

                            if (iVar3 < 0x16f801)
                                tags = 2;
                        }
                        else
                        {
                            iVar3 = vTransform.position.y + iVar3;
                            vTransform.position.y = iVar3;

                            if (iVar3 < 0x205801)
                            {
                                FUN_4DC94();
                                GameManager.instance.FUN_309A0(this);
                                return 0xffffffff;
                            }
                        }

                        break;
                    case 2:
                        iVar4 = physics1.X - 42;
                        physics1.X = iVar4;
                        iVar3 = -0x12a06;

                        if (-0x12a06 < iVar4)
                            iVar3 = iVar4;

                        physics1.X = iVar3;
                        iVar3 = vTransform.position.y + iVar3;
                        vTransform.position.y = iVar3;

                        if (iVar3 < -0x2dc7ff)
                        {
                            GameManager.instance.FUN_309A0(this);
                            return 0xffffffff;
                        }

                        break;
                }
            }
            else
            {
                if (arg1 != 4)
                    return 0;

                if (DAT_18 == 0)
                    return 0;

                GameManager.instance.FUN_1DE78(DAT_18);
                DAT_18 = 0;
            }
        }

        return 0;
    }
}
