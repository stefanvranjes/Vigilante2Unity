using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : VigObject
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
        VigObject oVar1;
        int iVar2;

        oVar1 = hit.self;

        if (oVar1.type == 3)
            return 0;

        if (oVar1.type == 2)
        {
            oVar1.physics1.Y = -195200;
            oVar1.physics2.Y = 50000;

            if (oVar1.id < 0)
                GameManager.instance.FUN_15B00(~oVar1.id, 255, 2, 128);
        }

        iVar2 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 65, vTransform.position);
        LevelManager.instance.FUN_4DE54(vTransform.position, 42);
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    private static byte[] DAT_20 = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0xC0, 0xFF, 0xFF, 0x00, 0xC0, 0xFF, 0xFF,
                                                0x00, 0xC0, 0xFF, 0xFF, 0x00, 0x40, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00,
                                                0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

    private static Color32[] DAT_950 = new Color32[] { new Color32(0x80, 0x00, 0x00, 8), new Color32(0x00, 0x80, 0x00, 8),
                                                       new Color32(0x00, 0x00, 0x80, 8) };

    //FUN_80 (MARATHON.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        int iVar2;

        if (arg1 != 2)
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (vTransform.position.y <= iVar1)
                    return 0;

                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 65, vTransform.position);
                LevelManager.instance.FUN_4DE54(vTransform.position, 42);
            }
            else
                return 0;
        }

        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigTransform tVar1;
        int iVar2;
        VigMesh mVar2;
        VigObject oVar3;
        Vector3Int iStack24;
        Vector3Int sStack8;

        if (arg1 == 5)
        {
            tVar1 = GameManager.instance.FUN_2CDF4(this);
            GameObject obj = new GameObject();
            oVar3 = obj.AddComponent<VigObject>();
            Utilities.FUN_2CC9C(this, oVar3);
            Utilities.ParentChildren(this, this);
            mVar2 = vMesh;
            oVar3.flags |= 0x410;
            //oVar3.vMesh = mVar2;
            oVar3.vTransform.rotation = vTransform.rotation;
            oVar3.vMesh = vData.FUN_1FD18(obj, mVar2.tmdID, true);
            Destroy(vMesh.GetRenderer());
            vMesh = null;
            vCollider = new VigCollider(DAT_20);
            flags |= 0x80;
            FUN_30C20();
            vTransform = GameManager.FUN_2A39C();
            vTransform.position = tVar1.position;
            oVar3 = DAT_84;

            if (oVar3 == null)
            {
                Vector3Int v3 = Utilities.FUN_24094(tVar1.rotation, screen);
                physics1.Z = v3.x;
                physics1.W = v3.y;
                physics2.X = v3.z;
                physics1.Z = (physics1.Z << 9) >> 12;
                physics1.W = (physics1.W << 9) >> 12;
                physics2.X = (physics2.X << 9) >> 12;
            }
            else
            {
                iStack24 = new Vector3Int();
                iStack24.x = oVar3.screen.x - tVar1.position.x;
                iStack24.y = oVar3.screen.y - tVar1.position.y;
                iStack24.z = oVar3.screen.z - tVar1.position.z;
                Utilities.FUN_29FC8(iStack24, out sStack8);
                physics1.Z = sStack8.x * 0x5968 >> 12;
                physics1.W = sStack8.y * 0x5968 >> 12;
                physics2.X = sStack8.z * 0x5968 >> 12;
            }

            FUN_2CCBC();
            transform.parent = null;
            FUN_305FC();
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar2, vData.sndList, 3, tVar1.position);
            UIManager.instance.FUN_4E414(tVar1.position, DAT_950[tags]);
            return 0xffffffff;
        }

        return 0;
    }
}
