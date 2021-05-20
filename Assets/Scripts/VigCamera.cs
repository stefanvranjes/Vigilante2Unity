using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigCamera : MonoBehaviour
{
    public VigObject vObject;
    public short fieldOfView; //0x1C
    public Vehicle target; //0x80
    public Vector3Int DAT_84; //0x84
    public int DAT_9C; //0x9C
    public int DAT_98; //0x98
    public int DAT_A0; //0xA0
    public short DAT_90; //0x90
    public short DAT_92; //0x92
    public short DAT_94; //0x94

    private void Awake()
    {
        vObject = GetComponent<VigObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {

        if ((vObject.flags & 0x10000000) != 0)
        {

        }

        if ((vObject.flags & 0x4000000) != 0)
        {

        }

        Vector3Int local_28 = new Vector3Int(0, 0, 0); //sp+10h

        if ((vObject.flags & 0x2000000) == 0)
        {
            if ((vObject.flags & 0x20000000) == 0)
            {
                int iVar = Utilities.FUN_2A27C(target.vObject.vTransform.rotation) << 16 >> 16;
                local_28.x = GameManager.DAT_65C90[((iVar + DAT_92 & 0xFFF) << 2) / 2];
                local_28.y = -(ushort)GameManager.DAT_65C90[((DAT_90 & 0xFFF) << 2) / 2];
                local_28.z = GameManager.DAT_65C90[((iVar + DAT_92 & 0xFFF) << 2) / 2 + 1];
            }
            else
            {
                local_28.x = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2];
                local_28.y = -(ushort)GameManager.DAT_65C90[((DAT_90 & 0xFFF) << 2) / 2];
                local_28.z = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2 + 1];
                local_28 = Utilities.ApplyMatrixSV(target.vObject.vTransform.rotation, local_28);
            }
        }
        else
        {
            int iVar1 = DAT_9C + 0x8F0; //r4
            int iVar2 = 0xAF000; //r3

            if (iVar1 < iVar2)
                iVar2 = iVar1;

            DAT_9C = iVar2;
            DAT_92 = (short)((ushort)DAT_92 + 8);
            local_28.x = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2];
            local_28.y = -(ushort)GameManager.DAT_65C90[((DAT_90 & 0xFFF) << 2) / 2];
            local_28.z = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2 + 1];
        }

        int iVar3 = (ushort)local_28.x << 16 >> 16; //r4
        int iVar4 = (ushort)local_28.x << 16 >> 31; //r5
        long lVar1 = (long)((ulong)(uint)iVar3 * (uint)DAT_9C); //r6, r7
        int iVar5 = iVar3 * (DAT_9C >> 31); //r11
        int iVar6 = DAT_9C * iVar4; //r3
        int iVar7 = (ushort)local_28.y << 16 >> 16; //r8
        int iVar8 = (ushort)local_28.y << 16 >> 31; //r9
        int iVar9 = (int)(lVar1 >> 32) + iVar5 + iVar6; //r7
        int iVar10 = (int)((uint)lVar1 >> 12 | (uint)(iVar9 << 20)); //r6
        iVar9 = iVar9 >> 12;
        int local_20 = target.vObject.vTransform.position.x - iVar10 - vObject.screen.x; //sp+18h
        long lVar2 = (long)((ulong)(uint)iVar7 * (uint)DAT_9C); //r2, r3
        iVar5 = iVar7 * (DAT_9C >> 31);
        iVar4 = DAT_9C * iVar8;
        iVar7 = (ushort)local_28.z << 16 >> 16;
        iVar8 = (ushort)local_28.z << 16 >> 31;
        iVar6 = (int)(lVar2 >> 32) + iVar5 + iVar4;
        int iVar11 = (int)((uint)lVar2 >> 12 | (uint)(iVar6 << 20)); //r2
        iVar6 = iVar6 >> 12;
        int local_1c = target.vObject.vTransform.position.y - iVar11 - vObject.screen.y; //sp+1Ch
        long lVar3 = (long)((ulong)(uint)iVar7 * (uint)DAT_9C); //r2, r3
        iVar5 = iVar7 * (DAT_9C >> 31);
        iVar4 = DAT_9C * iVar8;
        iVar6 = (int)(lVar3 >> 32) + iVar5 + iVar4;
        iVar11 = (int)((uint)lVar3 >> 12 | (uint)(iVar6 << 20));
        iVar6 = iVar6 >> 12;
        int local_18 = target.vObject.vTransform.position.z - iVar11 - vObject.screen.z; //sp+20h
        iVar10 = Utilities.FUN_29E84(new Vector3Int(local_20, local_1c, local_18));
        iVar9 = iVar10 >> 31;

        if (DAT_A0 < 0)
        {
            vObject.screen.x += local_20;
            vObject.screen.y += local_1c;
            vObject.screen.z += local_18;
        }
        else
        {
            bool bVar1 = false;
            int iVar = DAT_A0 + (int)((uint)DAT_A0 >> 31);
            iVar6 = iVar >> 1;
            iVar4 = iVar >> 31;
            iVar3 = iVar6;
            int iVar12 = 0; //r18
            int iVar13 = 0; //r19

            if (iVar4 < iVar9 || (iVar9 == iVar4 && (uint)iVar3 < (uint)iVar10))
            {
                iVar = (uint)iVar10 < (uint)iVar3 ? 1 : 0;
                iVar3 = iVar10 - iVar3;
                iVar4 = iVar9 - iVar4 - iVar << 12;
                iVar4 = (int)((uint)iVar4 | (uint)iVar3 >> 20);
                iVar3 = iVar3 << 12;
                long lVar4 = Utilities.Divdi3(iVar3, iVar4, iVar10, iVar9);
                iVar13 = (int)(lVar4 >> 32);
                iVar12 = (int)lVar4;

                if ((0 < lVar4 >> 32) || (lVar4 >> 32 == 0 && (int)lVar4 >= 256))
                    bVar1 = true;
            }

            if (bVar1)
            {
                if ((vObject.flags & 0x8000000) == 0)
                {
                    vObject.screen.x+=
                        (int)((uint)((ulong)(uint)local_20 * (uint)iVar12) >> 12 | 
                        (uint)((int)((ulong)(uint)local_20 * (uint)iVar12 >> 32) + local_20 * iVar13 + 
                        iVar12 * (local_20 >> 31)) * 0x100000);
                    vObject.screen.y +=
                        (int)((uint)((ulong)(uint)local_1c * (uint)iVar12) >> 12 |
                        (uint)((int)((ulong)(uint)local_1c * (uint)iVar12 >> 32) + local_1c * iVar13 +
                        iVar12 * (local_1c >> 31)) * 0x100000);
                    vObject.screen.z +=
                        (int)((uint)((ulong)(uint)local_18 * (uint)iVar12) >> 12 |
                        (uint)((int)((ulong)(uint)local_18 * (uint)iVar12 >> 32) + local_18 * iVar13 +
                        iVar12 * (local_18 >> 31)) * 0x100000);
                    goto LAB_4B694;
                }
            }
            else
            {
                if ((vObject.flags & 0x8000000) != 0)
                    vObject.flags &= 0xf7ffffff;
            }

            int iVar1 = local_20;

            if (local_20 < 0)
                iVar1 += 7;

            iVar4 = iVar1 >> 3;
            int iVar2 = -DAT_98;

            if (-DAT_98 <= iVar4)
            {
                iVar2 = DAT_98;

                if (iVar4 <= DAT_98)
                    iVar2 = iVar4;
            }

            vObject.screen.x += iVar2;
            iVar1 = local_1c;

            if (local_1c < 0)
                iVar1 += 15;

            iVar4 = iVar1 >> 4;
            iVar2 = -DAT_98;

            if (-DAT_98 <= iVar4)
            {
                iVar2 = DAT_98;

                if (iVar4 <= DAT_98)
                    iVar2 = iVar4;
            }

            vObject.screen.y += iVar2;
            iVar1 = local_18;

            if (local_18 < 0)
                iVar1 += 7;

            iVar4 = iVar1 >> 3;
            iVar2 = -DAT_98;

            if (-DAT_98 <= iVar4)
            {
                iVar2 = DAT_98;

                if (iVar4 <= DAT_98)
                    iVar2 = iVar4;
            }

            vObject.screen.z += iVar2;
        }

        LAB_4B694:
        if ((vObject.flags & 0x1000000) != 0)
        {
            int iVar1 = GameManager.instance.terrain.FUN_1B750
                ((uint)target.vObject.vTransform.position.x, (uint)target.vObject.vTransform.position.z);

            if (target.vObject.vTransform.position.y < iVar1)
            {
                iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vObject.screen.x, (uint)vObject.screen.z);
                int iVar2 = iVar1 - 0x8000;

                if (vObject.screen.y < iVar1 - 0x8000)
                    iVar2 = vObject.screen.y;

                vObject.screen.y = iVar2;
            }
        }

        vObject.FUN_4BAFC(target.vObject.vTransform.position);
        vObject.vr.x += (ushort)DAT_94;
        vObject.ApplyTransformation();
    }*/

    private void FixedUpdate()
    {
        FUN_4AFB8(0);
    }

    private void FUN_4AFB8(int param2)
    {
        long lVar1;
        bool bVar2;
        short sVar3;
        int puVar4;
        uint uVar5;
        uint uVar7;
        uint uVar8;
        int iVar6;
        int iVar9;
        int iVar10;
        ulong uVar11;
        Vector3Int local_28;
        Vector3Int local_20;

        if (param2 != 0)
        {
            if (param2 != 2)
                return;
            
            vObject.flags &= 0xebffffff;

            if (target.vCamera == this)
                return;

            //FUN_30CB0
            target.vCamera = this;
            return;
        }

        uVar5 = vObject.flags;

        if ((uVar5 & 0x10000000) != 0)
        {
            //...
        }

        if ((uVar5 & 0x4000000) != 0)
        {
            vObject.screen.x += DAT_84.x;
            vObject.screen.y += DAT_84.y;
            vObject.screen.z += DAT_84.z;
            goto LAB_4B694;
        }

        iVar6 = DAT_A0;

        if ((uVar5 & 0x2000000) == 0)
        {
            if ((uVar5 & 0x20000000) == 0)
            {
                sVar3 = Utilities.FUN_2A27C(target.vObject.vTransform.rotation);
                puVar4 = (int)((uint)(sVar3 + DAT_92) & 0xfffU) * 2;
                local_28 = new Vector3Int();
                local_28.x = GameManager.DAT_65C90[puVar4];
                sVar3 = GameManager.DAT_65C90[((ushort)DAT_90 & 0xfff) * 2];
                local_28.y = -sVar3;
                local_28.z = GameManager.DAT_65C90[puVar4 + 1];
            }
            else
            {
                local_28 = new Vector3Int(
                    GameManager.DAT_65C90[((ushort)DAT_92 & 0xfff) * 2],
                    -GameManager.DAT_65C90[((ushort)DAT_90 & 0xfff) * 2],
                    GameManager.DAT_65C90[((ushort)DAT_92 & 0xfff) * 2 + 1]);
                local_28 = Utilities.ApplyMatrixSV(target.vObject.vTransform.rotation, local_28);
            }
        }
        else
        {
            iVar9 = DAT_9C + 2288;
            iVar10 = 716800;

            if (iVar9 < 716800)
                iVar10 = iVar9;

            DAT_9C = iVar10;
            uVar5 = (uint)(ushort)DAT_92 + 8;
            DAT_92 = (short)uVar5;
            local_28 = new Vector3Int();
            local_28.x = GameManager.DAT_65C90[(uVar5 & 0xfff) * 2];
            sVar3 = GameManager.DAT_65C90[((ushort)DAT_90 & 0xfff) * 2];
            puVar4 = GameManager.DAT_65C90[((ushort)DAT_92 & 0xfff) * 2];
            local_28.y = -sVar3;
            local_28.z = GameManager.DAT_65C90[puVar4 + 1];
        }

        local_20 = new Vector3Int();
        uVar5 = (uint)((int)((uint)(ushort)local_28.x << 16) >> 16);
        uVar7 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar5 * uVar7);
        uVar8 = (uint)((int)((uint)(ushort)local_28.y << 16) >> 16);
        local_20.x = (target.vObject.vTransform.position.x -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar5 * ((int)uVar7 >> 31) +
                     (int)uVar7 * ((int)((uint)(ushort)local_28.x << 16) >> 31)) * 0x100000)) -
                     vObject.screen.x;
        uVar5 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar8 * uVar5);
        uVar7 = (uint)((int)((uint)(ushort)local_28.z << 16) >> 16);
        local_20.y = (target.vObject.vTransform.position.y -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar8 * ((int)uVar5 >> 31) +
                     (int)uVar5 * ((int)((uint)(ushort)local_28.y << 16) >> 31)) * 0x100000)) -
                     vObject.screen.y;
        uVar5 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar7 * uVar5);
        local_20.z = (target.vObject.vTransform.position.z -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar7 * ((int)uVar5 >> 31) +
                     (int)uVar5 * ((int)((uint)(ushort)local_28.z << 16) >> 31)) * 0x100000)) -
                     vObject.screen.z;
        uVar5 = (uint)Utilities.FUN_29E84(local_20);
        iVar10 = (int)uVar5 >> 31;

        if (iVar6 < 0)
        {
            vObject.screen.x += local_20.x;
            vObject.screen.y += local_20.y;
            vObject.screen.z += local_20.z;
        }
        else
        {
            bVar2 = false;
            uVar7 = (uint)(iVar6 / 2);
            iVar6 = iVar6 - (iVar6 >> 31) >> 31;
            uVar11 = 0;

            if (iVar6 < iVar10 || 
                (iVar10 == iVar6 && uVar7 < uVar5))
            {
                uVar11 = (ulong)Utilities.Divdi3((int)((uVar5 - uVar7) * 0x1000),
                                                (int)(((uint)(iVar10 - iVar6) - (uint)(uVar5 < uVar7 ? 1 : 0)) * 4096 | uVar5 - uVar7 >> 20), 
                                                (int)uVar5, iVar10);
                if (0 < (int)(uVar11 >> 32) || ((int)(uVar11 >> 32) == 0 && !((int)uVar11 <= 256)))
                    bVar2 = true;
            }

            iVar6 = (int)(uVar11 >> 32);
            uVar5 = (uint)uVar11;

            if (bVar2)
            {
                if ((vObject.flags & 0x8000000) == 0)
                {
                    vObject.screen.x +=
                        (int)((uint)((ulong)(uint)local_20.x * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.x * uVar5 >> 32) + local_20.x * iVar6 +
                        (int)uVar5 * (local_20.x >> 31)) * 0x100000;
                    vObject.screen.y +=
                        (int)((uint)((ulong)(uint)local_20.y * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.y * uVar5 >> 32) + local_20.y * iVar6 +
                        (int)uVar5 * (local_20.y >> 31)) * 0x100000;
                    vObject.screen.z +=
                        (int)((uint)((ulong)(uint)local_20.z * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.z * uVar5 >> 32) + local_20.z * iVar6 +
                        (int)uVar5 * (local_20.z >> 31)) * 0x100000;
                    goto LAB_4B694;
                }
            }
            else
            {
                if ((vObject.flags & 0x8000000) != 0)
                    vObject.flags &= 0xf7ffffff;
            }

            uVar5 = (uint)local_20.x;

            if (local_20.x < 0)
                uVar5 = (uint)(local_20.x + 7);

            iVar9 = DAT_98;
            iVar10 = (int)uVar5 >> 3;
            iVar6 = -iVar9;

            if (-iVar9 <= iVar10)
            {
                iVar6 = iVar9;

                if (iVar10 <= iVar9)
                    iVar6 = iVar10;
            }

            vObject.screen.x += iVar6;
            uVar5 = (uint)local_20.y;

            if (local_20.y < 0)
                uVar5 = (uint)(local_20.y + 15);

            iVar9 = DAT_98;
            iVar10 = (int)uVar5 >> 4;
            iVar6 = -iVar9;

            if (-iVar9 <= iVar10)
            {
                iVar6 = iVar9;

                if (iVar10 <= iVar9)
                    iVar6 = iVar10;
            }

            vObject.screen.y += iVar6;
            uVar5 = (uint)local_20.z;

            if (local_20.z < 0)
                uVar5 = (uint)(local_20.z + 7);

            iVar9 = DAT_98;
            iVar10 = (int)uVar5 >> 3;
            iVar6 = -iVar9;

            if (-iVar9 <= iVar10)
            {
                iVar6 = iVar9;

                if (iVar10 <= iVar9)
                    iVar6 = iVar10;
            }

            vObject.screen.z += iVar6;
        }

        LAB_4B694:
        if ((vObject.flags & 0x1000000) != 0)
        {
            iVar6 = VigTerrain.instance.FUN_1B750((uint)target.vObject.vTransform.position.x, (uint)target.vObject.vTransform.position.z);

            if (target.vObject.vTransform.position.y < iVar6)
            {
                iVar9 = vObject.screen.y;
                iVar10 = VigTerrain.instance.FUN_1B750((uint)vObject.screen.x, (uint)vObject.screen.z);
                iVar6 = iVar10 - 0x8000;

                if (iVar9 < iVar10 - 0x8000)
                    iVar6 = iVar9;

                vObject.screen.y = iVar6;
            }
        }

        vObject.FUN_4BAFC(target.vObject.vTransform.position);
        vObject.vr.x += DAT_94;
        vObject.ApplyTransformation();
    }

    private void FUN_4B820()
    {
        if (GameManager.instance.screenMode == _SCREEN_MODE.Horizontal)
        {
            DAT_90 = -160;
            DAT_94 = 80;
            return;
        }

        if (GameManager.instance.screenMode < _SCREEN_MODE.Vertical)
        {
            if (GameManager.instance.screenMode != _SCREEN_MODE.Whole)
                return;
        }
        else
        {
            if (GameManager.instance.screenMode != _SCREEN_MODE.Vertical)
            {
                if (GameManager.instance.screenMode == _SCREEN_MODE.Unknown)
                {
                    DAT_90 = -192;
                    DAT_94 = 128;
                    return;
                }

                return;
            }
        }

        DAT_90 = -256;
        DAT_94 = 160;
    }

    public void FUN_4B898()
    {
        int iVar1;

        FUN_4B820();
        DAT_92 = 0;

        if (GameManager.instance.screenMode == _SCREEN_MODE.Whole)
            iVar1 = 0x10000;
        else
        {
            if (GameManager.instance.screenMode == _SCREEN_MODE.Unknown)
                iVar1 = 0x20800;
            else
                iVar1 = 0x14000;
        }

        DAT_9C = target.vObject.DAT_58 + iVar1;
        DAT_98 = 11444;
        DAT_A0 = 204800;
    }
}
