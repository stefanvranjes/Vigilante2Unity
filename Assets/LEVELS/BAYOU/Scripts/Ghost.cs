using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : VigObject
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
        VigObject oVar2;
        Vehicle vVar2;
        int iVar5;
        Vector3Int local_10;

        oVar2 = hit.self;

        if (oVar2.type == 2)
        {
            vVar2 = (Vehicle)oVar2;
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar5, vData.sndList, 6, vTransform.position);
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar5, GameManager.instance.DAT_C2C, 24, vTransform.position);
            LevelManager.instance.FUN_4DE54(vTransform.position, 144);
            UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x00, 0x80, 0x00, 8));
            local_10 = new Vector3Int();
            local_10.x = physics1.Y << 6;
            local_10.y = physics1.Z << 6;
            local_10.z = physics1.W << 6;
            vVar2.FUN_2B370(local_10, vTransform.position);
            DAT_80 = this;
            flags |= 0x20;
            screen.y -= 0xfa000;
        }

        return 0;
    }

    //FUN_203C (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar2;
        VigObject oVar2;
        Ballistic puVar3;
        int iVar4;
        int iVar5;
        short sVar5;
        VigObject oVar5;
        uint uVar8;
        Vector3Int local_20;

        if (arg1 == 2)
        {
            sVar1 = (sbyte)(tags + 1);
            tags = sVar1;

            if (sVar1 == 1)
            {
                oVar5 = GameManager.instance.FUN_320DC(vTransform.position, 0);
                DAT_80 = oVar5;
            }
            else
            {
                if (sVar1 == 6)
                {
                    GameManager.instance.FUN_309A0(this);
                    return 0xffffffff;
                }
            }

            GameManager.instance.FUN_30CB0(this, 60);
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar5, vData.sndList, 8, vTransform.position);
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    oVar2 = DAT_80;
                    local_20 = new Vector3Int();
                    local_20.x = oVar2.screen.x;
                    local_20.z = oVar2.screen.z;
                    local_20.y = oVar2.screen.y - 0x5000;
                    local_20 = Utilities.FUN_24304(vTransform, local_20);
                    iVar2 = (local_20.y * -0x800) / local_20.z;

                    if (iVar2 < -0x200)
                        iVar4 = -0x200;
                    else
                    {
                        iVar4 = 0x200;

                        if (iVar2 < 0x201)
                            iVar4 = iVar2;
                    }

                    sVar5 = -0x200;

                    if (0 < local_20.x)
                        sVar5 = 0x200;

                    FUN_24700((short)iVar4, sVar5, 0);
                    iVar2 = vTransform.rotation.V02 * 0x3b9a;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    iVar2 = (iVar2 >> 12) - physics1.Y;

                    if (iVar2 < 0)
                        iVar2 += 15;

                    iVar2 >>= 4;
                    iVar4 = -0x100;

                    if (-0x101 < iVar2)
                    {
                        iVar4 = 0x100;

                        if (iVar2 < 0x101)
                            iVar4 = iVar2;
                    }

                    iVar2 = vTransform.rotation.V12 * 0x3b9a;
                    physics1.Y += iVar4;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    iVar2 = (iVar2 >> 12) - physics1.Z;

                    if (iVar2 < 0)
                        iVar2 += 7;

                    physics1.Z += iVar2 >> 3;
                    iVar2 = vTransform.rotation.V22 * 0x3b9a;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    iVar2 = (iVar2 >> 12) - physics1.W;

                    if (iVar2 < 0)
                        iVar2 += 15;

                    iVar2 >>= 4;
                    iVar4 = -0x100;

                    if (-0x101 < iVar2)
                    {
                        iVar4 = 0x100;

                        if (iVar2 < 0x101)
                            iVar4 = iVar2;
                    }

                    physics1.W += iVar4;
                    vTransform.position.x += physics1.Y;
                    vTransform.position.y += physics1.Z;
                    vTransform.position.z += physics1.W;
                    uVar8 = (uint)GameManager.instance.DAT_28 - DAT_19;

                    if ((uVar8 & 31) == 0)
                        vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                    
                    if ((uVar8 & 3) == 0)
                    {
                        puVar3 = vData.ini.FUN_2C17C(29, typeof(Ballistic), 8) as Ballistic;
                        puVar3.flags = 0x24;
                        puVar3.vTransform = vTransform;
                        puVar3.FUN_305FC();
                    }
                }
            }
        }

        return 0;
    }
}
