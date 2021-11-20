using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RSEG_DB : MonoBehaviour
{
    public JUNC_DB[] DAT_00; //0x00
    public ushort DAT_08; //0x08
    public ushort DAT_0A; //0x0A
    public ushort DAT_0C; //0x0C
    public int[] DAT_10; //0x10
    public int[] DAT_14; //0x14
    public int DAT_20; //0x20
    public int DAT_24; //0x24
    public int DAT_28; //0x28
    public int DAT_2C; //0x2C
    public int DAT_30; //0x30
    public int DAT_34; //0x34

    public bool LoadDB(string assetPath)
    {
        ushort uVar1;
        int iVar3;
        RSEG_DB dbVar4;
        int iVar5;
        JUNC_DB dbVar6;
        JUNC_DB dbVar7;

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader == null)
                return false;

            if (reader.BaseStream.Length == 22)
            {
                uVar1 = reader.ReadByte();
                DAT_0A = uVar1;
                uVar1 = reader.ReadByte();
                DAT_08 = uVar1;
                DAT_0C = 0;
            }
            else
            {
                uVar1 = reader.ReadUInt16BE();
                DAT_0A = uVar1;
                uVar1 = reader.ReadUInt16BE();
                DAT_08 = uVar1;
                uVar1 = reader.ReadUInt16BE();
                DAT_0C = uVar1;
            }

            iVar3 = reader.ReadUInt16BE();
            DAT_00 = new JUNC_DB[2];
            dbVar7 = levelManager.juncList[iVar3];
            DAT_00[0] = dbVar7;
            iVar3 = reader.ReadUInt16BE();
            dbVar6 = levelManager.juncList[iVar3];
            DAT_00[1] = dbVar6;
            DAT_10 = new int[2];
            iVar3 = reader.ReadInt32BE();
            DAT_10[0] = iVar3;
            DAT_14 = new int[2];
            iVar3 = reader.ReadInt32BE();
            DAT_14[0] = iVar3;
            iVar3 = reader.ReadInt32BE();
            DAT_10[1] = iVar3;
            iVar3 = reader.ReadInt32BE();
            DAT_14[1] = iVar3;
            dbVar4 = dbVar7.DAT_1C[0];
            iVar5 = 0;

            while(dbVar4 != null)
            {
                dbVar4 = dbVar7.DAT_1C[iVar5 + 1];
                iVar5++;
            }

            dbVar7.DAT_1C[iVar5] = this;
            dbVar4 = dbVar6.DAT_1C[0];
            iVar5 = 0;

            while (dbVar4 != null)
            {
                dbVar4 = dbVar6.DAT_1C[iVar5 + 1];
                iVar5++;
            }

            dbVar6.DAT_1C[iVar5] = this;
            FUN_50EFC();

#if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
            EditorUtility.SetDirty(levelManager.gameObject);
            PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
            return true;
        }
    }

    public void FUN_50EFC()
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = DAT_00[0].DAT_00.x - DAT_00[1].DAT_00.x << 1; //r4
        iVar2 = DAT_10[0] - DAT_10[1]; //r3
        iVar3 = (iVar2 << 1) + iVar2; //r2
        iVar3 = iVar1 + iVar3;

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_20 = iVar3 >> 4;
        iVar1 = DAT_00[0].DAT_00.z - DAT_00[1].DAT_00.z << 1; //r4
        iVar2 = DAT_14[0] - DAT_14[1]; //r3
        iVar3 = (iVar2 << 1) + iVar2; //r2
        iVar3 = iVar1 + iVar3;

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_24 = iVar3 >> 4;
        iVar3 = DAT_00[1].DAT_00.x - DAT_00[0].DAT_00.x; //r2
        iVar2 = (iVar3 << 1) + iVar3; //r3
        iVar3 = (DAT_10[0] << 1) + DAT_10[0] << 1;
        iVar2 -= iVar3;
        iVar3 = iVar2 + ((DAT_10[1] << 1) + DAT_10[1]);

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_28 = iVar3 >> 4;
        iVar3 = DAT_00[1].DAT_00.z - DAT_00[0].DAT_00.z; //r2
        iVar2 = (iVar3 << 1) + iVar3; //r3
        iVar3 = (DAT_14[0] << 1) + DAT_14[0] << 1;
        iVar2 -= iVar3;
        iVar2 = iVar2 + ((DAT_14[1] << 1) + DAT_14[1]);

        if (iVar2 < 0)
            iVar2 += 15;

        DAT_2C = iVar2 >> 4;
        iVar2 = (DAT_10[0] << 1) + DAT_10[0]; //r3

        if (iVar2 < 0)
            iVar2 += 15;

        DAT_30 = iVar2 >> 4;
        iVar3 = (DAT_14[0] << 1) + DAT_14[0]; //r3

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_34 = iVar3 >> 4;
    }

    public void FUN_285E4(int param1, ref Vector3Int param2, out Vector3Int param3)
    {
        ushort uVar1;
        JUNC_DB piVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;

        iVar5 = DAT_30;
        piVar2 = DAT_00[0];
        iVar3 = DAT_20 * param1 >> 12;
        iVar4 = DAT_28 + iVar3;
        iVar6 = (((iVar4 * param1 >> 12) + iVar5) * param1 >> 8) + piVar2.DAT_00.x;
        param2.x = iVar6;
        iVar3 = ((iVar3 + iVar4 * 2) * param1 >> 12) + iVar5;

        if (iVar3 < 0)
            iVar3 += 255;

        param3 = new Vector3Int();
        param3.x = iVar3 >> 8;
        iVar5 = DAT_34;
        iVar3 = DAT_24 * param1 >> 12;
        iVar4 = DAT_2C + iVar3;
        iVar7 = (((iVar4 * param1 >> 12) + iVar5) * param1 >> 8) + piVar2.DAT_00.z;
        param2.z = iVar7;
        uVar1 = DAT_0C;
        iVar3 = ((iVar3 + iVar4 * 2) * param1 >> 12) + iVar5;
        param3.y = 0;

        if (iVar3 < 0)
            iVar3 += 255;

        param3.z = iVar3 >> 8;

        if ((uVar1 & 8) != 0)
        {
            param2.y = piVar2.DAT_00.y;
            return;
        }

        if ((uVar1 & 1) != 0)
        {
            param2.y = piVar2.DAT_00.y + (param1 * (DAT_00[1].DAT_00.y - piVar2.DAT_00.y) >> 12);
            return;
        }

        iVar3 = GameManager.instance.terrain.FUN_1B750((uint)iVar6, (uint)iVar7);
        param2.y = iVar3;
    }

    public long FUN_5105C(int param1, bool param2, ref VigTransform param3)
    {
        long lVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        Vector3Int local_28;
        Vector3Int local_18;
        Vector3Int local_10;
        Vector3Int local_8;

        FUN_285E4(param1, ref param3.position, out local_28);

        if ((DAT_0C & 1) == 0)
        {
            if ((DAT_0C & 8) == 0)
            {
                local_10 = GameManager.instance.terrain.FUN_1B998((uint)param3.position.x, (uint)param3.position.z);
                local_10 = Utilities.VectorNormal(local_10);
                local_28.y = -(local_28.x * local_10.x + local_28.z * local_10.z) / local_10.y;
            }
            else
            {
                local_10 = new Vector3Int(0, -0x1000, 0);
                local_28.y = 0;
            }

            lVar1 = Utilities.VectorNormal2(local_28, out local_8);
        }
        else
        {
            local_28.y = DAT_00[1].DAT_00.y - DAT_00[0].DAT_00.y;
            lVar1 = Utilities.VectorNormal2(local_28, out local_8);
            iVar2 = local_8.x;
            iVar4 = iVar2 * local_8.y;

            if (iVar4 < 0)
                iVar4 += 4095;

            iVar3 = local_8.z;
            local_10 = new Vector3Int();
            local_10.x = iVar4 >> 12;
            iVar4 = iVar2 * iVar2 - iVar3 * iVar3;
            iVar3 = iVar3 * local_8.y;

            if (iVar4 < 0)
                iVar4 += 4095;

            local_10.y = iVar4 >> 12;

            if (iVar3 < 0)
                iVar3 += 4095;

            local_10.z = iVar3 >> 12;
            local_10 = Utilities.VectorNormal(local_10);
        }

        if (!param2)
        {
            local_8.x = -local_8.x;
            local_8.y = -local_8.y;
            local_8.z = -local_8.z;
        }

        local_18 = Utilities.FUN_2A1E0(local_10, local_8);
        param3.rotation.V00 = (short)-local_18.x;
        param3.rotation.V10 = (short)-local_18.y;
        param3.rotation.V20 = (short)-local_18.z;
        param3.rotation.V01 = (short)-local_10.x;
        param3.rotation.V11 = (short)-local_10.y;
        param3.rotation.V21 = (short)-local_10.z;
        param3.rotation.V02 = (short)local_8.x;
        param3.rotation.V12 = (short)local_8.y;
        param3.rotation.V22 = (short)local_8.z;
        return lVar1;
    }

    public int FUN_51334(Vector3Int param1)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        int iVar10;
        int iVar11;
        int iVar12;
        int iVar13;
        int local_70;
        int local_6c;
        int local_68;
        int local_64;
        int local_60;
        int local_5c;
        int local_58;
        int local_54;
        int[] local_50 = new int[17];

        local_70 = DAT_00[0].DAT_00.x;
        local_6c = DAT_00[0].DAT_00.z;
        local_68 = DAT_00[0].DAT_00.x + DAT_10[0];
        local_64 = DAT_00[0].DAT_00.z + DAT_14[0];
        iVar12 = 0;
        local_60 = DAT_00[1].DAT_00.x + DAT_10[1];
        iVar11 = 0x1000;
        local_5c = DAT_00[1].DAT_00.z + DAT_14[1];
        iVar13 = 0;
        local_58 = DAT_00[1].DAT_00.x;
        local_54 = DAT_00[1].DAT_00.z;

        do
        {
            local_50[16] = (local_68 + local_60) / 2;
            iVar3 = (local_64 + local_5c) / 2;
            local_50[0] = local_70;
            local_50[1] = local_6c;
            local_50[14] = local_58;
            local_50[15] = local_54;
            local_50[2] = (local_70 + local_68) / 2;
            local_50[3] = (local_6c + local_64) / 2;
            local_50[4] = (local_50[2] + local_50[16]) / 2;
            local_50[5] = (local_50[3] + iVar3) / 2;
            local_60 = (local_60 + local_58) / 2;
            local_5c = (local_5c + local_54) / 2;
            local_50[12] = local_60;
            local_50[13] = local_5c;
            local_68 = (local_60 + local_50[16]) / 2;
            local_64 = (local_5c + iVar3) / 2;
            local_50[10] = local_68;
            local_50[11] = local_64;
            local_50[8] = (local_50[4] + local_68) / 2;
            local_50[9] = (local_50[5] + local_64) / 2;
            local_50[6] = local_50[8];
            local_50[7] = local_50[9];
            iVar3 = 0;

            if (local_50[8] < local_70)
                iVar3 = 3;

            iVar7 = 0;

            if (local_50[9] < local_6c)
                iVar7 = 3;

            iVar10 = param1.x;
            iVar1 = iVar10 - local_50[(3 - iVar3) * 2];
            iVar4 = 0;

            if (0 < iVar1)
                iVar4 = iVar1;

            iVar1 = iVar10 - local_50[iVar3 * 2];
            iVar3 = 0;

            if (iVar1 < 0)
                iVar3 = iVar1;

            iVar9 = param1.z;
            iVar1 = 0;

            if (0 < iVar9 - local_50[(3 - iVar7) * 2 + 1])
                iVar1 = iVar9 - local_50[(3 - iVar7) * 2 + 1];

            iVar5 = 0;

            if (iVar9 - local_50[iVar7 * 2 + 1] < 0)
                iVar5 = iVar9 - local_50[iVar7 * 2 + 1];

            iVar7 = 0;

            if (local_58 < local_50[8])
                iVar7 = 3;

            iVar8 = 0;

            if (local_54 < local_50[9])
                iVar8 = 3;

            iVar2 = iVar10 - local_50[(3 - iVar7) * 2 + 8];
            iVar6 = 0;

            if (0 < iVar2)
                iVar6 = iVar2;

            iVar10 = iVar10 - local_50[iVar7 * 2 + 8];
            iVar7 = 0;

            if (iVar10 < 0)
                iVar7 = iVar10;

            iVar10 = 0;

            if (0 < iVar9 - local_50[(3 - iVar8) * 2 + 9])
                iVar10 = iVar9 - local_50[(3 - iVar8) * 2 + 9];

            iVar2 = 0;

            if (iVar9 - local_50[iVar8 * 2 + 9] < 0)
                iVar2 = iVar9 - local_50[iVar8 * 2 + 9];

            if ((iVar4 - iVar3) + (iVar1 - iVar5) < (iVar6 - iVar7) + (iVar10 - iVar2))
            {
                iVar11 = (iVar12 + iVar11) / 2;
                local_68 = local_50[2];
                local_64 = local_50[3];
                local_60 = local_50[4];
                local_5c = local_50[5];
                local_58 = local_50[8];
                local_54 = local_50[9];
            }
            else
            {
                iVar12 = (iVar12 + iVar11) / 2;
                local_70 = local_50[8];
                local_6c = local_50[9];
            }

            iVar13++;
        } while (iVar13 < 8);

        //unknown format
        return (iVar12 + iVar11) / 2;
    }
}
