using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _MISSILE_TYPE
{
    Shell, //FUN_441D4
    Projectile,  //FUN_438F4
    Halo //FUN_43E84
}

public class Missile : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _MISSILE_TYPE state;

    public override uint OnCollision(HitDetection hit)
    {
        uint uVar2;
        Particle2 pVar2;
        int iVar3;
        uint uVar3;

        switch (state)
        {
            case _MISSILE_TYPE.Shell:
                uVar2 = FUN_42638(hit, 37, 64);
                return uVar2;
            case _MISSILE_TYPE.Projectile:
                if ((GameManager.instance.DAT_40 & 0x400) == 0)
                {
                    uVar3 = FUN_42638(hit, 37, 64);
                    return uVar3;
                }

                pVar2 = LevelManager.instance.FUN_4E128(screen, 79, 40);
                pVar2.vr = new Vector3Int(0x800, 0, 0);
                pVar2.ApplyTransformation();
                pVar2.vTransform.rotation = Utilities.MulMatrix(vTransform.rotation, pVar2.vTransform.rotation); //MulMatrix2 ?
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 63, screen);
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            case _MISSILE_TYPE.Halo:
                if (hit.self.type != 8)
                {
                    uVar3 = FUN_42638(hit, 37, 64);
                    return uVar3;
                }

                return 0;
        }

        return 0;
    }

    //FUN_441D4
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        Particle1 e1Var2;
        Particle2 e2Var2;
        short sVar3;
        int iVar3;
        int iVar4;
        long lVar4;
        VigObject oVar4;
        Particle1 pVar4;
        int iVar5;
        long lVar6;
        Vector3Int local_18;

        switch (state)
        {
            case _MISSILE_TYPE.Shell:
                uint uVar2;

                if (arg1 < 4)
                {
                    if (arg1 != 0)
                        return 0;

                    physics1.W += 56;
                    screen.x += physics1.Z;
                    screen.y += physics1.W;
                    screen.z += physics2.X;
                    vTransform.position = screen;

                    if ((flags & 0x1000000) != 0)
                    {
                        physics1.Z = physics1.Z * 3968 >> 12;
                        physics1.W = physics1.W * 3968 >> 12;
                        physics2.X = physics2.X * 3968 >> 12;
                    }
                }
                else
                {
                    if (arg1 == 4)
                        DAT_84.flags &= 0xf7ffffff;
                }

                uVar2 = 0;
                return uVar2;
            case _MISSILE_TYPE.Projectile:
                if (3 < arg1)
                {
                    if (arg1 == 4)
                    {
                        if ((flags & 0x1000000) != 0)
                            DAT_84.flags &= 0xf7ffffff;
                    }

                    return 0;
                }

                if (arg1 != 0)
                    return 0;

                iVar2 = vTransform.rotation.V02 * 0x3b9a;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar2 = (iVar2 >> 12) - physics1.Z;

                if (iVar2 < 0)
                    iVar2 += 15;

                iVar2 >>= 4;
                iVar5 = -0x100;

                if (-0x101 < iVar2)
                {
                    iVar5 = 0x100;

                    if (iVar2 < 0x101)
                        iVar5 = iVar2;
                }

                iVar2 = vTransform.rotation.V12 * 0x3b9a;
                physics1.Z += iVar5;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar2 = (iVar2 >> 12) - physics1.W;

                if (iVar2 < 0)
                    iVar2 += 7;

                physics1.W += iVar2 >> 3;
                iVar2 = vTransform.rotation.V22 * 0x3b9a;

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar2 = (iVar2 >> 12) - physics2.X;

                if (iVar2 < 0)
                    iVar2 += 15;

                iVar2 >>= 4;
                iVar5 = -0x100;

                if (-0x101 < iVar2)
                {
                    iVar5 = 0x100;

                    if (iVar2 < 0x101)
                        iVar5 = iVar2;
                }

                physics2.X += iVar5;
                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                iVar2 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                if (iVar2 < screen.y)
                {
                    if ((GameManager.instance.DAT_40 & 0x400) == 0)
                    {
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 64, screen);
                        LevelManager.instance.FUN_4DE54(screen, 33);
                    }
                    else
                    {
                        e2Var2 = LevelManager.instance.FUN_4E128(screen, 79, 40);
                        e2Var2.FUN_2D114(e2Var2.screen, ref e2Var2.vTransform);
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 63, screen);
                    }

                    LevelManager.instance.FUN_309C8(this, 1);
                    return 0xffffffff;
                }

                vTransform.position = screen;
                local_18 = new Vector3Int();
                local_18.x = DAT_84.screen.x - screen.x;
                local_18.y = (DAT_84.screen.y - screen.y) +
                              DAT_84.vCollider.reader.ReadInt32(8) / 2;
                local_18.z = DAT_84.screen.z - screen.z;
                iVar2 = GameManager.instance.terrain.FUN_1B750((uint)(screen.x + physics1.Z * 16),
                                                               (uint)(screen.z + physics2.X * 16));
                iVar2 -= screen.y + 0x5000;

                if (iVar2 < local_18.y)
                    local_18.y = iVar2;

                local_18 = Utilities.FUN_2426C(vTransform.rotation, 
                    new Matrix2x4(local_18.x, local_18.y, local_18.z, 0));
                lVar4 = Utilities.Ratan2(-local_18.y, local_18.z);
                lVar6 = -0x100;

                if (-0x101 < lVar4)
                {
                    lVar6 = 0x100;

                    if (lVar4 < 0x101)
                        lVar6 = lVar4;
                }

                sVar3 = -0x100;

                if (0 < local_18.x)
                    sVar3 = 0x100;

                FUN_24700((short)lVar6, sVar3, 0);

                if ((GameManager.instance.DAT_28 - DAT_19 & 31) == 0)
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);

                if (GameManager.instance.DAT_36 && ((ushort)physics2.M2 & 3) == 0)
                {
                    e1Var2 = LevelManager.instance.FUN_4DE54(screen, 7);
                    e1Var2.flags |= 0x400;
                    e1Var2.vr.z = physics2.M2 * 96;
                    e1Var2.ApplyTransformation();
                }

                sVar1 = physics2.M2;
                physics2.M2--;

                if (sVar1 != 1)
                    return 0;

                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 64, screen);
                LevelManager.instance.FUN_4DE54(screen, 37);
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;

            case _MISSILE_TYPE.Halo:
                if (arg1 < 4)
                {
                    if (arg1 != 0)
                        return 0;

                    iVar2 = vTransform.rotation.V02 * 0x3b9a;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    iVar2 = (iVar2 >> 12) - physics1.Z;

                    if (iVar2 < 0)
                        iVar2 += 15;

                    iVar4 = 0x100;

                    if (iVar2 >> 4 < 0x100)
                        iVar4 = iVar2 >> 4;

                    iVar2 = -0x100;

                    if (-0x100 < iVar4)
                        iVar2 = iVar4;

                    iVar2 = physics1.Z + iVar2;
                    physics1.Z = iVar2;
                    iVar4 = vTransform.rotation.V22 * 0x3b9a;
                    screen.x += iVar2;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    iVar2 = (iVar4 >> 12) - physics2.X;

                    if (iVar2 < 0)
                        iVar2 += 15;

                    iVar4 = 0x100;

                    if (iVar2 >> 4 < 0x100)
                        iVar4 = iVar2 >> 4;

                    iVar2 = -0x100;

                    if (-0x100 < iVar4)
                        iVar2 = iVar4;

                    iVar2 = physics2.X + iVar2;
                    physics2.X = iVar2;
                    screen.z += iVar2;
                    iVar2 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                    if (iVar2 < screen.y)
                    {
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 64, screen);
                        LevelManager.instance.FUN_4DE54(screen, 33);
                        LevelManager.instance.FUN_309C8(this, 1);
                        return 0xffffffff;
                    }

                    vTransform.position = screen;
                    FUN_24700(0, 0x100, 0);

                    if ((-DAT_19 & 31) == 0)
                        vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);

                    if ((physics2.M2 & 3) == 0)
                    {
                        pVar4 = LevelManager.instance.FUN_4DE54(screen, 21);
                        pVar4.flags |= 0x400;
                        pVar4.vr.z = physics2.M2 * 96;
                        pVar4.ApplyTransformation();
                    }

                    oVar4 = DAT_84;

                    if ((oVar4.flags & 0x4000000) == 0)
                        oVar4.screen = screen;

                    sVar1 = physics2.M2;
                    physics2.M2--;

                    if (sVar1 == 1)
                    {
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 64, screen);
                        LevelManager.instance.FUN_4DE54(screen, 37);
                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }
                }
                else
                {
                    if (arg1 != 4)
                        return 0;

                    if ((flags & 0x1000000) != 0)
                        DAT_84.flags &= 0xf7ffffff;
                }
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;
        uint uVar2;

        switch (state)
        {
            case _MISSILE_TYPE.Shell:
                if (arg1 != 5)
                    return 0;

                GameManager.instance.FUN_1FEB8(vMesh);
                GameManager.instance.FUN_2C4B4(child2);
                uVar2 = 186;

                if ((flags & 0x1000000) != 0)
                    uVar2 = 198;

                FUN_2C344(vData, (ushort)uVar2, 8);
                uVar1 = flags;
                flags |= 0x84;

                if ((uVar1 & 0x1000000) == 0)
                    state = _MISSILE_TYPE.Projectile;
                else
                {
                    state = _MISSILE_TYPE.Halo;
                    DAT_84.flags |= 0x8000000;
                }

                physics2.M2 = 480;
                uVar2 = 0;
                return uVar2;

            case _MISSILE_TYPE.Projectile:
                if (arg1 != 10)
                    return 0;

                if (DAT_84 != arg2)
                    return 0;

                DAT_84 = DAT_80;
                break;
        }

        return 0;
    }
}
