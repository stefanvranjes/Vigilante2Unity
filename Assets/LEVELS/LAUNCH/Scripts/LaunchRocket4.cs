using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRocket4 : VigObject
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
        uint uVar1;
        VigObject oVar2;
        Vehicle vVar2;
        Vector3Int auStack16;

        oVar2 = hit.self;
        uVar1 = 0;

        if (oVar2.type == 2)
        {
            vVar2 = (Vehicle)oVar2;
            auStack16 = Utilities.FUN_24094(vTransform.rotation, LaunchRocket3.DAT_C4);
            vVar2.FUN_2B370(auStack16, vTransform.position);
            LevelManager.instance.FUN_39AF8(vVar2);
            uVar1 = 0;
        }

        return uVar1;
    }

    //FUN_14A8 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        int iVar2;
        int iVar3;
        int iVar4;

        if (arg1 < 4)
        {
            if (arg1 == 0)
            {
                vTransform.position.x += screen.x;
                vTransform.position.y += screen.y;
                vTransform.position.z += screen.z;
                iVar3 = screen.x;
                iVar2 = iVar3;

                if (iVar3 < 0)
                    iVar2 = iVar3 + 63;

                iVar4 = screen.z;
                screen.x = iVar3 - (iVar2 >> 6);
                iVar2 = iVar4;

                if (iVar4 < 0)
                    iVar2 = iVar4 + 63;

                screen.z = iVar4 - (iVar2 >> 6);
                screen.y -= 8;
                uVar1 = 0;
            }
            else
                uVar1 = 0;
        }
        else
            uVar1 = 0;

        return uVar1;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }
}
