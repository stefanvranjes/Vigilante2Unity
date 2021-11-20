using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam2 : VigObject
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
        VigTransform puVar1;
        VigObject oVar2;
        Vehicle vVar2;
        BufferedBinaryReader brVar2;
        uint uVar3;
        VigObject oVar6;
        HitDetection auStack136;
        Vector3Int iStack80;
        Vector3Int iStack32;

        oVar2 = hit.self;
        auStack136 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack136);
        iStack80 = new Vector3Int();
        iStack80.x = auStack136.position.x / 2;
        iStack80.y = auStack136.position.y / 2;
        iStack80.z = auStack136.position.z / 2;
        iStack80 = Utilities.FUN_24148(oVar2.vTransform, iStack80);

        //if (physics2.M2 == 0)
        //{
            //physics2.M2 = 1;
            //LevelManager.instance.FUN_4DE54(iStack80, 145);

            if (oVar2.type == 2)
            {
                vVar2 = (Vehicle)oVar2;
                puVar1 = GameManager.instance.FUN_2CDF4(this);
                iStack32 = new Vector3Int();
                iStack32.x = puVar1.rotation.V02 << 7;
                iStack32.z = puVar1.rotation.V22 << 7;
                iStack32.y = puVar1.rotation.V12 << 7;
                vVar2.FUN_2B370(iStack32, iStack80);
                //vVar2.FUN_39DCC(-32, Vector3Int.zero, true);

                if (oVar2.id < 0)
                    GameManager.instance.FUN_15ADC(~oVar2.id, 20);
                
                oVar2.vTransform.position.y -= 0x2800;
            if (physics2.M2 == 0)
            {
                physics2.M2 = 1;
                LevelManager.instance.FUN_4DE54(iStack80, 145);
                UIManager.instance.FUN_4E414(iStack80, new Color32(0x80, 0x40, 0x00, 8));
            }
        }
        //}

        if (hit.self.type != 3)
        {
            oVar1 = hit.self;

            if (hit.self.type == 2)
                maxHalfHealth = (ushort)DAT_A0;

            type = 8;
            brVar2 = hit.collider2;
            oVar6 = hit.object2;
            hit.self = this;
            hit.collider2 = hit.collider1;
            hit.collider1 = brVar2;
            hit.object2 = hit.object1;
            hit.object1 = oVar6;
            uVar3 = oVar1.OnCollision(hit);
            type = 3;
            maxHalfHealth = 4;
            return uVar3;
        }

        return 0;
    }

    public int DAT_A0;

    //FUN_60 (CORSAIR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 8)
            FUN_32B90((uint)arg2);

        return 0;
    }
}
