using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStick : VigObject
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
        ushort uVar3;
        BufferedBinaryReader brVar4;
        VigObject oVar5;
        Vehicle vVar5;

        oVar5 = hit.self;

        if (oVar5.type != 2)
            return 0;

        vVar5 = (Vehicle)oVar5;

        if (-1 < oVar5.id)
            return 0;

        if (DAT_4A + 0x9000 < 0x8800)
            return 0;

        brVar4 = oVar5.vCollider.reader;
        DAT_80 = oVar5;

        if (vVar5.body[0] == null)
            uVar3 = vVar5.maxHalfHealth;
        else
            uVar3 = (ushort)(vVar5.body[0].maxHalfHealth +
                             vVar5.body[1].maxHalfHealth);

        physics1.Z = uVar3 - 100;
        flags |= 0x20;
        physics1.W = (brVar4.ReadInt32(4) + brVar4.ReadInt32(16)) / 2;
        physics2.X = brVar4.ReadInt32(20);
        physics2.Y = brVar4.ReadInt32(24);
        FUN_30B78();
        iVar1 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar1, LevelManager.instance.xobfList[42].sndList, 5, vTransform.position);
        return 0;
    }

    //FUN_1158 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar2;
        int iVar4;
        VigObject oVar4;
        int iVar5;
        Vehicle vVar5;
        ConfigContainer ccVar5;
        Vector3Int local_30;
        Vector3Int local_20;
        Vector3Int local_10;

        switch (arg1)
        {
            case 0:
                vVar5 = DAT_80 as Vehicle;

                if (vVar5 == null)
                {
                    oVar4 = child2;
                    iVar5 = -oVar4.vTransform.rotation.V22 + 0x1000;

                    if (iVar5 < 0)
                        iVar5 = -oVar4.vTransform.rotation.V22 + 0x100f;

                    oVar4.vTransform.rotation.V22 += (short)(iVar5 >> 4);
                    iVar5 = -vTransform.rotation.V11;

                    if (0 < vTransform.rotation.V11)
                        iVar5 += 15;

                    iVar2 = vTransform.rotation.V10;

                    if (iVar2 < 0)
                        iVar2 += 15;

                    FUN_24700((short)(iVar5 >> 4), (short)(iVar2 >> 4), 0);
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                    return 0;
                }

                local_30 = Utilities.FUN_24148(vVar5.vTransform, new Vector3Int(physics1.W, physics2.X, physics2.Y));
                local_20 = new Vector3Int();
                local_20.x = local_30.x - vTransform.position.x;
                local_20.y = local_30.y - vTransform.position.y;
                local_20.z = local_30.z - vTransform.position.z;
                iVar4 = Utilities.FUN_29FC8(local_20, out local_10);
                vTransform.rotation = Utilities.FUN_2A724(local_10);
                iVar4 = (iVar4 << 12) / physics1.Y;

                if (iVar4 < 0x2000 && 
                    (InputManager.controllers[~vVar5.id].GetAxis() & 0x1000000) == 0 && 
                    0x87ffU < (ushort)(DAT_4A - 0x7000))
                {
                    if (vVar5.body[0] != null)
                    {
                        if (physics1.Z <= 
                            vVar5.body[0].maxHalfHealth +
                            vVar5.body[1].maxHalfHealth)
                        {
                            child2.vTransform.rotation.V22 = (short)iVar4;

                            if (iVar4 < 0x1001)
                                return 0;

                            iVar4 = 0x1000 - iVar4;
                            local_20.x = local_10.x * iVar4;

                            if (local_20.x < 0)
                                local_20.x += 127;

                            local_20.y = local_10.y * iVar4;
                            local_20.x >>= 7;

                            if (local_20.y < 0)
                                local_20.y += 127;

                            local_20.y >>= 7;
                            iVar4 = local_10.z * iVar4;

                            if (iVar4 < 0)
                                iVar4 += 127;

                            local_20.z = iVar4 >> 7;
                            vVar5.FUN_2B370(local_20, local_30);
                            return 0;
                        }

                        DAT_80 = null;
                        goto LAB_12CC;
                    }

                    if (physics1.Z <= vVar5.maxHalfHealth)
                    {
                        child2.vTransform.rotation.V22 = (short)iVar4;

                        if (iVar4 < 0x1001)
                            return 0;

                        iVar4 = 0x1000 - iVar4;
                        local_20.x = local_10.x * iVar4;

                        if (local_20.x < 0)
                            local_20.x += 127;

                        local_20.y = local_10.y * iVar4;
                        local_20.x >>= 7;

                        if (local_20.y < 0)
                            local_20.y += 127;

                        local_20.y >>= 7;
                        iVar4 = local_10.z * iVar4;

                        if (iVar4 < 0)
                            iVar4 += 127;

                        local_20.z = iVar4 >> 7;
                        vVar5.FUN_2B370(local_20, local_30);
                        return 0;
                    }
                }

                DAT_80 = null;
                LAB_12CC:
                GameManager.instance.FUN_30CB0(this, 60);
                return 0;
            case 1:
                vr.x = -0x400;
                flags |= 0x100;
                ccVar5 = child2.FUN_2C5F4(0x8000);
                physics1.Y = ccVar5.v3_1.z;
                ApplyTransformation();
                break;
            case 2:
                FUN_30BA8();
                flags &= 0xffffffdf;
                break;
        }

        return 0;
    }
}
