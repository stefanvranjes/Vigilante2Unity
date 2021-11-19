using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPlatform : VigObject
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
        Vehicle vVar3;

        if (hit.self.type == 2)
        {
            vVar3 = (Vehicle)hit.self;

            if (tags == 1)
            {
                if (hit.object1.id != 1)
                    return 0;

                if ((flags & 0x80) == 0)
                    return 0;

                DAT_18 = 0;
                tags = 0;
                FUN_30BA8();
            }
            else
            {
                if (hit.collider1.ReadUInt16(2) == 0)
                    return 0;

                if ((flags & 0x80) != 0)
                    return 0;

                //vVar3.FUN_3E32C(_WHEELS.Ground, 0);
            }

            GameManager.instance.FUN_30CB0(this, 30);
            return 0;
        }

        if (hit.self.type != 8)
            return 0;

        Destruction(hit.self.maxHalfHealth);
        return 0;
    }

    public bool DAT_A0_2;

    //FUN_2180 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar2;
        VigObject oVar3;

        switch (arg1)
        {
            case 0:
                /*oVar3 = PDAT_74;

                if (tags == 0)
                {
                    iVar2 = oVar3.vTransform.position.y - 915;
                    oVar3.vTransform.position.y = iVar2;

                    if (oVar3.screen.y <= iVar2)
                        return 0;
                }
                else
                {
                    iVar2 = oVar3.vTransform.position.y + 915;
                    oVar3.vTransform.position.y = iVar2;

                    if (iVar2 <= GameManager.instance.DAT_DB0 - (screen.y - 0xa000))
                        return 0;
                }

                DAT_18 = 0;
                FUN_30BA8();
                sVar1 = (sbyte)(1 - tags);
                tags = sVar1;

                if (sVar1 != 0)
                    GameManager.instance.FUN_30CB0(this, 300);*/
                break;
            case 1:
                oVar3 = child2;

                while (oVar3 != null && oVar3.id != 1)
                    oVar3 = oVar3.child;

                PDAT_74 = oVar3;
                type = 4;
                maxHalfHealth = 1000;
                maxFullHealth = 1000;

                if (oVar3 != null)
                {
                    oVar3.vTransform.position.y = GameManager.instance.DAT_DB0 - (screen.y - 0x5000);
                    flags |= 0x40;
                }

                oVar3 = child2;

                while (oVar3 != null)
                {
                    vData.FUN_1F288(51, oVar3.vMesh);
                    vData.FUN_1F288(52, oVar3.vMesh);
                    oVar3 = oVar3.child;
                }

                GameManager.instance.FUN_30CB0(this, 10);
                break;
            case 2:
                /*if (PDAT_74 != null && (flags & 0x80) == 0)
                    FUN_30B78();*/

                if (DAT_A0_2)
                {
                    oVar3 = child2;

                    while (oVar3 != null)
                    {
                        oVar3.vMesh.DAT_1C[0] = 51;
                        oVar3.vMesh.DAT_1C[0] = 51;
                        oVar3 = oVar3.child;
                    }
                }
                else
                {
                    oVar3 = child2;

                    while (oVar3 != null)
                    {
                        oVar3.vMesh.DAT_1C[0] = 52;
                        oVar3.vMesh.DAT_1C[0] = 52;
                        oVar3 = oVar3.child;
                    }
                }

                DAT_A0_2 = !DAT_A0_2;
                GameManager.instance.FUN_30CB0(this, 60);
                break;
            case 8:
                Destruction((uint)arg2);
                break;
            case 4:
                GameManager.instance.FUN_1DE78(DAT_18);
                break;
            case 9:
                if (arg2 == 0)
                {
                    if ((flags & 0x80) != 0)
                        FUN_30BA8();

                    FUN_30C68();
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                    tags = -1;
                }

                break;
        }

        return 0;
    }

    private void Destruction(uint param1)
    {
        bool bVar1;
        Orca oVar4;
        Pickup pVar5;

        bVar1 = FUN_32B90(param1);

        if (bVar1)
        {
            oVar4 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(22, typeof(Orca), 8) as Orca;
            oVar4.screen = vTransform.position;
            oVar4.screen.z -= 0x1000000;
            oVar4.tags = 1;
            oVar4.type = 4;
            oVar4.FUN_3066C();
            oVar4.ApplyTransformation();
            pVar5 = LevelManager.instance.FUN_4AD24(7);
            pVar5.screen = vTransform.position;
            pVar5.screen.y += 0x36000;
            pVar5.ApplyTransformation();
            pVar5.type = 3;
            pVar5.flags = pVar5.flags & 0xfffffff7 | 0x380;
            pVar5.FUN_305FC();
        }
    }
}
