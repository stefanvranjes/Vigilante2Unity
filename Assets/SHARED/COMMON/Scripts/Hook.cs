using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_934 (TOWTRUCK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 0)
            FUN_42330(arg2);

        return 0;
    }
    
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;
        int iVar2;
        ConfigContainer ccVar2;
        VigObject oVar2;
        Hook2 puVar3;
        int iVar4;
        VigObject oVar4;
        ConfigContainer ccVar4;
        Vehicle vVar5;
        VigObject oVar5;
        BufferedBinaryReader brVar5;
        VigTransform auStack112;
        Vector3Int auStack64;
        Vector3Int auStack48;
        Vector3Int local_28;
        Vector3Int local_50;


        if (arg1 == 1)
        {
            type = 3;
            maxHalfHealth = 6;
            flags |= 0x4000;
            oVar4 = vData.ini.FUN_2C17C(2, typeof(VigObject), 8);
            uVar1 = 0;

            if (oVar4 != null)
            {
                ccVar2 = FUN_2C5F4(0x8000);
                Utilities.FUN_2CA94(this, ccVar2, oVar4);
                Utilities.ParentChildren(this, this);
                oVar4.FUN_30BF0();
                PDAT_78 = oVar4;
                uVar1 = 0;
            }
        }
        else
        {
            if (arg1 == 0)
            {
                FUN_42330(arg2);
                uVar1 = 0;
            }
            else
            {
                if (arg1 == 12)
                {
                    vVar5 = (Vehicle)arg2;

                    if (vVar5.target == null || (flags & 0x8000000) != 0 ||
                        (vVar5.target.flags & 0x4000) == 0)
                    {
                        //iVar2 = GameManager.instance.FUN_1DD9C();
                        //GameManager.instance.FUN_1E14C(iVar2, GameManager.instance.DAT_C2C, 1);
                        return 0;
                    }
                    else
                    {
                        vVar5.tags = 3;
                        vVar5.DAT_58 += 0xa0000;
                        oVar2 = PDAT_78.FUN_2CCBC();
                        GameManager.instance.FUN_308C4(oVar2);
                        DAT_19 = 1;
                        PDAT_78 = null;
                        maxHalfHealth--;
                        puVar3 = vData.ini.FUN_2C17C(1, typeof(Hook2), 8) as Hook2;
                        puVar3.DAT_80 = vVar5.target;
                        ccVar4 = FUN_2C5F4(0x8000);
                        puVar3.DAT_A4 = ccVar4;
                        auStack112 = GameManager.instance.FUN_2CEAC(this, ccVar4);
                        puVar3.DAT_84 = this;
                        brVar5 = puVar3.DAT_80.vCollider.reader;
                        puVar3.physics1.W = (brVar5.ReadInt32(4) + brVar5.ReadInt32(16)) / 2;
                        puVar3.physics2.X = brVar5.ReadInt32(20);
                        puVar3.physics2.Y = brVar5.ReadInt32(24);
                        local_28 = Utilities.FUN_24148(puVar3.DAT_80.vTransform,
                            new Vector3Int(puVar3.physics1.W, puVar3.physics2.X, puVar3.physics2.Y));
                        local_50 = new Vector3Int();
                        local_50.x = local_28.x - auStack112.position.x;
                        local_50.y = local_28.y - auStack112.position.y;
                        local_50.z = local_28.z - auStack112.position.z;
                        auStack64 = Utilities.FUN_2426C(auStack112.rotation,
                            new Matrix2x4(local_50.x, local_50.y, local_50.z, 0));
                        Utilities.FUN_29FC8(auStack64, out auStack48);
                        puVar3.vTransform.rotation = Utilities.FUN_2A724(auStack48);
                        iVar2 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E580(iVar2, vData.sndList, 4, auStack112.position);
                        oVar5 = puVar3.child2;
                        oVar5.vTransform.rotation.V11 = 0x1000;
                        oVar5.vTransform.rotation.V00 = 0x1000;
                        oVar5.vTransform.rotation.V22 = 0x400;
                        puVar3.vTransform.position = ccVar4.v3_1;
                        iVar4 = Utilities.FUN_29E84(auStack64);
                        iVar4 /= 2;

                        if (0x70000 < iVar4)
                            iVar4 = 0x70000;

                        puVar3.physics2.Z = iVar4;
                        puVar3.physics2.W = 0x4000;
                        puVar3.tags = 0;
                        puVar3.DAT_A0_2 = (iVar4 - 0x4000) / 3;
                        Utilities.FUN_2CC48(this, puVar3);
                        Utilities.ParentChildren(this, this);
                        puVar3.FUN_30B78();
                        puVar3.FUN_30BF0();
                        GameManager.instance.FUN_30CB0(puVar3, 2);
                        oVar4 = vData.ini.FUN_2C17C(3, typeof(VigObject), 8);
                        puVar3.DAT_88 = oVar4;
                        oVar4.DAT_80 = puVar3;
                        ccVar2 = puVar3.FUN_2C5F4(0x8000);
                        Utilities.FUN_2CA94(puVar3, ccVar2, oVar4);
                        Utilities.ParentChildren(puVar3, puVar3);
                        flags |= 0x8000000;
                        iVar2 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E188(iVar2, vData.sndList, 2);
                    }

                    uVar1 = 780;

                    if (id < 0)
                        uVar1 = 480;
                }
                else
                {
                    uVar1 = 0;

                    if (arg1 == 13 && GameManager.instance.DAT_1084 == 0)
                    {
                        vVar5 = (Vehicle)arg2;
                        oVar5 = vVar5.target;
                        iVar4 = vVar5.vTransform.position.x - oVar5.vTransform.position.x;

                        if (iVar4 < 0)
                            iVar4 = -iVar4;

                        uVar1 = 0;

                        if (iVar4 < 0x40000)
                        {
                            iVar4 = vVar5.vTransform.position.y - oVar5.vTransform.position.y;

                            if (iVar4 < 0)
                                iVar4 = -iVar4;

                            uVar1 = 0;

                            if (iVar4 < 0x40000)
                            {
                                iVar4 = vVar5.vTransform.position.z - oVar5.vTransform.position.z;

                                if (iVar4 < 0)
                                    iVar4 = -iVar4;

                                uVar1 = (0x3ffff < iVar4 ? 1U : 0) ^ 1;
                            }
                        }
                    }
                }
            }
        }

        return uVar1;
    }
}
