using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : VigObject
{
    protected override void Start()
    {
        return;
    }

    protected override void Update()
    {
        return;
    }

    public Color32 DAT_34; //0x34
    public int DAT_3C; //0x3C

    //FUN_4E1FC
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        uint uVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        Color32 uVar6;
        int iVar7;

        if (arg1 == 0 && arg2 != 0)
        {
            iVar1 = DAT_3C - 0x80;

            if (iVar1 < 0)
                iVar1 = -iVar1;

            iVar1 = 0x80 - iVar1;
            iVar7 = iVar1 * DAT_34.b;

            if (iVar7 < 0)
                iVar7 += 0x7f;

            iVar4 = ((Color32)UIManager.instance.flash.color).b + (iVar7 >> 7);
            iVar7 = 0xff;

            if (iVar4 < 0xff)
                iVar7 = iVar4;

            iVar4 = iVar1 * DAT_34.g;

            if (iVar4 < 0)
                iVar4 += 0x7f;

            iVar5 = ((Color32)UIManager.instance.flash.color).g + (iVar4 >> 7);
            iVar4 = 0xff;

            if (iVar5 < 0xff)
                iVar4 = iVar5;

            iVar1 = iVar1 * DAT_34.r;
            uVar6 = new Color32();
            uVar6.b = (byte)iVar7;
            uVar6.g = (byte)iVar4;

            if (iVar1 < 0)
                iVar1 += 0x7f;

            iVar3 = ((Color32)UIManager.instance.flash.color).r + (iVar1 >> 7);
            uVar6.r = 0xff;

            if (iVar3 < 0xff)
                uVar6.r = (byte)iVar3;

            uVar6.a = 0xff;
            UIManager.instance.flash.color = uVar6;
            iVar1 = DAT_3C + DAT_34.a * arg2;
            DAT_3C = iVar1;
            uVar2 = 0;

            if (0xff < iVar1)
            {
                GameManager.instance.FUN_309A0(this);
                uVar2 = 0xffffffff;
            }
        }
        else
            uVar2 = 0;

        return uVar2;
    }
}
