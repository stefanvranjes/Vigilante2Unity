using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _FORKLIFT2_TYPE
{
    Default,
    Type1 //FUN_2640 (NUCLEAR.DLL)
}

public class ForkLift2 : VigObject
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
        if (hit.self.type == 3)
            return 0;

        FUN_4DC94();
        return 0;
    }

    public _FORKLIFT2_TYPE state;

    //FUN_2640 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        ForkLift3 ppcVar2;
        VigObject oVar3;
        int iVar4;
        VigObject oVar4;
        Vector3Int local_10;

        switch (state)
        {
            case _FORKLIFT2_TYPE.Type1:
                if (arg1 != 2)
                {
                    if (arg1 < 3)
                    {
                        if (arg1 != 0)
                            return 0;

                        vCollider.reader.Seek(4, SeekOrigin.Current);
                        FUN_2B4F8(vCollider.reader);
                        vCollider.reader.Seek(-4, SeekOrigin.Current);
                        return 0;
                    }

                    if (arg1 != 3)
                    {
                        if (arg1 != 9)
                            return 0;

                        if (arg2 == 0)
                        {
                            local_10 = GameManager.instance.FUN_2CE50(this);
                            ppcVar2 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(26, typeof(ForkLift3), 8) as ForkLift3;
                            Utilities.ParentChildren(ppcVar2, ppcVar2);

                            if (parent == null)
                                sVar1 = id;
                            else
                            {
                                oVar3 = Utilities.FUN_2CD78(this);
                                sVar1 = oVar3.id;
                            }

                            ppcVar2.id = sVar1;
                            ppcVar2.type = 8;
                            ppcVar2.flags |= 0x314;
                            ppcVar2.screen = local_10;
                            ppcVar2.maxHalfHealth = 150;
                            ppcVar2.FUN_3066C();
                            ppcVar2.DAT_58 = ppcVar2.vCollider.reader.ReadInt32(16);
                            LevelManager.instance.FUN_4DE54(local_10, 140);
                            iVar4 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E5D4(iVar4, LevelManager.instance.xobfList[42].sndList, 2, local_10);
                            return 0;
                        }

                        if (parent != null)
                        {
                            oVar4 = FUN_2CCBC();
                            GameManager.instance.FUN_307CC(oVar4);
                            return 0xffffffff;
                        }

                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }
                }

                FUN_4DC94();
                return 0;
        }

        return 0;
    }
}
