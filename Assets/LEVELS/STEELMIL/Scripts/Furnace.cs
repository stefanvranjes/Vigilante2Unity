using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Destructible
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
        bool bVar5;

        bVar5 = FUN_32CF0(hit);

        if (bVar5)
            FUN_30C68();

        return 0;
    }

    //0x144 (STEELMIL.DLL)
    public static byte[] DAT_144 = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0xD8, 0xFF, 0xFF, 0x00, 0xD8, 0xFF, 0xFF,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x28, 0x00, 0x00, 0x00, 0x00, 0x28, 0x00,
                                                 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 };

    //FUN_1C20 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        int iVar4;
        bool bVar5;
        int iVar5;
        VigObject oVar5;
        Furnace2 fVar5;

        if (arg1 == 2)
        {
            if (SteelMil.instance.DAT_4600 < 3)
            {
                ppiVar3 = GameManager.instance.worldObjs;

                for (int i = 0; i < ppiVar3.Count; i++)
                {
                    ppiVar2 = ppiVar3[i];
                    oVar5 = ppiVar2.vObject;

                    if (oVar5.type == 2 && oVar5.maxHalfHealth != 0)
                    {
                        iVar5 = Utilities.FUN_29F6C(screen, oVar5.vTransform.position);

                        if (iVar5 < 0x3c000)
                        {
                            fVar5 = FUN_1B30();
                            GameManager.instance.FUN_30CB0(fVar5, 60);
                            iVar4 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E628(iVar4, GameManager.instance.DAT_C2C, 67, fVar5.vTransform.position);
                            break;
                        }
                    }
                }

                GameManager.instance.FUN_30CB0(this, 240);
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 1)
                {
                    iVar5 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, iVar5 * 240 >> 15);
                }
            }
            else
            {
                if (arg1 != 8)
                    return 0;

                bVar5 = FUN_32B90((uint)arg2);

                if (bVar5)
                    FUN_30C68();
            }
        }

        return 0;
    }

    //FUN_1B30 (STEELMIL.DLL)
    private Furnace2 FUN_1B30()
    {
        XOBF_DB pcVar1;
        Furnace2 ppcVar2;
        ConfigContainer ccVar3;
        int iVar4;
        VigTransform auStack32;

        GameObject obj = new GameObject();
        ppcVar2 = obj.AddComponent<Furnace2>();
        ccVar3 = FUN_2C5F4(0x8000);
        auStack32 = Utilities.FUN_2C77C(ccVar3);
        ppcVar2.vTransform = Utilities.CompMatrixLV(vTransform, auStack32);
        ppcVar2.type = 8;
        ppcVar2.maxHalfHealth = 5;
        ppcVar2.flags |= 0x184;
        ppcVar2.vCollider = new VigCollider(DAT_144);
        pcVar1 = LevelManager.instance.xobfList[19];
        ppcVar2.physics2.M3 = 21;
        ppcVar2.physics1.M1 = 3;
        ppcVar2.DAT_98 = pcVar1;
        iVar4 = GameManager.instance.terrain.FUN_1B750((uint)ppcVar2.vTransform.position.x, (uint)ppcVar2.vTransform.position.z);
        ppcVar2.physics1.Y = 0;
        ppcVar2.screen.y = iVar4 - ppcVar2.vTransform.position.y;
        ppcVar2.physics1.Z = -0x200;
        ppcVar2.physics1.W = 0xc00;
        ppcVar2.FUN_305FC();
        ppcVar2.DAT_58 = 0x40000;
        ppcVar2.vTransform.rotation.V00 = 0x1000;
        ppcVar2.vTransform.rotation.V11 = 0x1000;
        ppcVar2.vTransform.rotation.V22 = 0x1000;
        return ppcVar2;
    }
}
