using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet1 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_47230
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 4)
            LevelManager.instance.DAT_117C--;

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        sbyte sVar1;
        ushort uVar2;
        short sVar3;
        int iVar4;
        BufferedBinaryReader brVar4;
        VigObject oVar5;

        if (arg1 != 5)
            return 0;

        sVar1 = tags;

        if (sVar1 != 1)
        {
            if (1 < sVar1)
            {
                if (sVar1 != 2)
                    return 0;

                oVar5 = Utilities.FUN_2CD78(this);
                LevelManager.instance.FUN_4DF20(oVar5.vTransform.position, 13, 0x800);
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar4, GameManager.instance.DAT_C2C, 63, oVar5.vTransform.position);
                GameManager.instance.FUN_309A0(oVar5);
                return 0xffffffff;
            }

            if (sVar1 != 0)
                return 0;

            brVar4 = vData.FUN_2CBB0(193);
            uVar2 = GameManager.instance.timer;
            vAnim.ChangeBuffer(brVar4);
            tags = 1;
            maxHalfHealth = 10;
            DAT_4A = uVar2;
            oVar5 = Utilities.FUN_2CD78(this);
            //FUN_4E414
            return 0;
        }

        sVar3 = (short)(maxHalfHealth - 1);
        maxHalfHealth = (ushort)sVar3;

        if (sVar3 != 0)
            return 0;

        brVar4 = vData.FUN_2CBB0(194);
        uVar2 = GameManager.instance.timer;
        vAnim.ChangeBuffer(brVar4);
        tags = 2;
        DAT_4A = uVar2;
        return 0;
    }
}
