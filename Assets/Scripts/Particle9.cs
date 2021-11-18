using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle9 : VigObject
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    //FUN_4E94C
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        VigObject oVar1;
        int iVar2;
        short sVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;

        if (arg1 == 1)
        {
            iVar1 = 0;
            sVar3 = 0;

            do
            {
                iVar1++;
                oVar4 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)DAT_1A, typeof(VigObject), 8);
                oVar4.screen = new Vector3Int(0, 0, 0);
                oVar4.vr = new Vector3Int(341, sVar3, 0);
                oVar4.ApplyTransformation();
                Utilities.FUN_2CC48(this, oVar4);
                Utilities.ParentChildren(this, this);
                sVar3 += 341;
            } while (iVar1 < 12);
        }
        else
        {
            if (arg1 != 0)
                return 0;

            oVar1 = child2;
            iVar2 = maxHalfHealth;

            while(oVar1 != null)
            {
                iVar4 = oVar1.vTransform.rotation.V02 * iVar2;

                if (iVar4 < 0)
                    iVar4 += 4095;

                iVar5 = oVar1.vTransform.rotation.V12 * iVar2;
                oVar1.vTransform.position.x += iVar4 >> 12;

                if (iVar5 < 0)
                    iVar5 += 4095;

                iVar4 = oVar1.vTransform.rotation.V22 * iVar2;
                oVar1.vTransform.position.y += iVar5 >> 12;

                if (iVar4 < 0)
                    iVar4 += 4095;

                oVar1.vTransform.position.z += iVar4 >> 12;
                oVar1 = oVar1.child;
            }

            maxHalfHealth = (ushort)(iVar2 * 31 >> 5);
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xfffffffe;
        }

        return 0;
    }
}
