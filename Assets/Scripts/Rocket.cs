using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _ROCKET_TYPE
{
    Rocket, //FUN_430F8
    Bastion, //FUN_42D18
    RoadRunner //FUN_43024
}

public class Rocket : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _ROCKET_TYPE state;

    public override uint OnCollision(HitDetection hit)
    {
        uint uVar1;
        VigObject oVar2;
        uint uVar3;
        Vector3Int v0;
        Vector3Int local_10;

        switch (state)
        {
            case _ROCKET_TYPE.Rocket:
                if ((flags & 0x1000000) != 0)
                {
                    oVar2 = hit.self;

                    if (oVar2.type == 2)
                    {
                        local_10 = new Vector3Int(
                            vTransform.position.x - oVar2.vTransform.position.x,
                            vTransform.position.y - oVar2.vTransform.position.y,
                            vTransform.position.z - oVar2.vTransform.position.z);
                        Utilities.FUN_243B4(oVar2.vTransform.rotation);
                        v0 = Utilities.FUN_23F7C(new Vector3Int(physics1.Z, physics1.W, physics2.X));
                        physics1.Z = v0.x;
                        physics1.W = v0.y;
                        physics2.X = v0.z;
                        vTransform.position = Utilities.FUN_23F7C(local_10);
                        vTransform.rotation = Utilities.FUN_247F4(vTransform.rotation);
                        GameManager.instance.FUN_300B8(GameManager.instance.worldObjs, this);
                        state = _ROCKET_TYPE.RoadRunner;
                        physics2.M2 = 360;
                        Utilities.FUN_2CC48(oVar2, this);
                        transform.parent = oVar2.transform;
                        return 1;
                    }
                }

                uVar3 = FUN_42638(hit, 33, 63);
                return uVar3;

            case _ROCKET_TYPE.Bastion:
                uVar1 = FUN_42638(hit, 33, 63);
                return uVar1;
        }

        return 0;
    }

    //FUN_430F8
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        uint uVar1;
        short sVar1;
        ushort uVar2;
        int iVar2;
        Vehicle vVar2;
        int iVar3;
        VigObject oVar3;
        int iVar4;
        Matrix3x3 m;
        Vector3Int local_18;
        Vector3Int auStack8;

        switch (state)
        {
            case _ROCKET_TYPE.Rocket:
                if (arg1 == 2)
                {
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                    state = _ROCKET_TYPE.Bastion;
                }
                else
                {
                    if (arg1 != 0)
                        return 0;

                    m = vTransform.rotation;
                    screen.x += physics1.Z;
                    screen.y += physics1.W;
                    screen.z += physics2.X;
                    vTransform.position = screen;
                    FUN_24700(0, 0, 512);

                    if ((physics2.M2 & 31) == 0)
                        vTransform.rotation = Utilities.MatrixNormal(m);

                    sVar1 = physics2.M2;
                    physics2.M2--;

                    if (sVar1 == 1)
                    {
                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }

                    iVar2 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                    if (iVar2 <= screen.y)
                    {
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 63, screen);
                        LevelManager.instance.FUN_4DE54(screen, 33);
                        LevelManager.instance.FUN_309C8(this, 1);
                        return 0xffffffff;
                    }
                }

                return 0;

            case _ROCKET_TYPE.Bastion:
                if (arg1 == 0)
                {
                    screen.x += physics1.Z;
                    screen.y += physics1.W;
                    screen.z += physics2.X;
                    vTransform.position = screen;
                    oVar3 = DAT_84;

                    if (oVar3 != null)
                    {
                        local_18 = new Vector3Int(
                            oVar3.screen.x - screen.x,
                            oVar3.screen.y - screen.y,
                            oVar3.screen.z - screen.z);
                        Utilities.FUN_2A098(local_18, out local_18);

                        if (physics2.M2 < 211)
                        {
                            physics1.Z = physics1.Z / 2 + local_18.x * 4;
                            physics1.W = physics1.W / 2 + local_18.y * 4;
                            physics2.X = physics2.X / 2 + local_18.z * 4;
                        }
                        else
                        {
                            iVar3 = physics1.Z * 15;

                            if (iVar3 < 0)
                                iVar3 += 15;

                            iVar4 = physics1.W * 15;
                            physics1.Z = (iVar3 >> 4) + local_18.x / 2;

                            if (iVar4 < 0)
                                iVar4 += 15;

                            iVar3 = physics2.X * 15;
                            physics1.W = (iVar4 >> 4) + local_18.y / 2;

                            if (iVar3 < 0)
                                iVar3 += 15;

                            physics2.X = (iVar3 >> 4) + local_18.z / 2;
                        }

                        local_18 = new Vector3Int(physics1.Z, physics1.W, physics2.X);
                        Utilities.FUN_29FC8(local_18, out auStack8);
                        vTransform.rotation = Utilities.FUN_2A724(auStack8);
                    }

                    uVar2 = (ushort)(physics2.M2 - 1);
                    physics2.M2 = (short)uVar2;

                    if (uVar2 == 0 ||
                       ((uVar2 & 15) == 0 && DAT_84 != null &&
                       GameManager.instance.FUN_30134(GameManager.instance.worldObjs, DAT_84) == null))
                    {
                        GameManager.instance.FUN_309A0(this);
                        uVar1 = 0xffffffff;
                    }
                    else
                    {
                        iVar3 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                        if (screen.y < iVar3)
                            uVar1 = 0;
                        else
                        {
                            iVar1 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E628(iVar1, GameManager.instance.DAT_C2C, 63, screen);
                            LevelManager.instance.FUN_4DE54(screen, 33);
                            LevelManager.instance.FUN_309C8(this, 1);
                            uVar1 = 0xffffffff;
                        }
                    }
                }
                else
                    uVar1 = 0;

                return uVar1;

            case _ROCKET_TYPE.RoadRunner:
                if (arg1 == 0)
                {
                    vVar2 = Utilities.FUN_2CD78(this) as Vehicle;

                    if ((vVar2.flags & 0x4000000) == 0)
                        vVar2.FUN_2B1FC(new Vector3Int(physics1.Z, physics1.W, physics2.X), vTransform.position);

                    sVar1 = physics2.M2;
                    physics2.M2--;

                    if (sVar1 == 1)
                    {
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 63, screen);
                        LevelManager.instance.FUN_4DE54(vTransform.position, 34);
                        vVar2.FUN_3A020(-40, vTransform.position, true);
                        oVar3 = FUN_2CCBC();
                        GameManager.instance.FUN_307CC(oVar3);
                        return 0xffffffff;
                    }
                }

                return 0;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 2)
        {
            if (2 < arg1)
            {
                if (arg1 != 3)
                    return 0;
            }
        }

        return 0;
    }
}
