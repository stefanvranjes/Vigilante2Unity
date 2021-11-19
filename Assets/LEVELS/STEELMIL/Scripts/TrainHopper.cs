using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHopper : TrainEngine
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
        FUN_32CF0(hit);
        return 0;
    }

    //FUN_4234 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;

        switch (arg1)
        {
            case 0:
                FUN_3AD0();
                uVar1 = 0;
                break;
            case 1:
                FUN_3754();
                uVar1 = 0;
                break;
            case 2:
                FUN_4DC94();
                uVar1 = 0;
                break;
            case 4:
                FUN_38FC();
                goto default;
            default:
                uVar1 = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                uVar1 = 0;
                break;
            case 9:
                uVar1 = 0;

                if (arg2 != 0)
                {
                    uVar1 = 0;

                    if (vMesh == null)
                    {
                        GameManager.instance.FUN_309A0(this);
                        uVar1 = 0;
                    }
                }

                break;
        }

        return uVar1;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (arg1)
        {
            case 20:
                FUN_3838((TrainEngine)arg2);
                return 0;
        }

        return 0;
    }
}
