using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slalom2 : Destructible
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
        Vector3Int v3Var3;
        uint uVar4;
        ushort uVar5;
        bool bVar6;
        int iVar6;
        VigObject oVar6;

        if (hit.collider1.ReadUInt16(0) != 1 || hit.collider1.ReadUInt16(2) == 0 || 
            hit.self.type != 2)
        {
            bVar6 = FUN_32CF0(hit);

            if (!bVar6)
                return 0;

            oVar6 = child2;

            while (oVar6 != null)
            {
                if (oVar6.id == 1)
                    oVar6.type = 3;

                oVar6 = oVar6.child;
            }

            return 0;
        }

        ppiVar1 = ((OLYMPIC)LevelManager.instance.level).DAT_A4;
        oVar6 = null; //not in the original code

        for (int i = 0; i < ppiVar1.Count; i++)
        {
            ppiVar2 = ppiVar1[i];
            oVar6 = ppiVar2.vObject;

            if (oVar6.id == hit.self.id) goto LAB_36C4;
        }

        return 0;

        LAB_36C4:
        uVar5 = oVar6.DAT_19;

        if (uVar5 == 97)
        {
            if (id == 65)
                oVar6.DAT_19 = 65;

            uVar5 = oVar6.DAT_19;
        }

        if (uVar5 != id)
            return 0;

        oVar6.DAT_19 = (byte)(uVar5 + 1);
        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar3, vData.sndList, 2, vTransform.position);
        GameManager.instance.FUN_1E30C(iVar3, (id - 64) * 250 + 0x1000);
        uVar4 = GameManager.FUN_2AC5C();

        if ((uVar4 & 1) == 0)
            return 0;

        iVar6 = id;

        if (iVar6 < 67)
            return 0;

        if (71 < iVar6)
            return 0;

        oVar6 = GameManager.instance.FUN_318D0(iVar6 + 1);

        if (oVar6 == null)
            return 0;

        oVar6 = oVar6.child2;

        if (oVar6 == null)
            return 0;

        do
        {
            if (oVar6.id == 1) break;

            oVar6 = oVar6.child2;
        } while (oVar6 != null);

        if (oVar6 != null)
        {
            v3Var3 = GameManager.instance.FUN_2CE50(oVar6);
            LevelManager.instance.FUN_4AAC0(0xfe000000, v3Var3, Slalom.DAT_194);
        }

        return 0;
    }

    //FUN_35F4 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar6;
        VigObject oVar6;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar6 = child2;

            while (oVar6 != null)
            {
                if (oVar6.id == 1)
                    oVar6.type = 3;

                oVar6 = oVar6.child;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        bVar6 = FUN_32B90((uint)arg2);

        if (!bVar6)
            return 0;

        oVar6 = child2;

        while (oVar6 != null)
        {
            if (oVar6.id == 1)
                oVar6.type = 3;

            oVar6 = oVar6.child;
        }

        return 0;
    }
}
