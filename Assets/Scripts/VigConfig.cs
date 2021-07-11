using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigContainer
{
    public ushort flag; //0x00
    public short colliderID; //0x02
    public Vector3Int v3_1; //0x04
    public Vector3Int v3_2; //0x10
    public ushort objID; //0x16
    public ushort previous; //0x18
    public ushort next; //0x1A
}

public class VigConfig : MonoBehaviour
{
    public int dataID;
    public List<ConfigContainer> configContainers;
    public uint pointerUnk1; //0x08
    public VigObject[] obj;
    public int currentID=0;

    [HideInInspector]
    public XOBF_DB xobf;

    /*public VigObject FUN_2C17C(ushort param1, Type param2, uint param3)
    {
        int containerID = ((((param1 & 0xFFFF) << 3) - (param1 & 0xFFFF)) << 2) / 0x1C;
        VigObject vObject;

        if (configContainers[containerID].flag < 0 || (0xFF < configContainers[containerID].objID && (param3 & 0x20) != 0))
        {
            if ((param3 & 1) == 0 || configContainers[containerID].previous == -1)
                vObject=null; //no need to return null
            else
                vObject=FUN_2C17C(configContainers[containerID].previous, 128, param3);
        }
        else
        {
            vObject=FUN_2BF44(containerID, param2);
            currentID++;
            vObject.DAT_1A = (short)param1;
            vObject.id = configContainers[containerID].objID;

            if ((param3 & 8) == 0)
                vObject.DAT_64 = 0;
            else
            {
                int iVar = 0;

                if (pointerUnk1 != 0)
                {
                    //...
                }

                vObject.DAT_64 = iVar;
            }

            vObject.unk4 = GameManager.instance.timer;

            if ((param3 & 1) != 0 && configContainers[containerID].previous != -1)
            {
                VigObject previous = FUN_2C17C(configContainers[containerID].previous, 128, param3);
                vObject.child = previous;

                if (previous != null)
                {
                    previous.parent = vObject;
                    vObject.child.ApplyTransformation();
                }
            }

            if ((param3 & 2) == 0 && configContainers[containerID].next != -1)
            {
                VigObject next = FUN_2C17C(configContainers[containerID].next, 128, param3 | 33);
                vObject.child2 = next;

                if (next != null)
                {
                    next.parent = vObject;
                    vObject.child2.ApplyTransformation();
                }
            }
        }

        return vObject;
    }*/

    public VigObject FUN_2C17C(ushort param1, Type param2, uint param3)
    {
        VigObject oVar1;
        VigObject oVar3;
        ConfigContainer psVar5;

        psVar5 = configContainers[param1];

        if ((short)psVar5.flag < 0 || (255 < (short)psVar5.objID && (param3 & 0x20) != 0))
        {
            if ((param3 & 1) == 0 || (short)psVar5.previous == -1)
                oVar1 = null;
            else
                oVar1 = FUN_2C17C(psVar5.previous, typeof(VigObject), param3);
        }
        else
        {
            oVar1 = FUN_2BF44(psVar5, param2);
            oVar1.DAT_1A = (short)param1;
            oVar1.id = (short)psVar5.objID;
            //animation
            oVar1.DAT_4A = GameManager.instance.timer;

            if ((param3 & 1) != 0 && (short)psVar5.previous != -1)
            {
                oVar3 = FUN_2C17C(psVar5.previous, typeof(VigObject), param3);
                oVar1.child = oVar3;

                if (oVar3 != null)
                {
                    oVar3.parent = oVar1;
                    oVar1.child.ApplyTransformation();
                }
            }

            if ((param3 & 2) == 0 && (short)psVar5.next != -1)
            {
                oVar3 = FUN_2C17C(psVar5.next, typeof(VigObject), param3 | 33);
                oVar1.child2 = oVar3;

                if (oVar3 != null)
                {
                    oVar3.parent = oVar1;
                    oVar1.child2.ApplyTransformation();
                }
            }
        }

        return oVar1;
    }

