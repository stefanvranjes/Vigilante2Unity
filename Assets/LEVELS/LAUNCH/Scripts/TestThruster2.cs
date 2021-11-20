using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThruster2 : VigObject
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
        int iVar1;
        uint uVar1;
        int iVar2;
        VigObject oVar2;
        Vehicle vVar2;
        int iVar3;
        VigObject ppcVar7;
        VigObject pcVar8;
        Vector3Int local_78;
        Vector3Int local_60;
        Vector3Int local_50;
        HitDetection auStack64;

        ppcVar7 = hit.object1;
        oVar2 = hit.self;

        if (ppcVar7 == this)
        {
            if (oVar2.type == 2)
            {
                vVar2 = (Vehicle)oVar2;
                local_78 = new Vector3Int();
                local_60 = new Vector3Int();
                local_60.y = -0x4180;
                local_78.x = physics1.Y << 7;
                local_78.z = physics1.W << 7;
                local_78.y = -0x4180;
                local_60.x = local_78.x;
                local_60.z = local_78.z;
                auStack64 = new HitDetection(null);
                GameManager.instance.FUN_2FB70(this, hit, auStack64);
                local_50 = new Vector3Int();
                local_50.x = auStack64.position.x / 2;
                local_50.y = auStack64.position.y / 2;
                local_50.z = auStack64.position.z / 2;
                local_50 = Utilities.FUN_24148(vVar2.vTransform, local_50);
                vVar2.FUN_2B370(local_78, local_50);
                vVar2.FUN_3A064(-1, vTransform.position, true);
                LevelManager.instance.FUN_4DE54(local_50, 142);
                iVar1 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 24, local_50);
                physics1.Y = physics1.Y / 2;
                physics1.Z = physics1.Z / 2;
                physics1.W = physics1.W / 2;
                pcVar8 = FUN_6EC();
                DAT_80 = pcVar8;
            }
            else
            {
                if (oVar2.type == 3)
                    return 0;

                GameManager.instance.FUN_2F798(this, hit);
                iVar2 = physics1.Y * hit.normal1.x +
                        physics1.Z * hit.normal1.y +
                        physics1.W * hit.normal1.z;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar2 >>= 12;

                if (-1 < iVar2)
                    return 0;

                iVar3 = iVar2 * hit.normal1.x;

                if (iVar3 < 0)
                    iVar3 += 4095;

                physics1.X = physics1.X + -(iVar3 >> 12);
                iVar3 = iVar2 * hit.normal1.y;

                if (iVar3 < 0)
                    iVar3 += 4095;

                physics1.Y = physics1.Y + -(iVar3 >> 12);
                iVar2 = iVar2 * hit.normal1.z;

                if (iVar2 < 0)
                    iVar2 += 4095;

                physics1.Z = physics1.Z + -(iVar2 >> 12);
            }
        }
        else
        {
            if (ppcVar7.GetType().IsSubclassOf(typeof(VigObject)))
            {
                uVar1 = ppcVar7.OnCollision(hit);
                return uVar1;
            }
        }

        return 0;
    }

    //FUN_7DC (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        uint uVar1;
        bool bVar2;
        int iVar2;
        int iVar3;
        int iVar4;
        uint uVar4;
        int iVar5;
        int iVar6;
        int iVar8;
        VigObject pcVar8;
        Vector3Int local_98;
        Vector3Int local_88;
        Vector3Int local_80;

        if (arg1 == 2)
        {
            if (tags == 0)
            {
                FUN_30B78();
                GameManager.instance.FUN_30CB0(this, 720);
                pcVar8 = FUN_6EC();
                DAT_80 = pcVar8;
                screen = vTransform.position;
                tags = 1;
            }
            else
            {
                if (tags != 1)
                    return 0;

                if (GetType().IsSubclassOf(typeof(VigObject)))
                {
                    uVar1 = UpdateW(8, 10000);
                    return uVar1;
                }
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                if (vTransform.position.z < GameManager.instance.DAT_DA0 && 
                    GameManager.instance.DAT_DB0 < vTransform.position.y)
                {
                    LevelManager.instance.FUN_4DE54(vTransform.position, 138);
                    iVar1 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar1, GameManager.instance.DAT_C2C, 70);

                    if (GetType().IsSubclassOf(typeof(VigObject)))
                        UpdateW(8, 10000);

                    return 0xffffffff;
                }

                iVar2 = vTransform.rotation.V02 * 3814;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar5 = physics1.Y;
                iVar8 = iVar5;

                if (iVar5 < 0)
                    iVar8 = iVar5 + 7;

                physics1.Y = iVar5 + -((iVar2 >> 12) + (iVar8 >> 3));
                iVar2 = vTransform.rotation.V12 * 3814;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar5 = physics1.Z;
                iVar8 = iVar5;

                if (iVar5 < 0)
                    iVar8 = iVar5 + 7;

                physics1.Z = iVar5 + -((iVar2 >> 12) + (iVar8 >> 3));
                iVar2 = vTransform.rotation.V22 * 3814;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar5 = physics1.W;
                iVar8 = iVar5;

                if (iVar5 < 0)
                    iVar8 = iVar5 + 7;

                iVar6 = physics1.Y;
                physics1.W = iVar5 + -((iVar2 >> 12) + (iVar8 >> 3));

                if (iVar6 < -45776)
                    iVar8 = -45776;
                else
                {
                    iVar8 = 45776;

                    if (iVar6 < 45777)
                        iVar8 = iVar6;
                }

                physics1.Y = iVar8;
                iVar8 = physics1.Z;

                if (iVar8 < -45776)
                    iVar5 = -45776;
                else
                {
                    iVar5 = 45776;

                    if (iVar8 < 45777)
                        iVar5 = iVar8;
                }

                iVar8 = physics1.W;
                physics1.Z = iVar5 + 90;

                if (iVar8 < -45776)
                    iVar5 = -45776;
                else
                {
                    iVar5 = 45776;

                    if (iVar8 < 45777)
                        iVar5 = iVar8;
                }

                physics1.W = iVar5;
                screen.x += physics1.Y;
                screen.y += physics1.Z;
                screen.z += physics1.W;
                iVar2 = vTransform.rotation.V12 * 30;

                if (iVar2 < 0)
                    iVar2 = vTransform.rotation.V12 * -30;

                iVar3 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                if (iVar3 < screen.y + iVar2 + 0x14000 && -1 < physics1.Z)
                {
                    physics1.Z = -physics1.Z;

                    if (0 < physics2.X)
                        physics2.X = -physics2.X;

                    uVar4 = GameManager.FUN_2AC5C();

                    if ((uVar4 & 3) == 0)
                    {
                        pcVar8 = FUN_6EC();
                        DAT_80 = pcVar8;
                    }
                }

                vTransform.position = screen;
                local_98 = new Vector3Int();
                local_80 = new Vector3Int();
                local_98.x = screen.x - DAT_80.screen.x;
                local_98.y = screen.y + (-(DAT_80.vCollider.reader.ReadInt32(8) / 2) -
                                    DAT_80.screen.y);
                local_98.z = screen.z - DAT_80.screen.z;
                local_80.x = vTransform.rotation.V02;
                local_80.y = vTransform.rotation.V12;
                local_80.z = vTransform.rotation.V22;
                Utilities.FUN_29FC8(local_98, out local_88);
                local_88 = Utilities.FUN_2A1E0(local_80, local_88);
                iVar2 = local_88.y;

                if (iVar2 < 0)
                    iVar2 += 3;

                iVar8 = physics2.Y;
                iVar4 = (iVar2 >> 2) - screen.y;

                if ((iVar4 ^ iVar8) < 0)
                {
                    iVar5 = iVar8 + 4;

                    if (iVar4 < 1)
                        iVar5 = iVar8 - 4;
                }
                else
                {
                    iVar5 = iVar8 + 2;

                    if (iVar4 < 1)
                        iVar5 = iVar8 - 2;
                }

                physics2.Y = iVar5;
                iVar5 = physics2.Y;
                iVar8 = -64;

                if (-65 < iVar5)
                {
                    iVar8 = 64;

                    if (iVar5 < 65)
                        iVar8 = iVar5;
                }

                physics2.Y = iVar8;
                vr.y = (short)((((ushort)vr.y + (ushort)physics2.Y) * 16) >> 4);
                local_88 = Utilities.FUN_24238(vTransform.rotation, local_88);
                iVar2 = local_88.x;

                if (iVar2 < 0)
                    iVar2 += 3;

                iVar8 = physics2.X;
                iVar4 = (iVar2 >> 2) - iVar8;

                if ((iVar4 ^ iVar8) < 0 && iVar8 != 0)
                    physics2.X = 0;
                else
                {
                    if (iVar4 < 1)
                        iVar8 = physics2.X - 1;
                    else
                        iVar8 = physics2.X + 1;

                    physics2.X = iVar8;
                }

                iVar5 = physics2.X;
                iVar8 = -8;

                if (-9 < iVar5)
                {
                    iVar8 = 8;

                    if (iVar5 < 9)
                        iVar8 = iVar5;
                }

                physics2.X = iVar8;
                iVar3 = (((ushort)vr.x + (ushort)physics2.X) * 0x100000) >> 20;
                iVar2 = -0x200;

                if (-0x201 < iVar3)
                {
                    iVar2 = 64;

                    if (iVar3 < 65)
                        iVar2 = iVar3;
                }

                vr.x = (short)iVar2;
                ApplyRotationMatrix();
                return 0;
            }

            if (arg1 != 3)
            {
                if (arg1 != 8)
                    return 0;

                bVar2 = FUN_32B90((uint)arg2);

                if (!bVar2)
                    return 0;

                GameManager.instance.FUN_309A0(this);
                return 0xfffffffe;
            }
        }

        return 0;
    }

    //FUN_6EC (LAUNCH.DLL)
    private static VigObject FUN_6EC()
    {
        VigObject ppiVar1;
        List<VigTuple> ppiVar2;
        int iVar3;
        VigTuple ppiVar4;
        int iVar5;

        iVar5 = 0;
        ppiVar2 = GameManager.instance.worldObjs;
        
        for (int i = 0; i < ppiVar2.Count; i++)
        {
            ppiVar4 = ppiVar2[i];

            if (ppiVar4.vObject.type == 2 && ppiVar4.vObject.maxHalfHealth != 0)
                iVar5++;
        }

        iVar3 = (int)GameManager.FUN_2AC5C();
        iVar5 = iVar3 * iVar5 >> 15;
        ppiVar2 = GameManager.instance.worldObjs;
        ppiVar1 = null;

        for (int i = 0; i < ppiVar2.Count; i++)
        {
            ppiVar4 = ppiVar2[i];
            ppiVar1 = ppiVar4.vObject;

            if (!(ppiVar1.type != 2 || ppiVar1.maxHalfHealth == 0 || --iVar5 != -1))
                break;
        }

        return ppiVar1;
    }
}
