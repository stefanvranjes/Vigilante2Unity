using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite2 : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0xC0 (ROUTE66.DLL)
    private static Vector3Int DAT_C0 = new Vector3Int(0x2000, 0x2000, 0x2000);
    //0xD0 (ROUTE66.DLL)
    private static Vector3Int DAT_D0 = new Vector3Int(0x4000, 0x4000, 0x4000);

    public override uint OnCollision(HitDetection hit)
    {
        FUN_32CF0(hit);
        return 0;
    }

    //FUN_780 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        Particle2 pVar2;
        Particle2 pVar3;
        int iVar4;
        Ballistic puVar5;
        Meteorite3 mVar5;
        uint uVar6;

        if (arg1 < 4)
        {
            if (arg1 == 0)
            {
                FUN_2AF20();

                if (vTransform.position.y < screen.y)
                {
                    sVar1 = (sbyte)(DAT_19 - 1);
                    DAT_19 = (byte)sVar1;

                    if (sVar1 == 0)
                    {
                        puVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(109, typeof(Ballistic), 8) as Ballistic;
                        uVar6 = GameManager.FUN_2AC5C();
                        DAT_19 = (byte)((sbyte)(uVar6 & 3) + 5);
                        puVar5.flags = 0x434;
                        puVar5.id = 0;
                        puVar5.vTransform.position.x = vTransform.position.x + ((int)(uVar6 & 3) - 2) * 0x8000;
                        puVar5.vTransform.position.y = vTransform.position.y;
                        puVar5.vTransform.position.z = vTransform.position.z + (((int)uVar6 >> 2 & 3) - 2) * 0x8000;
                        Ant2.FUN_50F0(ref puVar5.vTransform.rotation, DAT_D0);
                        puVar5.FUN_305FC();
                    }
                }
                else
                {
                    pVar2 = LevelManager.instance.FUN_4E128(screen, 79, 40);
                    pVar2.FUN_2D114(pVar2.screen, ref pVar2.vTransform);
                    Ant2.FUN_50F0(ref pVar2.vTransform.rotation, DAT_C0);
                    pVar3 = LevelManager.instance.FUN_4E128(screen, 53, 0);
                    pVar3.flags |= 0x10;
                    pVar3.FUN_2D114(pVar3.screen, ref pVar3.vTransform);
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 66, pVar3.vTransform.position);
                    GameManager.instance.FUN_1E30C(iVar4, 0xe3d);
                    GameObject obj = new GameObject();
                    mVar5 = obj.AddComponent<Meteorite3>();
                    mVar5.screen = pVar2.vTransform.position;
                    mVar5.flags = 0xa0;
                    mVar5.FUN_3066C();
                    FUN_30BA8();
                    FUN_4DC94();
                }
            }
        }
        else
        {
            if (arg1 == 8)
                FUN_32B90((uint)arg2);
            else
            {
                if (arg1 == 9 && arg2 != 0)
                    GameManager.instance.FUN_309A0(this);
            }
        }

        return 0;
    }
}
