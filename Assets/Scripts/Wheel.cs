using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _WHEEL_TYPE
{
    Unflatten, 
    Flatten //FUN_395E0
}

public class Wheel : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _WHEEL_TYPE state;

    public override uint UpdateW(int arg1, int arg2)
    {
        Vehicle vVar1;
        int iVar3;

        switch (state)
        {
            case _WHEEL_TYPE.Flatten:
                if (arg1 == 2)
                {
                    vVar1 = Utilities.FUN_2CDB0(this) as Vehicle;
                    iVar3 = 0;
                    flags &= 0xbfffffff;
                    physics2.X += 0xc00;

                    do
                    {
                        if (vVar1.wheels[iVar3] != null &&
                            (vVar1.wheels[iVar3].flags & 0x40000000) != 0)
                            return 0;

                        iVar3++;
                    } while (iVar3 < 6);

                    vVar1.flags &= 0xfffdffff;
                    state = _WHEEL_TYPE.Unflatten;
                }

                return 0;
        }

        return 0;
    }

    public new VigObject FUN_2C344(XOBF_DB param1, ushort param2, uint param3)
    {
        VigMesh mVar1;
        int iVar2;
        VigObject oVar3;
        BufferedBinaryReader brVar4;
        ConfigContainer puVar5;

        puVar5 = param1.ini.configContainers[param2];

        if ((puVar5.flag & 0x7ff) == 0x7ff)
            vMesh = null;
        else
        {
            mVar1 = param1.FUN_1FD18(gameObject, puVar5.flag & 0x7ffU, true);
            vMesh = mVar1;
        }

        if (puVar5.colliderID < 0)
            vCollider = null;
        else
        {
            VigCollider collider = param1.cbbList[puVar5.colliderID];
            vCollider = new VigCollider(collider.buffer);
        }

        vData = param1;
        DAT_1A = (short)param2;

        if ((param3 & 8) == 0)
            vAnim = null;
        else
        {
            brVar4 = new BufferedBinaryReader(param1.animations);

            if (brVar4.GetBuffer() != null)
            {
                iVar2 = brVar4.ReadInt32(param2 * 4 + 4);

                if (iVar2 != 0)
                    brVar4.Seek(iVar2, SeekOrigin.Begin);
                else
                    brVar4 = null;
            }
            else
                brVar4 = null;

            vAnim = brVar4;
        }

        DAT_4A = GameManager.instance.timer;

        if ((param3 & 2) == 0 && puVar5.next != 0xffff)
        {
            oVar3 = param1.ini.FUN_2C17C_3(puVar5.next, typeof(WheelChild), param3 | 0x21);
            child2 = oVar3;

            if (oVar3 != null)
            {
                oVar3.ApplyTransformation();
                child2.parent = this;
            }
        }
        else
            child2 = null;

        return this;
    }
}
