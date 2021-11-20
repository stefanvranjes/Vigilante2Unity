using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGate4 : VigObject
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
        bool bVar2;
        Vehicle vVar4;

        if (hit.self.type != 2)
            return 0;

        vVar4 = (Vehicle)hit.self;

        if (!hitList.Contains(vVar4))
        {
            bVar2 = LevelManager.instance.FUN_39AF8(vVar4);

            if (bVar2)
            {
                vVar4.physics1.Y -= 0xee680;
                UIManager.instance.FUN_4E414(vTransform.position, new Color32(0xff, 0xff, 0x00, 8));
            }

            //flags |= 0x20;
            hitList.Add(vVar4);
        }

        return 0;
    }

    public List<Vehicle> hitList = new List<Vehicle>();

    //FUN_64C (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        Ballistic puVar3;
        int iVar4;
        VigObject oVar4;

        if (arg1 == 2)
        {
            if (tags == 1)
            {
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }

            puVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(109, typeof(Ballistic), 8) as Ballistic;
            puVar3.flags = 0x410;
            puVar3.id = 0;
            iVar4 = (int)GameManager.FUN_2AC5C();
            puVar3.screen.x = (iVar4 * 3051 >> 15) - 1525;
            puVar3.screen.y = -3051;
            iVar4 = (int)GameManager.FUN_2AC5C();
            puVar3.screen.z = (iVar4 * 3051 >> 15) - 1525;
            puVar3.screen = Utilities.FUN_24094(puVar3.vTransform.rotation, puVar3.screen);
            Utilities.FUN_2CC9C(this, puVar3);
            puVar3.transform.parent = transform;
            sVar1 = (sbyte)(DAT_19 + 1);
            DAT_19 = (byte)sVar1;

            if (sVar1 == 1)
            {
                FUN_30B78();
                flags &= 0xffffffdf;
            }

            if (DAT_19 < 12U)
                GameManager.instance.FUN_30CB0(this, 3);
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                oVar4 = child2;

                while (true)
                {
                    if (oVar4 == null)
                        return 0;

                    if (oVar4.id != 0) break;
                    oVar4.vTransform.position.x += oVar4.screen.x;
                    oVar4.vTransform.position.y += oVar4.screen.y;
                    oVar4.vTransform.position.z += oVar4.screen.z;
                    oVar4 = oVar4.child;
                }
            }
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 5)
            return 0;

        //GameManager.instance.FUN_309A0(this);
        //return 0xffffffff;
        tags = 1;
        flags |= 0x20;
        GameManager.instance.FUN_30CB0(this, 40);
        return 0;
    }
}
