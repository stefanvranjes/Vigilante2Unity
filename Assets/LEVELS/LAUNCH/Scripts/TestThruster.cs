using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThruster : VigObject
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
        ConfigContainer ccVar2;
        TestThruster3 puVar4;
        int iVar5;
        VigObject oVar8;

        if (hit.collider1.ReadUInt16(2) != 0)
        {
            if (tags != 0)
                return 0;

            if (hit.self.type != 2)
                return 0;

            oVar8 = child2;

            while (oVar8 != null && oVar8.id != 1)
                oVar8 = oVar8.child;

            tags = 1;
            ccVar2 = oVar8.FUN_2C5F4(0x8000);

            if (ccVar2 == null)
                return 0;

            puVar4 = vData.ini.FUN_2C17C(24, typeof(TestThruster3), 8) as TestThruster3;
            Utilities.ParentChildren(puVar4, puVar4);

            if (puVar4 == null)
                return 0;

            puVar4.vTransform = GameManager.instance.FUN_2CEAC(oVar8, ccVar2);
            puVar4.flags = 4;
            puVar4.type = 3;
            puVar4.FUN_305FC();
            GameManager.instance.FUN_30CB0(puVar4, 72);
            puVar4.tags = 0;
            puVar4.DAT_80 = this;
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
            return 0;
        }

        oVar8 = hit.self;

        if (oVar8.type != 8)
            return 0;

        if ((ushort)oVar8.id - 114 < 3)
            return 0;

        FUN_32B90(oVar8.maxHalfHealth);
        return 0;
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        Dictionary<int, Type> dict = new Dictionary<int, Type>();
        dict.Add(599, typeof(VigChild));
        VigObject obj = arg1.ini.FUN_2C17C((ushort)arg2, typeof(TestThruster), arg3, dict);
        ((VigChild)obj.child2).state = _CHILD_TYPE.Default;
        return obj;
    }

    //FUN_1090 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        TestThruster3 puVar3;
        VigTransform tVar4;
        VigChild puVar4;
        int iVar5;
        TestThruster2 puVar5;
        VigObject oVar8;
        ConfigContainer ccVar8;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            puVar4 = child2 as VigChild;
            DAT_58 = 0x19cccc;

            while (puVar4 != null)
            {
                puVar4.type = 3;
                puVar4.state = _CHILD_TYPE.Child;
                puVar4 = puVar4.child as VigChild;
            }

            return 0;
        }

        if (arg1 != 8)
        {
            if (arg1 != 9)
                return 0;

            if (arg2 != 0)
                return 0;

            if (1 < tags)
                return 0;

            oVar8 = child2;
            tags = 2;

            if (oVar8 == null)
                return 0;

            do
            {
                if (oVar8.id == 1) break;

                oVar8 = oVar8.child;
            } while (oVar8 != null);

            if (oVar8 == null)
                return 0;

            tVar4 = GameManager.instance.FUN_2CDF4(oVar8);
            oVar8.vTransform = tVar4;
            sVar1 = id;
            oVar8.type = 4;
            oVar8.id = sVar1;
            oVar8.FUN_2CCBC();
            oVar8.vr = Utilities.FUN_2A2E0(oVar8.vTransform.rotation);
            oVar8.ApplyRotationMatrix();
            puVar5 = Utilities.FUN_52188(oVar8, typeof(TestThruster2)) as TestThruster2;
            puVar5.tags = 0;
            puVar5.flags = puVar5.flags & 0xffffffdf | 0x800;

            if (puVar5.child2 != null)
                puVar5.child2.parent = puVar5;

            puVar5.FUN_305FC();
            GameManager.instance.FUN_30CB0(puVar5, 30);
            ccVar8 = puVar5.FUN_2C5F4(0x8000);

            if (ccVar8 == null)
                return 0;

            puVar3 = puVar5.vData.ini.FUN_2C17C(24, typeof(TestThruster3), 8) as TestThruster3;

            if (puVar3 == null)
                return 0;

            puVar3.vTransform = Utilities.FUN_2C77C(ccVar8);
            puVar3.type = 3;
            Utilities.FUN_2CA94(puVar5, ccVar8, puVar3);
            Utilities.ParentChildren(puVar5, puVar5);
            puVar3.FUN_30BF0();
            puVar3.tags = 0;
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
            puVar5.DAT_58 = 0x19cccc;
            puVar3.DAT_58 = 0x19cccc;
            return 0;
        }

        FUN_32B90((uint)arg2);
        return 0;
    }
}
