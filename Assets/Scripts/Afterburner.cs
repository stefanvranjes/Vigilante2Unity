using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_443C8
    public override uint UpdateW(int arg1, int arg2)
    {
        Throwaway ppcVar2;
        ConfigContainer ccVar3;
        VigObject oVar3;
        Vector3Int v3Var3;
        int iVar4;
        VigObject oVar5;
        int iVar6;

        if (arg1 == 0)
        {
            oVar5 = (Vehicle)PDAT_78;

            if ((oVar5.flags & 0x4000000) != 0)
                return 0;

            id--;

            if (id == -1)
            {
                oVar5 = Utilities.FUN_2CD78(this);
                ppcVar2 = vData.ini.FUN_2C17C(197, typeof(Throwaway), 0) as Throwaway;
                ccVar3 = oVar5.FUN_2C5F4(0x8001);
                ppcVar2.vTransform = GameManager.instance.FUN_2CEAC(oVar5, ccVar3);
                ppcVar2.type = 4;
                ppcVar2.flags = 0xA0;
                iVar6 = ppcVar2.vTransform.rotation.V02;
                ppcVar2.state = _THROWAWAY_TYPE.Unspawnable;

                if (iVar6 < 0)
                    iVar6 += 3;

                iVar4 = ppcVar2.vTransform.rotation.V12;
                ppcVar2.physics1.Z = iVar6 >> 2;

                if (iVar4 < 0)
                    iVar4 += 3;

                ppcVar2.physics1.W = iVar4 >> 2;
                iVar6 = ppcVar2.vTransform.rotation.V22;

                if (iVar6 < 0)
                    iVar6 += 3;

                ppcVar2.physics2.X = iVar6 >> 2;
                ppcVar2.DAT_87 = 2;
                ppcVar2.FUN_305FC();
                oVar3 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar3);

                if (oVar5.maxHalfHealth != 0)
                    return 0xffffffff;

                oVar5.FUN_3A368();
                return 0xffffffff;
            }

            oVar5.physics1.X += oVar5.vTransform.rotation.V02 * 4;
            oVar5.physics1.Y += oVar5.vTransform.rotation.V12 * 4;
            oVar5.physics1.Z += oVar5.vTransform.rotation.V22 * 4;

            if (((ushort)id & 3) != 0)
                return 0;

            v3Var3 = GameManager.instance.FUN_2CE50(this);
            oVar5 = LevelManager.instance.FUN_4DE54(v3Var3, 7);
            oVar5.flags |= 0x400;
            oVar5.vr.z = id * 96;
            oVar5.ApplyTransformation();
        }

        return 0;
    }
}
