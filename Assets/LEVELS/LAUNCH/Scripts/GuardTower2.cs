using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _GUARDTOWER2_TYPE
{
    Default, 
    Type1 //FUN_2FEC (LAUNCH.DLL)
}

public class GuardTower2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _GUARDTOWER2_TYPE state;

    //FUN_2FEC (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        sbyte sVar2;
        VigTransform puVar3;
        int iVar4;
        ConfigContainer ccVar4;
        Ballistic puVar5;
        Bullet puVar6;
        VigObject piVar7;
        ushort uVar8;
        int iVar9;
        VigObject oVar9;
        int iVar10;
        VigTransform tVar10;
        int iVar11;
        List<VigTuple> ppiVar15;
        VigTuple ppiVar16;
        Vehicle vVar17;
        int iVar18;
        VigObject piVar19;
        Vector2Int v2Var20;
        Vector3Int local_58;
        Vector3Int local_48;
        Vector3Int local_40;
        Vector3Int local_38;

        if (arg1 == 0)
        {
            if (2 < tags)
                return 0;

            if (tags < 1)
                return 0;

            vVar17 = PDAT_78 as Vehicle;
            puVar3 = GameManager.instance.FUN_2CDF4(this);
            local_58 = new Vector3Int();
            local_38 = new Vector3Int();
            local_58.x = vVar17.screen.x - puVar3.position.x;
            local_58.y = vVar17.screen.y - puVar3.position.y;
            local_58.z = vVar17.screen.z - puVar3.position.z;
            local_38.x = puVar3.rotation.V02;
            local_38.y = puVar3.rotation.V12;
            local_38.z = puVar3.rotation.V22;
            Utilities.FUN_29FC8(local_58, out local_40);
            local_48 = Utilities.FUN_2A1E0(local_38, local_40);
            iVar4 = local_48.y;

            if (iVar4 < 0)
                iVar4 += 3;

            iVar11 = screen.y;
            iVar18 = (iVar4 >> 2) - iVar11;

            if ((iVar18 ^ iVar11) < 0)
            {
                iVar4 = iVar11 + 2;

                if (iVar18 < 1)
                    iVar4 = iVar11 - 2;
            }
            else
            {
                iVar4 = iVar11 + 1;

                if (iVar18 < 1)
                    iVar4 = iVar11 - 1;
            }

            screen.y = iVar4;
            iVar9 = screen.y;
            iVar4 = -4;

            if (-5 < iVar9)
            {
                iVar4 = 4;

                if (iVar9 < 5)
                    iVar4 = iVar9;
            }

            screen.y = iVar4;
            vr.y = (short)((((ushort)vr.y + (ushort)screen.y) * 16) >> 4);
            local_48 = Utilities.FUN_24238(puVar3.rotation, local_48);
            iVar4 = local_48.x;

            if (iVar4 < 0)
                iVar4 += 3;

            iVar11 = screen.x;
            iVar18 = (iVar4 >> 2) - iVar11;

            if ((iVar18 ^ iVar11) < 0)
            {
                iVar4 = iVar11 + 2;

                if (iVar18 < 1)
                    iVar4 = iVar11 - 2;
            }
            else
            {
                iVar4 = iVar11 + 1;

                if (iVar18 < 1)
                    iVar4 = iVar11 - 1;
            }

            screen.x = iVar4;
            iVar9 = screen.x;
            iVar4 = -16;

            if (-17 < iVar9)
            {
                iVar4 = 16;

                if (iVar9 < 17)
                    iVar4 = iVar9;
            }

            screen.x = iVar4;
            iVar9 = (short)((((ushort)vr.x + (ushort)screen.x) * 0x100000) >> 20);
            iVar4 = -0x200;

            if (-0x201 < iVar9)
            {
                iVar4 = 0x200;

                if (iVar9 < 0x201)
                    iVar4 = iVar9;
            }

            vr.x = iVar4;
            ApplyRotationMatrix();

            if (tags == 1)
            {
                iVar4 = Utilities.FUN_29E24(local_48);

                if (31 < iVar4)
                    return 0;

                GameManager.instance.FUN_30CB0(this, 120);
                DAT_19 = 8;
                tags = 2;
                return 0;
            }

            if (tags != 2)
                return 0;

            sVar2 = (sbyte)(DAT_19 - 1);
            DAT_19 = (byte)sVar2;

            if (sVar2 != 0)
                return 0;

            puVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(2, typeof(Ballistic), 8) as Ballistic;
            puVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(210, typeof(Bullet), 8) as Bullet;
            ccVar4 = FUN_2C5F4(0x8000);
            tVar10 = Utilities.FUN_2C77C(ccVar4);
            puVar6.vTransform = Utilities.CompMatrixLV(puVar3, tVar10);
            puVar6.DAT_80 = this;
            oVar9 = Utilities.FUN_2CD78(this);
            uVar8 = 7;
            sVar1 = oVar9.id;
            puVar6.type = 8;
            puVar6.id = sVar1;
            puVar6.vTransform = puVar3;
            puVar6.screen = puVar6.vTransform.position;
            puVar6.flags = 0x280;

            if (vVar17.doubleDamage != 0)
                uVar8 = 14;

            puVar6.maxHalfHealth = uVar8;

            if (puVar5 != null)
            {
                puVar5.vTransform = GameManager.FUN_2A39C();
                puVar5.vTransform.position = ccVar4.v3_1;
                Utilities.FUN_2CC48(this, puVar5);
                Utilities.ParentChildren(this, this);
            }

            puVar6.physics1.Z = puVar6.vTransform.rotation.V02 << 3;
            puVar6.physics1.W = puVar6.vTransform.rotation.V12 << 3;
            puVar6.physics2.X = puVar6.vTransform.rotation.V22 << 3;
            puVar6.physics2.M2 = 90;
            puVar6.FUN_305FC();
            puVar5.FUN_30BF0();
            iVar10 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar10, GameManager.instance.DAT_C2C, 41, puVar6.screen);
            DAT_19 = 8;
            return 0;
        }

        if (arg1 != 2)
            return 0;

        sVar2 = tags;

        if (sVar2 == 1)
            FUN_30BA8();
        else
        {
            if (sVar2 < 2)
            {
                if (sVar2 != 0)
                    return 0;

                local_58 = GameManager.instance.FUN_2CE50(this);
                iVar18 = 0x900000;
                piVar19 = null;

                if (GameManager.instance.worldObjs != null)
                {
                    ppiVar15 = GameManager.instance.worldObjs;
                    
                    for (int i = 0; i < ppiVar15.Count; i++)
                    {
                        ppiVar16 = ppiVar15[i];
                        piVar7 = ppiVar16.vObject;

                        if (piVar7.type == 2 && piVar7.maxHalfHealth != 0)
                        {
                            local_48 = new Vector3Int();
                            local_48.x = piVar7.screen.x - local_58.x;
                            local_48.y = piVar7.screen.y - local_58.y;
                            local_48.z = piVar7.screen.z - local_58.z;
                            v2Var20 = Utilities.FUN_2A1C0(local_48);
                            iVar11 = (int)((uint)v2Var20.x >> 16 | (uint)v2Var20.y << 16);

                            if (iVar11 < iVar18)
                            {
                                piVar19 = ppiVar16.vObject;
                                iVar18 = iVar11;
                            }
                        }
                    }
                }

                if (piVar19 == null)
                    iVar10 = 8;
                else
                {
                    tags = 1;
                    screen = new Vector3Int(0, 0, 0);
                    PDAT_78 = piVar19;
                    FUN_30B78();
                    iVar10 = 600;
                }

                goto LAB_35E8;
            }

            if (sVar2 == 2)
            {
                tags = 3;
                FUN_30BA8();
                iVar10 = 90;
                goto LAB_35E8;
            }

            if (sVar2 != 3)
                return 0;
        }

        tags = 0;
        iVar10 = 60;
        LAB_35E8:
        GameManager.instance.FUN_30CB0(this, iVar10);
        return 0;
    }
}
