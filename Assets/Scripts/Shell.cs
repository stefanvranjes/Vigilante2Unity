using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Shell : VigObject
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
        VigObject oVar3;
        Vehicle vVar3;
        Particle2 pVar3;
        int iVar5;
        uint uVar7;

        if (hit.object2.type == 3)
            return 0;

        sVar1 = tags;

        if (sVar1 == 1)
        {
            LevelManager.instance.FUN_4DE54(screen, 140);
            //sound
            oVar3 = hit.self;

            if (oVar3.type == 2)
            {
                vVar3 = (Vehicle)oVar3;
                vVar3.physics1.Y = -0x8F080;
                vVar3.physics2.Z = 52000;

                if (vVar3.id < 0)
                    GameManager.instance.FUN_15B00(~vVar3.id, 255, 2, 128);
            }
        }
        else
        {
            if (sVar1 < 2)
            {
                if (sVar1 == 0)
                {
                    uVar7 = FUN_42638(hit, 37, 66);
                    return uVar7;
                }

                return 0;
            }

            if (sVar1 == 2)
            {
                //FUN_4E414

                if (hit.self.type == 2 && (hit.self.flags & 0x10000000) != 0)
                {
                    pVar3 = LevelManager.instance.FUN_4E128(screen, 53, 0);
                    pVar3.FUN_2D114(pVar3.screen, ref pVar3.vTransform);
                    pVar3.flags |= 0x10;
                    GameManager.instance.terrain.FUN_45B00(screen.x, screen.z, 0x20000, maxHalfHealth << 8);
                    uVar7 = FUN_42638(hit, 37, 66);
                    return uVar7;
                }

                LevelManager.instance.FUN_4E128(screen, 79, 200);
                //sound
            }
            else
            {
                if (sVar1 != 3)
                    return 0;

                LevelManager.instance.FUN_4DE54(screen, 141);
                LevelManager.instance.FUN_4DE54(screen, 42);
                //sound
                oVar3 = hit.self;

                if (oVar3.type == 2)
                {
                    vVar3 = (Vehicle)oVar3;
                    vVar3.physics1.Y = 0x11e180;
                    iVar5 = 0;

                    do
                    {
                        if (vVar3.wheels[iVar5] != null)
                            vVar3.FUN_3968C(vVar3.wheels[iVar5]);

                        iVar5++;
                    } while (iVar5 < 6);
                }
            }
        }

        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_45D84
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        long lVar2;
        int iVar3;
        VigObject oVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;
        int iVar6;
        int iVar8;
        int iVar9;
        uint uVar10;
        uint uVar11;
        
        if (arg1 < 4)
        {
            if (arg1 != 0)
                return 0;

            if (physics2.M2 < 20)
            {
                oVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)DAT_1A, typeof(VigObject), 8);
                oVar3.vTransform.rotation.SetValue32(0, 0x1000);
                oVar3.vTransform.rotation.SetValue32(1, 0);
                oVar3.vTransform.rotation.SetValue32(2, 0x1000);
                oVar3.vTransform.rotation.SetValue32(3, 0);
                oVar3.vTransform.rotation.SetValue32(4, 0x1000);
                oVar3.vTransform.position = new Vector3Int(0, 0, 0);
                oVar3.flags |= 0x410;
                //animation
                Utilities.FUN_2CC9C(this, oVar3);
                physics2.M2++;
            }

            oVar3 = child2;
            oVar4 = child2.child;

            while (oVar4 != null)
            {
                oVar3.vTransform.position.x -= physics1.Z;
                oVar3.vTransform.position.y -= physics1.W;
                oVar3.vTransform.position.z -= physics2.X;
                oVar3 = oVar4;
                oVar4 = oVar4.child;
            }

            oVar3.vTransform.position.x = -physics1.Z;
            oVar3.vTransform.position.y = -physics1.W;
            oVar3.vTransform.position.z = -physics2.X;
            screen.x += physics1.Z;
            screen.y += physics1.W;
            screen.z += physics2.X;
            vTransform.position = screen;

            if ((uint)physics1.W + 56 < 56)
            {
                //sound
                id = 0;
                flags &= 0xffffffdf;
            }

            oVar3 = DAT_84;
            physics1.W += 56;

            if (oVar3 != null)
            {
                iVar4 = oVar3.screen.x;
                iVar8 = screen.x;
                iVar5 = oVar3.screen.z;
                iVar9 = screen.z;
                lVar2 = (long)physics1.W * physics1.W;
                uVar10 = (uint)(oVar3.screen.y - screen.y) * 112;
                uVar11 = (uint)lVar2 + uVar10;
                iVar3 = (int)((ulong)lVar2 >> 32) + ((int)uVar10 >> 31) + (uVar11 < uVar10 ? 1 : 0);

                if (0 < iVar3 || (iVar3 == 0 && uVar11 != 0))
                {
                    iVar6 = Utilities.FUN_2ABC4(uVar11, iVar3);
                    iVar6 -= physics1.W;
                    iVar3 = iVar6;

                    if (iVar6 < 0)
                        iVar3 = -iVar6;

                    if (0x100 < iVar3)
                    {
                        iVar3 = ((iVar4 - iVar8) * 56) / iVar6 - physics1.Z;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics1.Z += iVar3 >> 5;
                        iVar3 = ((iVar5 - iVar9) * 56) / iVar6 - physics2.X;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics2.X += iVar3 >> 5;
                    }
                }
            }

            FUN_2D1DC();
            iVar3 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

            if (screen.y <= iVar3)
                return 0;

            sVar1 = tags;
            iVar3 = screen.x;

            if (sVar1 == 2)
            {
                //...
            }
            else
            {
                if (sVar1 < 3)
                {
                    if (sVar1 < 0)
                        return 0;
                }
                else
                {
                    if (sVar1 != 3)
                        return 0;
                }

                LevelManager.instance.FUN_4DE54(screen, 39);
                //sound
            }
            
            LevelManager.instance.FUN_309C8(this, 1);
            return 0xffffffff;
        }
        
        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 10)
            return 0;

        if (DAT_84 == arg2)
        {
            DAT_84 = DAT_80;
            return 0;
        }

        return 0;
    }
}
