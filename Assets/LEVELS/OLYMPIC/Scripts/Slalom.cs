using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slalom : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x194 (OLYMPIC.DLL)
    public static Vector3Int DAT_194 = new Vector3Int(0, 0, 0);

    public override uint OnCollision(HitDetection hit)
    {
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;
        int iVar3;
        Vector3Int v3Var3;
        uint uVar4;
        int iVar5;
        bool bVar5;
        VigObject oVar5;

        if (hit.collider1.ReadUInt16(0) != 1 || hit.collider1.ReadUInt16(2) == 0 || 
            hit.self.type != 2)
        {
            bVar5 = FUN_32CF0(hit);

            if (!bVar5)
                return 0;

            oVar5 = child2;

            while(oVar5 != null)
            {
                if (oVar5.id == 1)
                    oVar5.type = 3;

                oVar5 = oVar5.child;
            }

            return 0;
        }

        ppiVar1 = ((OLYMPIC)LevelManager.instance.level).DAT_A4;
        oVar5 = null; //not in the original code

        for (int i = 0; i < ppiVar1.Count; i++)
        {
            ppiVar2 = ppiVar1[i];
            oVar5 = ppiVar2.vObject;

            if (oVar5.id == hit.self.id) goto LAB_33DC;
        }

        return 0;

        LAB_33DC:
        if (oVar5.DAT_19 != id)
            return 0;

        oVar5.DAT_19++;
        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar3, vData.sndList, 2, vTransform.position);
        GameManager.instance.FUN_1E30C(iVar3, (id - 96) * 250 + 0x1000);
        uVar4 = GameManager.FUN_2AC5C();

        if ((uVar4 & 1) == 0)
            return 0;

        iVar5 = id;

        if (iVar5 < 99)
            return 0;

        if (103 < iVar5)
            return 0;

        oVar5 = GameManager.instance.FUN_318D0(iVar5 + 1);

        if (oVar5 == null)
            return 0;

        do
        {
            if (oVar5.id == 1) break;

            oVar5 = oVar5.child2;
        } while (oVar5 != null);

        if (oVar5 != null)
        {
            v3Var3 = GameManager.instance.FUN_2CE50(oVar5);
            LevelManager.instance.FUN_4AAC0(0xfe000000, v3Var3, DAT_194);
        }

        return 0;
    }

    //FUN_330C (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar5;
        VigObject oVar5;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar5 = child2;

            while(oVar5 != null)
            {
                if (oVar5.id == 1)
                    oVar5.type = 3;

                oVar5 = oVar5.child;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        bVar5 = FUN_32B90((uint)arg2);

        if (!bVar5)
            return 0;

        oVar5 = child2;

        while(oVar5 != null)
        {
            if (oVar5.id == 1)
                oVar5.type = 3;

            oVar5 = oVar5.child;
        }

        return 0;
    }
}
