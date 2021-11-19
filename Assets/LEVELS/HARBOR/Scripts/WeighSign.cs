using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeighSign : Destructible
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
        VigObject oVar1;
        BufferedBinaryReader brVar2;
        uint uVar3;
        VigObject oVar6;

        if (tags != 0 || hit.self.type != 2)
        {
            if (hit.self.type != 8)
                return 0;

            if (hit.collider1.ReadUInt16(0) != 1 || hit.collider1.ReadUInt16(2) == 0)
            {
                
                oVar1 = hit.self;
                type = 0;
                brVar2 = hit.collider2;
                oVar6 = hit.object2;
                hit.self = this;
                hit.collider2 = hit.collider1;
                hit.collider1 = brVar2;
                hit.object2 = hit.object1;
                hit.object1 = oVar6;
                uVar3 = oVar1.OnCollision(hit);
                type = 3;
                FUN_32B90(oVar1.maxHalfHealth);
                return uVar3;
            }

            return 0;
        }
        
        if (DAT_80_2 == null)
        {
            DAT_84_2 = 30;
            DAT_80_2 = (Vehicle)hit.self;
            FUN_30B78();
        }
        else
        {
            if (DAT_80_2 != hit.self)
                return 0;
        }

        DAT_88 = GameManager.instance.DAT_28;
        return 0;
    }

    public Vehicle DAT_80_2; //0x80
    public int DAT_84_2; //0x84
    public int DAT_88; //0x88

    //FUN_6B8 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        uint uVar1;
        int iVar2;
        int iVar3;
        Vehicle vVar3;

        switch (arg1)
        {
            case 0:
                iVar3 = DAT_88 - GameManager.instance.DAT_28;

                if (iVar3 < 0)
                    iVar3 = -iVar3;

                if (iVar3 < 4 && tags != 1)
                {
                    iVar3 = DAT_84_2 + 1;
                    DAT_84_2 = iVar3;

                    if (iVar3 < 31)
                        return 0;

                    vVar3 = DAT_80_2;
                    DAT_84_2 = 0;

                    if (vVar3.body[0] == null)
                        iVar2 = vVar3.maxHalfHealth;
                    else
                        iVar2 = vVar3.body[0].maxHalfHealth +
                                vVar3.body[1].maxHalfHealth;

                    if (vVar3.maxFullHealth <= iVar2)
                        return 0;

                    vVar3.FUN_3A0C0(60);
                    iVar1 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar1, GameManager.instance.DAT_C2C, 44, vTransform.position);
                    return 0;
                }

                DAT_80_2 = null;
                goto LAB_8B0;
            case 1:
                tags = 1;
                type = 3;
                flags &= 0xfffffffb;
                GameManager.instance.FUN_30CB0(this, 600);
                FUN_2C01C();
                child2.FUN_2C01C();
                child2.child.FUN_2C01C();
                GameManager.instance.FUN_2FEE8(this, 0);
                uVar1 = 0;
                break;
            case 2:
                if (tags == 0)
                {
                    tags = 1;
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, (iVar3 * 600 >> 15) + 600);
                    GameManager.instance.FUN_2C0A0(this);
                    GameManager.instance.FUN_2FEE8(this, DAT_4A);
                    uVar1 = 0;
                }
                else
                {
                    uVar1 = 0;

                    if (tags == 1)
                    {
                        iVar1 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar1, vData.sndList, 4, vTransform.position);
                        tags = 0;
                        GameManager.instance.FUN_30CB0(this, 600);
                        GameManager.instance.FUN_2FEE8(this, (ushort)(DAT_4A + vAnim.ReadUInt16(0)));
                        uVar1 = 0;
                    }
                }

                break;
            default:
                uVar1 = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                uVar1 = 0;
                break;
            case 9:
                if (arg2 != 0)
                    return 0;

                tags = 2;
                LAB_8B0:
                FUN_30BA8();
                uVar1 = 0;
                break;
        }

        return uVar1;
    }
}
