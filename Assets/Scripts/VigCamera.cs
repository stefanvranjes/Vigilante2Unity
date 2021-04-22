using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigCamera : MonoBehaviour
{
    public int paramUnk;
    public VigObject vObject;
    public VigObject target; //0x80
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
    void FixedUpdate()
    {
        if (paramUnk != 0)
        {
            if (paramUnk != 2)
                return;
        }

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
                int iVar = Utilities.FUN_2A27C(target.rotation) << 16 >> 16;
                local_28.x = GameManager.DAT_65C90[((iVar + DAT_92 & 0xFFF) << 2) / 2];
                local_28.y = -(ushort)GameManager.DAT_65C90[((DAT_90 & 0xFFF) << 2) / 2];
                local_28.z = GameManager.DAT_65C90[((iVar + DAT_92 & 0xFFF) << 2) / 2 + 1];
            }
            else
            {
                local_28.x = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2];
                local_28.y = -(ushort)GameManager.DAT_65C90[((DAT_90 & 0xFFF) << 2) / 2];
                local_28.z = GameManager.DAT_65C90[((DAT_92 & 0xFFF) << 2) / 2 + 1];
                local_28 = Utilities.ApplyMatrixSV(target.rotation, local_28);
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
        int local_20 = target.position.x - iVar10 - vObject.screen.x; //sp+18h
        long lVar2 = (long)((ulong)(uint)iVar7 * (uint)DAT_9C); //r2, r3
        iVar5 = iVar7 * (DAT_9C >> 31);
        iVar4 = DAT_9C * iVar8;
        iVar7 = (ushort)local_28.z << 16 >> 16;
        iVar8 = (ushort)local_28.z << 16 >> 31;
        iVar6 = (int)(lVar2 >> 32) + iVar5 + iVar4;
        int iVar11 = (int)((uint)lVar2 >> 12 | (uint)(iVar6 << 20)); //r2
        iVar6 = iVar6 >> 12;
        int local_1c = target.position.y - iVar11 - vObject.screen.y; //sp+1Ch
        long lVar3 = (long)((ulong)(uint)iVar7 * (uint)DAT_9C); //r2, r3
        iVar5 = iVar7 * (DAT_9C >> 31);
        iVar4 = DAT_9C * iVar8;
        iVar6 = (int)(lVar3 >> 32) + iVar5 + iVar4;
        iVar11 = (int)((uint)lVar3 >> 12 | (uint)(iVar6 << 20));
        iVar6 = iVar6 >> 12;
        int local_18 = target.position.z - iVar11 - vObject.screen.z; //sp+20h
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
            int iVar1 = GameManager.instance.terrain.FUN_1B750((uint)target.position.x, (uint)target.position.z);

            if (target.position.y < iVar1)
            {
                iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vObject.screen.x, (uint)vObject.screen.z);
                int iVar2 = iVar1 - 0x8000;

                if (vObject.screen.y < iVar1 - 0x8000)
                    iVar2 = vObject.screen.y;

                vObject.screen.y = iVar2;
            }
        }

        vObject.FUN_4BAFC(target.position);
        vObject.vr.x += (ushort)DAT_94;
        vObject.ApplyTransformation();
    }

    public void FUN_4B898()
    {

    }
}
