using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NASA : VigObject
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
        sbyte sVar1;
        int iVar2;
        VigObject oVar3;
        LaunchVehicle vVar3;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) == 2)
        {
            if (hit.self.type != 2)
                return 0;

            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar2, vData.sndList, 7, vTransform.position);
            UIManager.instance.FUN_4E338(new Color32(0x80, 0x80, 0x80, 8));
            oVar3 = child2;

            do
            {
                if (oVar3 == null)
                {
                    vVar3 = GameManager.instance.FUN_302A8(GameManager.instance.worldObjs, typeof(LaunchVehicle)) as LaunchVehicle;

                    if (vVar3 == null)
                        return 0;

                    if (vVar3.tags == 0)
                    {
                        if ((vVar3.flags & 0x1000000) == 0)
                        {
                            vVar3.physics1.W = 0;
                            vVar3.FUN_30B78();
                            sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                            vVar3.DAT_18 = sVar1;
                            GameManager.instance.FUN_1E628(sVar1, vVar3.vData.sndList, 0, vVar3.vTransform.position, true);
                        }
                        else
                        {
                            GameManager.instance.FUN_1DE78(vVar3.DAT_18);
                            vVar3.DAT_18 = 0;
                            vVar3.FUN_30BA8();
                        }

                        vVar3.flags ^= 0x1000000;
                    }
                    else
                    {
                        if (vVar3.tags == 1 && (vVar3.flags & 0x1000000) == 0)
                        {
                            vVar3.flags |= 0x1000000;
                            GameManager.instance.FUN_30CB0(vVar3, 3);
                        }
                    }

                    GameManager.instance.FUN_30CB0(this, 180);
                    return 0;
                }

                if (oVar3.id == 2)
                {
                    oVar3.flags |= 0x20;
                    vVar3 = GameManager.instance.FUN_302A8(GameManager.instance.worldObjs, typeof(LaunchVehicle)) as LaunchVehicle;

                    if (vVar3 == null)
                        return 0;

                    if (vVar3.tags == 0)
                    {
                        if ((vVar3.flags & 0x1000000) == 0)
                        {
                            vVar3.physics1.W = 0;
                            vVar3.FUN_30B78();
                            sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                            vVar3.DAT_18 = sVar1;
                            GameManager.instance.FUN_1E628(sVar1, vVar3.vData.sndList, 0, vVar3.vTransform.position, true);
                        }
                        else
                        {
                            GameManager.instance.FUN_1DE78(vVar3.DAT_18);
                            vVar3.DAT_18 = 0;
                            vVar3.FUN_30BA8();
                        }

                        vVar3.flags ^= 0x1000000;
                    }
                    else
                    {
                        if (vVar3.tags == 1 && (vVar3.flags & 0x1000000) == 0)
                        {
                            vVar3.flags |= 0x1000000;
                            GameManager.instance.FUN_30CB0(vVar3, 3);
                        }
                    }

                    GameManager.instance.FUN_30CB0(this, 180);
                    return 0;
                }

                oVar3 = oVar3.child2;
            } while (true);
        }

        if (hit.self.type != 8)
            return 0;

        hit.object1.FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_2004 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar3;

        if (arg1 == 2)
        {
            oVar3 = child2;

            while (oVar3 != null)
            {
                if (oVar3.id == 2)
                {
                    oVar3.flags &= 0xffffffdf;
                    return 0;
                }

                oVar3 = oVar3.child2;
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 1)
                {
                    oVar3 = child2;

                    while (oVar3 != null)
                    {
                        if (oVar3.id == 1)
                            oVar3.maxHalfHealth = 100;
                        else
                        {
                            if (oVar3.id == 2)
                                oVar3.type = 3;
                        }

                        oVar3 = oVar3.child2;
                    }
                }
            }
            else
            {
                if (arg1 != 8)
                    return 0;
            }

            FUN_32B90((uint)arg2);
        }

        return 0;
    }
}
