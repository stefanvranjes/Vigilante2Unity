using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigCamera : VigObject
{
    public short fieldOfView { get { return (short)maxHalfHealth; } } //0x1C
    public VigObject target; //0x80
    public Vector3Int DAT_84; //0x84
    public int DAT_9C; //0x9C
    public int DAT_98; //0x98
    public int DAT_A0_1; //0xA0
    public short DAT_90; //0x90
    public short DAT_92; //0x92
    public short DAT_94; //0x94

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4AFB8
    public override uint UpdateW(int arg1, int arg2)
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

        if (arg1 != 0)
        {
            if (arg1 != 2)
                return 0;

            Vehicle targetVehicle = (Vehicle)target;

            flags &= 0xebffffff;

            if (targetVehicle.vCamera == this)
                return 0;

            //FUN_30CB0
            targetVehicle.vCamera = this;
            return 0;
        }

        uVar5 = flags;

        if ((uVar5 & 0x10000000) != 0)
        {
            //...
        }

        if ((uVar5 & 0x4000000) != 0)
        {
            screen.x += DAT_84.x;
            screen.y += DAT_84.y;
            screen.z += DAT_84.z;
            goto LAB_4B694;
        }

        iVar6 = DAT_A0_1;

        if ((uVar5 & 0x2000000) == 0)
        {
            if ((uVar5 & 0x20000000) == 0)
            {
                sVar3 = Utilities.FUN_2A27C(target.vTransform.rotation);
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
                local_28 = Utilities.ApplyMatrixSV(target.vTransform.rotation, local_28);
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
            puVar4 = GameManager.DAT_65C90[((ushort)DAT_92 & 0xfff) * 2 + 1];
            local_28.y = -sVar3;
            local_28.z = puVar4;
        }

        local_20 = new Vector3Int();
        uVar5 = (uint)((int)((uint)(ushort)local_28.x << 16) >> 16);
        uVar7 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar5 * uVar7);
        uVar8 = (uint)((int)((uint)(ushort)local_28.y << 16) >> 16);
        local_20.x = (target.vTransform.position.x -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar5 * ((int)uVar7 >> 31) +
                     (int)uVar7 * ((int)((uint)(ushort)local_28.x << 16) >> 31)) * 0x100000)) -
                     screen.x;
        uVar5 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar8 * uVar5);
        uVar7 = (uint)((int)((uint)(ushort)local_28.z << 16) >> 16);
        local_20.y = (target.vTransform.position.y -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar8 * ((int)uVar5 >> 31) +
                     (int)uVar5 * ((int)((uint)(ushort)local_28.y << 16) >> 31)) * 0x100000)) -
                     screen.y;
        uVar5 = (uint)DAT_9C;
        lVar1 = (long)((ulong)uVar7 * uVar5);
        local_20.z = (target.vTransform.position.z -
                     (int)((uint)lVar1 >> 12 |
                     (uint)((int)((ulong)lVar1 >> 32) + (int)uVar7 * ((int)uVar5 >> 31) +
                     (int)uVar5 * ((int)((uint)(ushort)local_28.z << 16) >> 31)) * 0x100000)) -
                     screen.z;
        uVar5 = (uint)Utilities.FUN_29E84(local_20);
        iVar10 = (int)uVar5 >> 31;

        if (iVar6 < 0)
        {
            screen.x += local_20.x;
            screen.y += local_20.y;
            screen.z += local_20.z;
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
                if ((flags & 0x8000000) == 0)
                {
                    screen.x +=
                        (int)((uint)((ulong)(uint)local_20.x * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.x * uVar5 >> 32) + local_20.x * iVar6 +
                        (int)uVar5 * (local_20.x >> 31)) * 0x100000;
                    screen.y +=
                        (int)((uint)((ulong)(uint)local_20.y * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.y * uVar5 >> 32) + local_20.y * iVar6 +
                        (int)uVar5 * (local_20.y >> 31)) * 0x100000;
                    screen.z +=
                        (int)((uint)((ulong)(uint)local_20.z * uVar5) >> 12) |
                        ((int)((ulong)(uint)local_20.z * uVar5 >> 32) + local_20.z * iVar6 +
                        (int)uVar5 * (local_20.z >> 31)) * 0x100000;
                    goto LAB_4B694;
                }
            }
            else
            {
                if ((flags & 0x8000000) != 0)
                    flags &= 0xf7ffffff;
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

            screen.x += iVar6;
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

            screen.y += iVar6;
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

            screen.z += iVar6;
        }

        LAB_4B694:
        if ((flags & 0x1000000) != 0)
        {
            iVar6 = VigTerrain.instance.FUN_1B750((uint)target.vTransform.position.x, (uint)target.vTransform.position.z);

            if (target.vTransform.position.y < iVar6)
            {
                iVar9 = screen.y;
                iVar10 = VigTerrain.instance.FUN_1B750((uint)screen.x, (uint)screen.z);
                iVar6 = iVar10 - 0x8000;

                if (iVar9 < iVar10 - 0x8000)
                    iVar6 = iVar9;

                screen.y = iVar6;
            }
        }

        FUN_4BAFC(target.vTransform.position);
        vr.x += DAT_94;
        ApplyTransformation();
        return 0;
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

        DAT_9C = target.DAT_58 + iVar1;
        DAT_98 = 11444;
        DAT_A0_1 = 204800;
    }

    public void FUN_4BC0C()
    {
        long lVar1;
        short sVar2;
        int iVar3;
        uint uVar4;
        uint uVar5;
        VigObject oVar6;
        int iVar7;
        uint uVar8;

        sVar2 = Utilities.FUN_2A27C(target.vTransform.rotation);
        uVar4 = (uint)DAT_9C;
        iVar3 = ((sVar2 + DAT_92) & 0xfff) * 2;
        uVar5 = (uint)((ushort)GameManager.DAT_65C90[iVar3] << 16 >> 16);
        lVar1 = (long)((ulong)uVar4 * uVar5);
        screen.x = target.vTransform.position.x -
                   (int)((uint)lVar1 >> 12 |
                   (uint)((int)((ulong)lVar1 >> 32) +
                   (int)uVar4 * ((ushort)GameManager.DAT_65C90[iVar3] << 16 >> 31) +
                   (int)uVar5 * ((int)uVar4 >> 31)) * 0x100000);
        uVar8 = (uint)((long)(int)uVar4 * -GameManager.DAT_65C90[(DAT_90 & 0xfff) * 2]);
        iVar7 = (int)((ulong)((long)(int)uVar4 * -GameManager.DAT_65C90[(DAT_90 & 0xfff) * 2]) >> 32);
        uVar4 = (uint)DAT_9C;
        screen.y = target.vTransform.position.y - (int)(uVar8 >> 12 | (uint)iVar7 << 20);
        uVar5 = (uint)((ushort)GameManager.DAT_65C90[iVar3 + 1] << 16 >> 16);
        lVar1 = (long)((ulong)uVar4 * uVar5);
        oVar6 = target;
        screen.z = target.vTransform.position.z -
                   (int)((uint)lVar1 >> 12 |
                   (uint)((int)((ulong)lVar1 >> 32) +
                   (int)uVar4 * ((ushort)GameManager.DAT_65C90[iVar3 + 1] << 16 >> 31) +
                   (int)uVar5 * ((int)uVar4 >> 31)) * 0x100000);
        FUN_4BAFC(oVar6.vTransform.position);
        vr.x += DAT_94;
        ApplyTransformation();
    }
}
