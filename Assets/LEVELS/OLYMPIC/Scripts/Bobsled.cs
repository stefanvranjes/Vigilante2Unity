using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobsled : Destructible
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
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;
        int iVar3;
        bool bVar4;
        VigObject oVar4;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0 && 
            hit.self.type == 2)
        {
            ppiVar1 = ((OLYMPIC)LevelManager.instance.level).DAT_98;

            for (int i = 0; i < ppiVar1.Count; i++)
            {
                ppiVar2 = ppiVar1[i];
                oVar4 = ppiVar2.vObject;

                if (oVar4.id == hit.self.id)
                {
                    if (oVar4.DAT_19 != id)
                        return 0;

                    oVar4.DAT_19++;
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar3, vData.sndList, 2, vTransform.position);
                    GameManager.instance.FUN_1E30C(iVar3, (id - 33) * 250 + 0x1000);
                    return 0;
                }
            }
        }
        else
        {
            bVar4 = FUN_32CF0(hit);

            if (bVar4)
            {
                oVar4 = child2;

                while (oVar4 != null)
                {
                    if (oVar4.id == 1)
                        oVar4.type = 3;

                    oVar4 = oVar4.child;
                }
            }
        }

        return 0;
    }

    //FUN_30C0 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar4;
        VigObject oVar4;

        if (arg1 < 4)
        {
            if (arg1 == 1)
            {
                oVar4 = child2;

                while (oVar4 != null)
                {
                    if (oVar4.id == 1)
                        oVar4.type = 3;

                    oVar4 = oVar4.child;
                }
            }
        }
        else
        {
            if (arg1 == 8)
            {
                bVar4 = FUN_32B90((uint)arg2);

                if (bVar4)
                {
                    oVar4 = child2;

                    while (oVar4 != null)
                    {
                        if (oVar4.id == 1)
                            oVar4.type = 3;

                        oVar4 = oVar4.child;
                    }
                }
            }
        }

        return 0;
    }
}
