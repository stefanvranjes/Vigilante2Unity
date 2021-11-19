using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenSteel : VigObject
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
        byte bVar1;
        int iVar7;
        VigObject oVar8;
        Vehicle vVar8;

        oVar8 = hit.self;
        bVar1 = oVar8.type;

        if (bVar1 == 2)
        {
            vVar8 = (Vehicle)oVar8;
            vVar8.physics1.Y -= 0x5f580;
            flags |= 0x20;
            LevelManager.instance.FUN_39AF8(vVar8);
            UIManager.instance.FUN_4E414(vVar8.vTransform.position, new Color32(0x80, 0x00, 0x00, 8));
            LevelManager.instance.FUN_4E8C8(vVar8.vTransform.position, 48);
        }
        else
        {
            if (bVar1 == 3)
                return 0;

            if (bVar1 == 8)
                return 0;
        }

        iVar7 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar7, GameManager.instance.DAT_C2C, 69, oVar8.vTransform.position);
        FUN_14D0();
        return 0xffffffff;
    }

    //FUN_151C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar3;
        List<VigTuple> ppiVar4;
        short sVar5;
        int iVar6;
        int iVar8;
        VigObject oVar8;
        int iVar9;
        VigObject oVar9;
        int iVar10;
        VigObject oVar10;
        Vector3Int local_20;

        switch (arg1)
        {
            case 0:
                if (SteelMil.instance.DAT_4600 < 4)
                {
                    iVar8 = physics1.Y;
                    physics1.Y = iVar8 + 1;

                    if (iVar8 < 0x2a31)
                    {
                        if (tags == 0)
                        {
                            iVar10 = (vr.y & 0xfff) * 2;
                            iVar8 = GameManager.DAT_65C90[iVar10] * 457;

                            if (iVar8 < 0)
                                iVar8 += 4095;

                            iVar10 = GameManager.DAT_65C90[iVar10 + 1] * 457;
                            iVar8 = vTransform.position.x + (iVar8 >> 12);

                            if (iVar10 < 0)
                                iVar10 += 4095;

                            iVar9 = vTransform.position.z;
                            iVar10 >>= 12;
                        }
                        else
                        {
                            if (tags != 1)
                                return 0;

                            iVar9 = vTransform.position.z;
                            iVar10 = physics1.W;
                            iVar8 = vTransform.position.x + physics1.Y;
                        }

                        GameManager.instance.terrain.FUN_2D16C(iVar8, iVar9 + iVar10, ref vTransform);

                        if (2894 < vTransform.rotation.V11)
                            return 0;
                    }
                }

                FUN_14D0();
                return 0xffffffff;
            case 1:
                sVar5 = (short)GameManager.FUN_2AC5C();
                vr.y = sVar5;
                SteelMil.instance.DAT_4600++;
                flags |= 0x80;
                break;
            case 2:
                local_20 = new Vector3Int();

                if (tags == 0)
                {
                    oVar8 = null;
                    iVar10 = 0xa0000;
                    ppiVar4 = GameManager.instance.worldObjs;

                    for (int i = 0; i < ppiVar4.Count; i++)
                    {
                        ppiVar3 = ppiVar4[i];
                        oVar9 = ppiVar3.vObject;

                        if (oVar9.type == 2 && oVar9.maxHalfHealth != 0)
                        {
                            iVar6 = Utilities.FUN_29F6C(oVar9.vTransform.position, vTransform.position);

                            if (iVar6 < iVar10)
                            {
                                iVar10 = iVar6;
                                oVar8 = oVar9;
                            }
                        }
                    }

                    if (oVar8 == null)
                    {
                        iVar8 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, (iVar8 * 45 >> 15) + 75);
                        return 0;
                    }

                    DAT_80 = oVar8;
                    tags++;
                    local_20.x = oVar8.vTransform.position.x - vTransform.position.x;
                    local_20.z = oVar8.vTransform.position.z;
                }
                else
                {
                    if (tags != 1)
                        return 0;

                    oVar10 = DAT_80;
                    iVar8 = Utilities.FUN_29F6C(oVar10.vTransform.position, vTransform.position);

                    if (0x9ffff < iVar8)
                    {
                        tags = 0;
                        DAT_80 = null;
                        iVar8 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, (iVar8 * 45 >> 15) + 75);
                        return 0;
                    }

                    local_20.x = oVar10.vTransform.position.x - vTransform.position.x;
                    local_20.z = oVar10.vTransform.position.z;
                }

                local_20.y = 0;
                local_20.z -= vTransform.position.z;
                Utilities.FUN_2A098(local_20, out local_20);
                iVar8 = local_20.x * 915;

                if (iVar8 < 0)
                    iVar8 += 4095;

                physics1.Y = iVar8 >> 12;
                physics1.Z = 0;
                iVar8 = local_20.z * 915;

                if (iVar8 < 0)
                    iVar8 += 4095;

                physics1.W = iVar8 >> 12;
                iVar8 = (int)GameManager.FUN_2AC5C();
                GameManager.instance.FUN_30CB0(this, (iVar8 * 45 >> 15) + 60);
                return 0;
            case 4:
                SteelMil.instance.DAT_4600--;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
        }

        return 0;
    }

    //FUN_14D0 (STEELMIL.DLL)
    private void FUN_14D0()
    {
        LevelManager.instance.FUN_4DE54(vTransform.position, 33);
        LevelManager.instance.FUN_4DE54(vTransform.position, 29);
        GameManager.instance.FUN_309A0(this);
    }
}
