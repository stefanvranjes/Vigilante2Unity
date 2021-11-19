using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glacier : Destructible
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
        Vehicle vVar3;
        int iVar4;
        bool bVar7;
        Vector3Int local_18;

        if (hit.self.type == 2 && tags == 1)
        {
            vVar3 = (Vehicle)hit.self;
            iVar4 = (16 << 11) / vVar3.DAT_A6;
            iVar1 = physics1.X * iVar4;
            local_18 = new Vector3Int();

            if (iVar1 < -0x80000)
                local_18.x = -0x80000;
            else
            {
                local_18.x = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.x = iVar1;
            }

            iVar1 = physics1.Y * iVar4;
            local_18.y = -0x80000;

            if (-0x80001 < iVar1)
            {
                local_18.y = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.y = iVar1;
            }

            iVar1 = physics1.Z * iVar4;
            local_18.z = -0x80000;

            if (-0x80001 < iVar1)
            {
                local_18.z = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.z = iVar1;
            }

            vVar3.FUN_2B370(local_18, vTransform.position);

            if (vVar3.id < 0)
                GameManager.instance.FUN_15B00(~vVar3.id, 255, 2, 128);

            vVar3.FUN_3A064(-180, vTransform.position, true);
        }
        else
        {
            if (hit.self.type == 4 && hit.self.GetType() == typeof(Orca))
            {
                FUN_4DC94();
                return 0xfffffffe;
            }
        }

        bVar7 = FUN_32CF0(hit);
        
        if (bVar7)
            return 0xfffffffe;

        return 0;
    }

    //0xF8 (OILFIELD.DLL)
    public static Vector3Int DAT_F8 = new Vector3Int(0, 0, 0);
    //0x29D0 (OILFIELD.DLL)
    public static int[] DAT_29D0 = new int[] { 20, 21, 19 };

    public Vector3Int DAT_A8; //0xA8
    public int DAT_B4; //0xB4
    public int DAT_B8; //0xB8
    public int DAT_BC; //0xBC

    //FUN_1D34 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        uint uVar2;
        int iVar3;
        ConfigContainer ccVar3;
        GlacierSmall ppcVar4;
        int iVar5;
        bool bVar7;
        int iVar7;
        Oilfield lVar7;
        VigObject oVar7;

        switch (arg1)
        {
            case 0:
                iVar7 = physics1.X;

                if (iVar7 < 0)
                    iVar7 += 127;

                iVar5 = physics1.Z;
                vTransform.position.x += iVar7 >> 7;

                if (iVar5 < 0)
                    iVar5 += 127;

                iVar7 = DAT_BC + DAT_B8;
                DAT_BC = iVar7;
                vTransform.position.z += iVar5 >> 7;
                vr.y = iVar7 >> 16;
                ApplyRotationMatrix();
                iVar7 = vTransform.position.x - DAT_A8.x;

                if (iVar7 < 0)
                    iVar7 = -iVar7;

                if (0xffff < iVar7)
                    return 0;

                iVar7 = vTransform.position.z - DAT_A8.z;

                if (iVar7 < 0)
                    iVar7 = -iVar7;

                if (0xffff < iVar7)
                    return 0;

                if ((sbyte)DAT_19 == -95)
                    uVar2 = flags & 0xfeffffff;
                else
                {
                    if ((sbyte)DAT_19 != -90) goto LAB_1E80;

                    uVar2 = flags | 0x1000000;
                }

                flags = uVar2;

                LAB_1E80:
                if ((flags & 0x1000000) == 0)
                    sVar1 = (sbyte)(DAT_19 + 1);
                else
                    sVar1 = (sbyte)(DAT_19 - 1);

                DAT_19 = (byte)sVar1;
                oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, DAT_19);
                DAT_A8 = oVar7.screen;
                iVar7 = oVar7.vr.y * 0x10000;
                iVar5 = iVar7 - DAT_BC;
                DAT_B4 = iVar7;
                DAT_B8 = iVar5;

                if (iVar5 < 0)
                {
                    DAT_B8 = iVar5 + 0x10000000;
                    DAT_BC -= 0x10000000;
                }

                DAT_B8 /= 600;
                physics1.X = ((DAT_A8.x - vTransform.position.x) * 128) / 600;
                physics1.Z = ((DAT_A8.z - vTransform.position.z) * 128) / 600;
                FUN_30BA8();
                tags = 0;
                GameManager.instance.FUN_30CB0(this, 600);
                return 0;
            case 1:
                type = 4;
                flags |= 0x10000;
                DAT_19 = (byte)id;
                DAT_A8 = screen;
                iVar3 = GameManager.instance.DAT_DB0;
                DAT_BC = vr.y << 16;
                vTransform.position.y = iVar3;
                break;
            case 2:
                tags = 1;
                FUN_30B78();
                return 0;
            case 8:
                bVar7 = FUN_32B90((uint)arg2);

                if (bVar7)
                    return 0xfffffffe;

                break;
            case 9:
                iVar7 = 0;

                if (arg2 != 0)
                {
                    do
                    {
                        ccVar3 = FUN_2C5F4((ushort)(iVar7 - 0x8000 & 0xffff));
                        ppcVar4 = vData.ini.FUN_2C17C((ushort)DAT_29D0[iVar7], typeof(GlacierSmall), 8) as GlacierSmall;
                        ppcVar4.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar3);
                        ppcVar4.flags |= 0x80;
                        ppcVar4.physics1.X = (ppcVar4.vTransform.position.x - vTransform.position.x) * 2;
                        iVar7++;
                        ppcVar4.physics1.Z = (ppcVar4.vTransform.position.z - vTransform.position.z) * 2;
                        iVar5 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(ppcVar4, (iVar5 * 420 >> 15) + 480);
                        ppcVar4.FUN_305FC();
                    } while (iVar7 < 3);

                    LevelManager.instance.FUN_4AAC0(0xfe780000, vTransform.position, DAT_F8);
                    lVar7 = (Oilfield)LevelManager.instance.level;
                    iVar5 = lVar7.DAT_A0_2 + 1;
                    lVar7.DAT_9C--;
                    lVar7.DAT_A0_2 = iVar5;

                    if (iVar5 == 3)
                    {
                        //FUN_326B0
                    }

                    GameManager.instance.FUN_309A0(this);
                    return 0;
                }

                break;
        }

        return 0;
    }
}
