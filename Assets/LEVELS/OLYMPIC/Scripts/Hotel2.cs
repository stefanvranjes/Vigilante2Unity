using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_1F6C (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar1;

        if (arg1 == 2)
        {
            oVar1 = child.child2;

            while (oVar1 != null)
            {
                if (oVar1.id == id)
                {
                    if (child.GetType().IsSubclassOf(typeof(VigObject)))
                    {
                        oVar1.FUN_32B90(100);
                        return 0;
                    }

                    break;
                }

                oVar1 = oVar1.child;
            }

            Destroy(gameObject);
        }

        return 0;
    }
}
