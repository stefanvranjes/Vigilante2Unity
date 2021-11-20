using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public Bird DAT_94; //0x94

    //FUN_4EC (FRONTIER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar3;
        Ballistic puVar4;
        int iVar5;
        int iVar6;
        VigObject oVar6;
        int iVar9;
        int iVar10;
        Tornado puVar11;
        Vector3Int local_18;
        Vector3Int local_20;
        Vector3Int local_30;

        if (arg1 == 1)
            physics1.Z = 1525;
        else
        {
            if (arg1 == 0)
            {
                iVar9 = physics1.Z;
                iVar6 = vTransform.rotation.V02 * iVar9;

                if (iVar6 < 0)
                    iVar6 += 4095;

                iVar10 = vTransform.rotation.V12 * iVar9;
                vTransform.position.x += iVar6 >> 12;

                if (iVar10 < 0)
                    iVar10 += 4095;

                iVar6 = vTransform.rotation.V22 * iVar9;
                vTransform.position.y += iVar10 >> 12;

                if (iVar6 < 0)
                    iVar6 += 4095;

                vTransform.position.z += iVar6 >> 12;

                if (DAT_19 < 2)
                {
                    if (DAT_19 == 0)
                        local_18 = GameManager.instance.FUN_2CE50(DAT_94);
                    else
                    {
                        oVar6 = DAT_84;
                        local_18 = new Vector3Int();
                        local_18.x = oVar6.screen.x;
                        local_18.z = oVar6.screen.z;
                        local_18.y = oVar6.screen.y - 0x32000;
                    }

                    local_30 = new Vector3Int();
                    local_30.x = local_18.x - vTransform.position.x;
                    local_30.y = local_18.y - vTransform.position.y;
                    local_30.z = local_18.z - vTransform.position.z;
                    local_30 = Utilities.FUN_2426C(vTransform.rotation, new Matrix2x4(local_30.x, local_30.y, local_30.z, 0));
                    Utilities.FUN_29FC8(local_30, out local_20);
                    iVar10 = 6103;
                    iVar6 = local_30.z;

                    if (local_30.z < 0)
                        iVar6 = local_30.z + 15;

                    iVar6 >>= 4;

                    if (DAT_19 != 0)
                        iVar10 = 12207;

                    iVar5 = 1525;

                    if (1524 < iVar6)
                    {
                        iVar5 = iVar10;

                        if (iVar6 <= iVar10)
                            iVar5 = iVar6;
                    }

                    if (local_20.y < 1)
                    {
                        if (vr.x < 0x155)
                            vr.x += 22;
                    }
                    else
                    {
                        if (-0x155 < vr.x)
                            vr.x -= 22;
                    }

                    if (local_20.x < 0)
                    {
                        if (-0x155 < vr.z)
                            vr.z -= 22;
                    }
                    else
                    {
                        if (vr.z < 0x155)
                            vr.z += 22;
                    }

                    iVar6 = vr.z;

                    if (iVar6 < 0)
                        iVar6 += 15;

                    vr.y += iVar6 >> 4;

                    if (tags == 0)
                    {
                        if (iVar9 + 0x131 < iVar5)
                        {
                            tags = 1;
                            FUN_2C124(1);
                            Utilities.ParentChildren(this, this);
                        }

                        iVar6 = physics1.Z - 25;
                    }
                    else
                    {
                        if (iVar5 < iVar9 - 610)
                        {
                            tags = 0;
                            FUN_2C124(2);
                            Utilities.ParentChildren(this, this);
                        }

                        iVar6 = physics1.Z + 25;
                    }

                    physics1.Z = iVar6;

                    if (DAT_19 != 0)
                    {
                        if (arg2 != 0)
                            DAT_4A -= (ushort)arg2;

                        if (0xc00 < local_20.z && local_30.z < 0x32000)
                        {
                            iVar3 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E580(iVar3, vData.sndList, 4, vTransform.position);
                            vr.x = -0x155;
                            DAT_19 = 2;
                        }
                    }

                    ApplyRotationMatrix();
                    return 0;
                }

                if ((GameManager.instance.DAT_28 & 1) != 0)
                {
                    puVar4 = vData.ini.FUN_2C17C(3, typeof(Ballistic), 8) as Ballistic;
                    puVar4.flags = 0x24;
                    puVar4.vTransform = vTransform;
                    puVar4.FUN_305FC();
                }

                iVar6 = FUN_2CFBC(vTransform.position);

                if (iVar6 - 0xf000 < vTransform.position.y)
                {
                    puVar11 = vData.ini.FUN_2C17C(7, typeof(Tornado), 8) as Tornado;
                    Utilities.ParentChildren(puVar11, puVar11);
                    puVar11.type = 3;
                    puVar11.flags = 0x60000184;
                    puVar11.screen = vTransform.position;
                    puVar11.DAT_80 = DAT_80;
                    puVar11.DAT_84 = DAT_84;
                    sVar1 = vTransform.rotation.V02;
                    puVar11.physics1.W = 12207;
                    puVar11.physics1.Z = sVar1;
                    puVar11.physics2.X = vTransform.rotation.V22;
                    puVar11.FUN_3066C();
                    GameManager.instance.FUN_30CB0(puVar11, 330);
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar3, vData.sndList, 3, vTransform.position);
                    GameManager.instance.FUN_30CB0(this, 0);
                    return 0;
                }
            }
            else
            {
                if (arg1 != 2)
                    return 0;

                oVar6 = DAT_94;
                GameManager.instance.DAT_1084--;

                if (oVar6.maxHalfHealth == 0)
                {
                    oVar6.FUN_3A368();
                    return 0xffffffff;
                }

                oVar6.id += 240;
                DAT_19 = 0;
            }
        }

        return 0;
    }
}
