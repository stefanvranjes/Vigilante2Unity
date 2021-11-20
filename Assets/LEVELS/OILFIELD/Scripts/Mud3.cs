using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public int DAT_80_2; //0x80
    public int DAT_84_2; //0x84
    public Vehicle DAT_88; //0x88
    public int DAT_8C; //0x8C
    public Vector3Int[] DAT_90 = new Vector3Int[16]; //0x90

    //FUN_574 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        int iVar1;
        int iVar2;
        Wheel wVar2;
        Vector3Int puVar3;
        uint uVar4;
        int unaff_s5;
        Ballistic puVar5;
        int iVar6;
        Vehicle vVar7;
        int iVar9;
        int local_24;
        VigTransform auStack88;
        Vector3Int auStack48;
        Vector3Int auStack40;
        Vector3Int auStack56;

        if (arg1 == 2)
        {
            if (tags == 0)
            {
                if (DAT_8C == 0)
                {
                    iVar1 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar1, LevelManager.instance.xobfList[42].sndList, 0, DAT_88.vTransform.position);
                }

                unaff_s5 = 0; //not in the original code
                iVar9 = 2;
                auStack88 = DAT_88.FUN_2AE18();
                iVar6 = DAT_8C;
                vVar7 = DAT_88;
                DAT_90[iVar6] = vVar7.vTransform.position;
                DAT_8C = iVar6 + 1;

                do
                {
                    wVar2 = DAT_88.wheels[iVar9];

                    if (wVar2 != null)
                    {
                        auStack40 = Utilities.FUN_24148(auStack88, wVar2.vTransform.position);
                        local_24 = 0;
                        unaff_s5 = Utilities.FUN_29FC8(auStack40, out auStack56);

                        if (0xe480 < unaff_s5)
                        {
                            puVar3 = GameManager.instance.FUN_2CE50(wVar2);
                            auStack48 = GameManager.instance.terrain.FUN_1BB50(puVar3.x, puVar3.z);
                            auStack48 = Utilities.VectorNormal(auStack48);
                            uVar4 = GameManager.FUN_2AC5C();
                            iVar1 = 17;

                            if ((uVar4 & 1) != 0)
                                iVar1 = 16;

                            puVar5 = LevelManager.instance.xobfList[42].ini.FUN_2C17C((ushort)iVar1, typeof(Ballistic), 8) as Ballistic;
                            puVar5.type = 7;
                            puVar5.flags = 0x24;
                            Utilities.FUN_2A85C(ref puVar5.vTransform.rotation, auStack56, auStack48);
                            puVar5.vTransform.position.x = puVar3.x;
                            puVar5.vTransform.position.z = puVar3.z;
                            iVar1 = GameManager.instance.terrain.FUN_1B750((uint)puVar3.x, (uint)puVar3.z);
                            puVar5.vTransform.position.y = iVar1;
                            puVar5.FUN_305FC();
                        }
                    }

                    iVar9++;
                } while (iVar9 < 4);

                if (DAT_8C < 16)
                {
                    if (unaff_s5 == 0)
                        iVar2 = 60;
                    else
                    {
                        iVar2 = 6;

                        if (6 < 0x640000 / unaff_s5)
                            iVar2 = 0x640000 / unaff_s5;
                    }

                    GameManager.instance.FUN_30CB0(this, iVar2);
                    uVar1 = 0;
                }
                else
                {
                    DAT_88 = null;
                    tags = 1;
                    GameManager.instance.FUN_30CB0(this, 300);
                    uVar1 = 0;
                }
            }
            else
            {
                if (tags == 1)
                {
                    GameManager.instance.FUN_300B8(((Oilfield)LevelManager.instance.level).DAT_84_2[DAT_84_2], this);
                    DAT_8C = 0;
                    Destroy(gameObject);
                    uVar1 = 0xffffffff;
                }
                else
                    uVar1 = 0;
            }
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
