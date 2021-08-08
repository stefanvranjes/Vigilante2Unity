using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGun : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_42A64
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (arg1)
        {
            case 1:
                maxHalfHealth = 1280;
                return 0;
        }

        return 0;
    }
}
