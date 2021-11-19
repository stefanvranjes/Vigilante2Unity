using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace3 : VigObject
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
        int iVar2;
        bool bVar3;

        if (hit.object2.type == 3)
            return 0;

        if (hit.self.type == 2)
        {
            bVar3 = LevelManager.instance.FUN_39AF8((Vehicle)hit.self);

            if (bVar3)
            {
                LevelManager.instance.FUN_4DE54(vTransform.position, 35);
                UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x80, 0x00, 8));
            }
        }

        return 0;
    }

    public XOBF_DB DAT_98; //0x98

    //213C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        Furnace4 ppcVar3;
        int iVar4;
        VigObject oVar4;
        int iVar6;
        MoltenSteel ppcVar7;

        if (arg1 == 0)
        {
            sVar1 = (short)(physics1.M0 - 1);
            physics1.M0 = sVar1;

            if (sVar1 == -1)
            {
                ppcVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(113, typeof(Furnace4), 8) as Furnace4;
                ppcVar3.flags |= 0x410;
                iVar4 = (int)GameManager.FUN_2AC5C();
                iVar6 = physics1.W;
                ppcVar3.physics1.W = 0;
                ppcVar3.physics1.Z = iVar6 + (iVar4 * (iVar6 / 2) >> 15);
                iVar4 = (int)GameManager.FUN_2AC5C();
                ppcVar3.physics2.X = physics1.W + (iVar4 * (physics1.W / 2) >> 15);
                ppcVar3.vTransform = GameManager.FUN_2A39C();
                Utilities.FUN_2CC48(this, ppcVar3);
                Utilities.ParentChildren(this, this);
                physics1.M0 = physics1.M1;
            }

            oVar4 = child2;

            if (oVar4 == null)
            {
                GameManager.instance.FUN_309A0(this);
                uVar2 = 0xffffffff;
            }
            else
            {
                do
                {
                    if (oVar4.tags == 0)
                    {
                        oVar4.vTransform.position.y += oVar4.physics1.W;
                        oVar4.vTransform.position.x += oVar4.physics1.Z;
                        oVar4.physics1.W += 90;
                        oVar4.vTransform.position.z += oVar4.physics2.X;

                        if (screen.y < oVar4.vTransform.position.y)
                        {
                            oVar4.vTransform.position.y = screen.y;
                            iVar6 = (int)GameManager.FUN_2AC5C();
                            oVar4.physics1.Z = oVar4.physics1.Z + (iVar6 * 0xccc >> 15) - 0x666;
                            iVar6 = (int)GameManager.FUN_2AC5C();
                            oVar4.physics1.W = -(oVar4.physics1.W / 2 + (iVar6 * 0x666 >> 15));
                            iVar6 = (int)GameManager.FUN_2AC5C();
                            oVar4.physics2.X = oVar4.physics2.X + (iVar6 * 0xccc >> 15) - 0x666;
                            oVar4.IDAT_78 = 0x1000;
                            oVar4.tags = 1;
                            oVar4.FUN_30B78();

                            if ((flags & 0x1000000) == 0)
                            {
                                ppcVar7 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(28, typeof(MoltenSteel), 8) as MoltenSteel;
                                Utilities.ParentChildren(ppcVar7, ppcVar7);
                                ppcVar7.flags |= 4;
                                ppcVar7.screen = GameManager.instance.FUN_2CE50(oVar4);
                                ppcVar7.physics2.X = 0x1000;
                                ppcVar7.FUN_3066C();
                                flags |= 0x1000000;
                                GameManager.instance.FUN_30CB0(ppcVar7, 120);
                            }
                        }
                    }

                    oVar4 = oVar4.child;
                    uVar2 = 0;
                } while (oVar4 != null);
            }
        }
        else
        {
            uVar2 = 0;

            if (arg1 == 2)
            {
                physics1.M0 = -1;
                uVar2 = 0;
            }
        }

        return uVar2;
    }
}
