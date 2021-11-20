using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockWheel : VigObject
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
        VigObject oVar4;
        VigTuple2 tVar5;
        int iVar6;

        if ((flags & 0x80) == 0)
        {
            oVar4 = GameManager.instance.FUN_318F8(id, this);

            if (oVar4.tags < 6)
            {
                tVar5 = GameManager.instance.FUN_2FFD0(oVar4.id);

                if (tVar5 != null)
                    LevelManager.instance.FUN_359CC(tVar5.array, 0);

                PDAT_74 = oVar4;

                if ((oVar4.flags & 0x80) == 0)
                    GameManager.instance.FUN_30CB0(oVar4, 0);

                maxHalfHealth = 90;
                FUN_30B78();
                iVar6 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar6, vData.sndList, 3, screen);
            }
        }

        return 0;
    }

    //FUN_125C (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        LockWheel2 ppcVar3;
        int iVar6;
        VigObject pcVar7;

        if (arg1 == 0)
        {
            vr.x += (short)maxHalfHealth * 2;

            if (arg2 != 0)
                ApplyTransformation();

            pcVar7 = PDAT_74;
            pcVar7.vTransform.position.y -= (maxHalfHealth * 0x5000) / 0x1fa4;
            sVar2 = (short)(maxHalfHealth - 1);
            maxHalfHealth = (ushort)sVar2;

            if (sVar2 == 0)
            {
                FUN_30BA8();
                sVar1 = pcVar7.tags;
                pcVar7.tags = (sbyte)(sVar1 + 1);

                if ((sVar1 + 1) == 6 && pcVar7.PDAT_74.tags == 6)
                {
                    GameObject obj = new GameObject();
                    ppcVar3 = obj.AddComponent<LockWheel2>();
                    ppcVar3.type = 255;
                    ppcVar3.child = pcVar7;
                    ppcVar3.FUN_30B78();
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar6, vData.sndList, 9, screen);
                    LevelManager.instance.level.flags |= 0x1000000;
                }
            }
        }

        return 0;
    }
}
