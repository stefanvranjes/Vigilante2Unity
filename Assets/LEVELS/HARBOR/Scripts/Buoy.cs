using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : VigObject
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
        ushort uVar1;
        bool bVar2;
        BufferedBinaryReader brVar3;

        if (hit.self.type != 8)
            return 0;

        if (maxHalfHealth < hit.self.maxHalfHealth)
        {
            uVar1 = (ushort)child2.DAT_1A;
            maxHalfHealth = maxFullHealth;
            bVar2 = child2.FUN_4DC94();

            if (bVar2)
            {
                brVar3 = vData.FUN_2CBB0(uVar1);
                child2.vAnim = new BufferedBinaryReader(null);
                child2.vAnim.ChangeBuffer(brVar3);
            }
        }
        else
            maxHalfHealth -= hit.self.maxHalfHealth;

        return 0;
    }

    //FUN_590 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        bool bVar2;
        BufferedBinaryReader brVar3;

        if (arg1 != 8)
            return 0;

        if (maxHalfHealth < arg2)
        {
            uVar1 = (ushort)child2.DAT_1A;
            maxHalfHealth = maxFullHealth;
            bVar2 = child2.FUN_4DC94();

            if (bVar2)
            {
                brVar3 = vData.FUN_2CBB0(uVar1);
                child2.vAnim = new BufferedBinaryReader(null);
                child2.vAnim.ChangeBuffer(brVar3);
            }
        }
        else
            maxHalfHealth -= (ushort)arg2;

        return 0;
    }
}
