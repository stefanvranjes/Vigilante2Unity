using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_900 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        TileData tVar2;
        int iVar3;
        Wheel wVar4;
        int iVar5;
        int iVar7;
        int iVar8;
        Vehicle vVar9;
        Vector3Int local_48;
        Vector3Int local_38;

        if (arg1 == 0)
        {
            vVar9 = (Vehicle)child;
            iVar1 = vVar9.physics1.W - 4577;

            if (iVar1 < 0)
                iVar1 = vVar9.physics1.W - 4574;

            iVar1 >>= 2;
            iVar7 = 0;
            iVar8 = 0;

            do
            {
                wVar4 = vVar9.wheels[iVar7];

                if (wVar4 != null)
                {
                    iVar5 = wVar4.physics1.Z - wVar4.physics2.X;

                    if (0 < iVar5)
                    {
                        local_48 = new Vector3Int();
                        local_48.x = wVar4.screen.x;
                        local_48.y = wVar4.screen.y + wVar4.physics2.X;
                        local_48.z = wVar4.screen.z;
                        local_38 = Utilities.FUN_24148(vVar9.vTransform, local_48);
                        tVar2 = GameManager.instance.terrain.GetTileByPosition((uint)local_38.x, (uint)local_38.z);
                        iVar3 = iVar5;

                        if (tVar2.DAT_10[3] == 0)
                        {
                            if (0x100 < iVar5)
                                iVar3 = 0x100;

                            wVar4.physics2.X += iVar3;
                        }
                        else
                        {
                            if (0 < iVar1)
                            {
                                if (iVar1 < iVar5)
                                    iVar3 = iVar1;

                                wVar4.physics2.X += iVar3;
                            }
                        }

                        iVar8 += iVar5;
                    }
                }

                iVar7++;
            } while (iVar7 < 4);

            if (iVar8 == 0)
            {
                vVar9.flags &= 0xfffdffff;
                GameManager.instance.FUN_30904(this);
                return 0xffffffff;
            }
        }

        return 0;
    }
}
