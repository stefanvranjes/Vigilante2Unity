using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigContainer
{
    public short flag; //0x00
    public short colliderID; //0x02
    public Vector3Int v3_1; //0x04
    public Vector3Int v3_2; //0x10
    public short objID; //0x16
    public short previous; //0x18
    public short next; //0x1A
}

public class VigConfig : MonoBehaviour
{
    public int dataID;
    public List<ConfigContainer> configContainers;
    public uint pointerUnk1; //0x08
    public VigObject[] obj;
    public int currentID=0;

    public VigObject FUN_2C17C(int int1, int allocSpace, int int3) //int3 = r19, configContainers = r20, int1 = r18
    {
        int containerID = ((((int1 & 0xFFFF) << 3) - (int1 & 0xFFFF)) << 2) / 0x1C;
        VigObject vObject;

        if (configContainers[containerID].flag < 0 || (0xFF < configContainers[containerID].objID && (int3 & 0x20) != 0))
        {
            if ((int3 & 1) == 0 || configContainers[containerID].previous == -1)
                vObject=null; //no need to return null
            else
                vObject=FUN_2C17C(configContainers[containerID].previous, 128, int3);
        }
        else
        {
            vObject=FUN_2BF44(containerID, allocSpace);
            currentID++;
            vObject.DAT_1A = (short)int1;
            vObject.id = configContainers[containerID].objID;

            if ((int3 & 8) == 0)
                vObject.unk3 = 0;
            else
            {
                int iVar = 0;

                if (pointerUnk1 != 0)
                {
                    //...
                }

                vObject.unk3 = iVar;
            }

            vObject.unk4 = GameManager.instance.timer;

            if ((int3 & 1) != 0 && configContainers[containerID].previous != -1)
            {
                VigObject previous = FUN_2C17C(configContainers[containerID].previous, 128, int3);
                vObject.child = previous;

                if (previous != null)
                {
                    previous.parent = vObject;
                    vObject.child.ApplyTransformation();
                }
            }

            if ((int3 & 2) == 0 && configContainers[containerID].next != -1)
            {
                VigObject next = FUN_2C17C(configContainers[containerID].next, 128, int3 | 33);
                vObject.child2 = next;

                if (next != null)
                {
                    next.parent = vObject;
                    vObject.child2.ApplyTransformation();
                }
            }
        }

        return vObject;
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

    private VigObject FUN_2BF44(int container, int allocSpace)
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