    public ConfigContainer FUN_2C534(ushort param1, ushort param2)
    {
        uint uVar1;
        ConfigContainer psVar2;

        if (param1 != 0xffff)
        {
            uVar1 = param1;

            do
            {
                psVar2 = configContainers[(int)uVar1];

                if (psVar2.flag == param2)
                    return psVar2;

                uVar1 = psVar2.previous;
            } while (uVar1 != 0xffff);
        }

        return null;
    }

    public ConfigContainer FUN_2C590(int int1, int int2)
    {
        int1 &= 0xFFFF;
        int configIndex = (int1 << 3) - int1 << 2;
        return FUN_2C534(configContainers[configIndex / 0x1C].next, int2 & 0xFFFF);
    }

    public ConfigContainer FUN_2C6D0(ConfigContainer container, int int2)
    {
        return FUN_2C638(container.next, int2 & 0xFFFF);
    }

    public ConfigContainer FUN_2C5CC(ConfigContainer container, int int2)
    {
        return FUN_2C534(container.next, int2 & 0xFFFF);
    }

    public int FUN_2C73C(ConfigContainer container)
    {
        int offset = configContainers.IndexOf(container) * 0x1C;
        //int offset = List.IndexOf(configContainers, container) * 0x1C;
        int iVar1 = (offset << 3) + offset; //r2
        int iVar2 = iVar1 << 6; //r3
        iVar1 = iVar1 + iVar2 << 3;
        iVar1 += offset;
        iVar2 = iVar1 << 15;
        iVar1 = iVar1 + iVar2 << 3;
        iVar1 += offset;
        return -iVar1 >> 2;
    }

    private ConfigContainer FUN_2C638(int int1, int int2)
    {
        int2 &= 0xFFFF;

        if (int1 != -1)
        {
            int iVar1;

            do
            {
                iVar1 = int1 & 0xFFFF;
                int configIndex = (iVar1 << 3) - iVar1 << 2;
                configIndex = configIndex / 0x1C;

                if ((ushort)configContainers[configIndex].objID == int2)
                    return configContainers[configIndex];

                int1 = configContainers[configIndex].previous;
            } while (int1 != -1);
        }

        return null;
    }

    private ConfigContainer FUN_2C534(int int1, int int2)
    {
        int2 &= 0xFFFF;

        if (int1 != -1)
        {
            int iVar1;

            do
            {
                iVar1 = int1 & 0xFFFF;
                int configIndex = (iVar1 << 3) - iVar1 << 2;
                configIndex = configIndex / 0x1C;

                if ((ushort)configContainers[configIndex].flag == int2)
                    return configContainers[configIndex];

                int1 = configContainers[configIndex].previous;
            } while (int1 != -1);
        }

        return null;
    }

    /*private VigObject FUN_2BF44(ConfigContainer container, Type component)
    {
        obj[currentID].flags = (uint)((configContainers[container].flag & 0x800) != 0 ? 1 : 0) << 4;
        obj[currentID].vr = configContainers[container].v3_2;
        obj[currentID].screen = configContainers[container].v3_1;
        obj[currentID].vData = this;

        if ((configContainers[container].flag & 0x7FF) < 0x7FF)
        {
            //apply 3d model; not necessery since it's been applied from the inspector
        }

        if (-1 < configContainers[container].colliderID)
        {
            //apply collider; not necessery since it's been applied from the inspector
        }

        return obj[currentID];
    }*/

    private VigObject FUN_2BF44(ConfigContainer container, Type component)
    {
        ushort uVar2;
        VigObject oVar3;
        VigMesh mVar4;

        GameObject obj = new GameObject();
        oVar3 = obj.AddComponent(component) as VigObject;
        uVar2 = container.flag;
        oVar3.flags = (uint)((uVar2 & 0x800) != 0 ? 1 : 0) << 4;
        oVar3.vr = container.v3_2;
        oVar3.screen = container.v3_1;
        oVar3.vData = xobf;

        if ((uVar2 & 0x7ff) < 0x7ff)
        {
            mVar4 = xobf.FUN_1FD18(obj, (uint)(uVar2 & 0x7ff));
            oVar3.vMesh = mVar4;
        }

        if (-1 < container.colliderID)
            oVar3.vCollider = xobf.cbbList[container.colliderID];

        return oVar3;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
