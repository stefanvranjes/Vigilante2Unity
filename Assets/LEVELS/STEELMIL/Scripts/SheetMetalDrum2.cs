using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMetalDrum2 : VigObject
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
        bool bVar3;
        VigObject oVar3;
        Vehicle vVar3;
        Vector3Int local_20;
        Vector3Int auStack16;

        oVar3 = hit.self;

        if (oVar3.type == 8)
        {
            bVar3 = FUN_32B90(oVar3.maxHalfHealth);

            if (!bVar3)
                return 0;

            GameManager.instance.FUN_309A0(this);
        }
        else
        {
            if (oVar3.type != 2)
                return 0;

            vVar3 = (Vehicle)oVar3;

            if (-1 < physics2.Y * (vVar3.vTransform.position.z - vTransform.position.z))
            {
                GameManager.instance.FUN_2F798(this, hit);
                auStack16 = Utilities.FUN_24148(vTransform, hit.position);
                LevelManager.instance.FUN_4DE54(auStack16, 142);

                if (vVar3.physics1.Z * physics2.Y < 0)
                    vVar3.physics1.Z = -vVar3.physics1.Z;

                local_20 = new Vector3Int();
                local_20.x = 0;
                local_20.y = -195200;
                local_20.z = physics2.Y << 5;
                DAT_A0_2 = 60;
                physics2.Y = -physics2.Y;
                vVar3.FUN_2B370(local_20, auStack16);
                vVar3.FUN_3A020(-100, DAT_F4, true);
                return 0xffffffff;
            }
        }

        return 0xffffffff;
    }

    private static Vector3Int DAT_F4 = new Vector3Int(0, 0, 0); //0xF4 (STEELMIL.DLL)

    public int DAT_A0_2; //0xA0
    public short DAT_A4; //0xA4
    public int DAT_A8; //0xA8

    //FUN_1000 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        Vector3Int local_40;

        if (arg1 == 2)
        {
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar5, vData.sndList, 4, vTransform.position);
        }
        else
        {
            if (2 < arg1)
            {
                if (arg1 != 8)
                    return 0;

                FUN_32B90((uint)arg2);
                return 0;
            }

            if (arg1 != 0)
                return 0;

            if (DAT_A0_2 != 0)
                DAT_A0_2--;

            local_40 = new Vector3Int();
            local_40.x = vTransform.position.x;
            local_40.y = vTransform.position.y + DAT_A8;
            local_40.z = vTransform.position.z;
            iVar2 = FUN_2CFBC(local_40);
            iVar3 = iVar2 - physics2.Z;

            if (iVar3 < 0)
                iVar3 = 0;

            if (iVar3 < 3276 && DAT_A0_2 == 0)
            {
                iVar3 += 3276;
                DAT_A0_2 = 240;
                physics2.Y = -physics2.Y;
                GameManager.instance.FUN_30CB0(this, 60);
            }

            iVar4 = physics2.W - iVar2;

            if (iVar4 < 0)
                iVar4 = -iVar4;

            vTransform.position.y = iVar2 - DAT_A8;
            iVar3 /= 12;

            if (physics2.Y < 0)
            {
                iVar3 = -iVar3;
                iVar4 = -iVar4;
            }

            iVar4 = -(iVar3 + iVar4) * DAT_A4;
            vTransform.position.z += iVar3;

            if (iVar4 < 0)
                iVar4 += 4095;

            sVar1 = (short)(iVar4 >> 12);
            physics1.M6 = sVar1;
            iVar3 = sVar1;

            if (iVar3 < 0)
                iVar3 += 3;

            vr.x += iVar3 >> 2;
            ApplyRotationMatrix();
            physics2.W = iVar2;
        }

        return 0;
    }
}
