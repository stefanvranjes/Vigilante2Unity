using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _PLACEHOLDER_TYPE
{
    LoadWonderwagon, //FUN_368DC
    LoadThunderbolt, //FUN_36900
    LoadDakota, //FUN_36924
    LoadSamson, //FUN_36948
    LoadLivingston, //FUN_36B40
    LoadXanadu, //FUN_3696C
    LoadPalomino, //FUN_36990
    LoadGuerrero, //FUN_369B4
    LoadBurro, //FUN_369D8
    LoadExcelsior, //FUN_369FC
    LoadTsunami, //FUN_36A20
    LoadMarathon, //FUN_36A44
    LoadTrekker, //FUN_36A68
    LoadLoader, //FUN_36A8C
    LoadStinger, //FUN_36AB0
    LoadVertigo, //FUN_36AD4
    LoadGoliath, //FUN_36AF8
    LoadWapiti, //FUN_36B1C
}

public class Placeholder : VigObject
{
    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    public static Dictionary<_PLACEHOLDER_TYPE, _VEHICLE_INIT> vehicleInit = new Dictionary<_PLACEHOLDER_TYPE, _VEHICLE_INIT>()
    {
        { _PLACEHOLDER_TYPE.LoadWonderwagon, _LoadWonderwagon },
        { _PLACEHOLDER_TYPE.LoadThunderbolt, _LoadThunderbolt },
        { _PLACEHOLDER_TYPE.LoadDakota, _LoadDakota },
        { _PLACEHOLDER_TYPE.LoadLivingston, _LoadLivingston },
        { _PLACEHOLDER_TYPE.LoadSamson, _LoadSamson },
        { _PLACEHOLDER_TYPE.LoadXanadu, _LoadXanadu },
        { _PLACEHOLDER_TYPE.LoadPalomino, _LoadPalomino },
        { _PLACEHOLDER_TYPE.LoadGuerrero, _LoadGuerrero },
        { _PLACEHOLDER_TYPE.LoadBurro, _LoadBurro },
        { _PLACEHOLDER_TYPE.LoadExcelsior, _LoadExcelsior },
        { _PLACEHOLDER_TYPE.LoadTsunami, _LoadTsunami },
        { _PLACEHOLDER_TYPE.LoadMarathon, _LoadMarathon },
        { _PLACEHOLDER_TYPE.LoadTrekker, _LoadTrekker },
        { _PLACEHOLDER_TYPE.LoadLoader, _LoadLoader },
        { _PLACEHOLDER_TYPE.LoadStinger, _LoadStinger },
        { _PLACEHOLDER_TYPE.LoadVertigo, _LoadVertigo },
        { _PLACEHOLDER_TYPE.LoadGoliath, _LoadGoliath },
        { _PLACEHOLDER_TYPE.LoadWapiti, _LoadWapiti },
    };

    public static VigObject FUN_31D30(_PLACEHOLDER_TYPE param1, XOBF_DB param2, short param3, uint param4)
    {
        VigObject oVar1;
        _VEHICLE_INIT dVar2;

        vehicleInit.TryGetValue(param1, out dVar2);

        if (dVar2 != null)
        {
            oVar1 = dVar2(param2, param3, param4);

            if (oVar1 != null)
                return oVar1;
        }

        if (param2 == null || param3 == -1)
        {
            GameObject obj = new GameObject();
            VigObject comp = obj.AddComponent(typeof(VigObject)) as VigObject;
            comp.vData = param2;
            return comp;
        }
        else
            return param2.ini.FUN_2C17C((ushort)param3, typeof(VigObject), param4);
    }

    public static Vehicle _LoadWonderwagon(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[0]);
    }

    public static Vehicle _LoadThunderbolt(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[1]);
    }

    public static Vehicle _LoadDakota(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[2]);
    }

    public static Vehicle _LoadSamson(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[3]);
    }

    public static Vehicle _LoadLivingston(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[4], true);
    }

    public static Vehicle _LoadXanadu(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[5]);
    }

    public static Vehicle _LoadPalomino(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[6]);
    }

    public static Vehicle _LoadGuerrero(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[7]);
    }

    public static Vehicle _LoadBurro(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[8]);
    }

    public static Vehicle _LoadExcelsior(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[9]);
    }

    public static Vehicle _LoadTsunami(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[10]);
    }

    public static Vehicle _LoadMarathon(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[11]);
    }

    public static Vehicle _LoadTrekker(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[12]);
    }

    public static Vehicle _LoadLoader(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[13], true);
    }

    public static Vehicle _LoadStinger(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[14]);
    }

    public static Vehicle _LoadVertigo(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[15]);
    }

    public static Vehicle _LoadGoliath(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[16]);
    }

    public static Vehicle _LoadWapiti(XOBF_DB param1, int param2, uint param3 = 0)
    {
        return FUN_367A4(param1, GameManager.vehicleConfigs[17]);
    }

    public static Vehicle FUN_367A4(XOBF_DB arg1, VehicleData arg2, bool arg3 = false)
    {
        return arg1.FUN_3C464(0, arg2, typeof(Vehicle), arg3);
    }

    public _PLACEHOLDER_TYPE state;

    public override VigObject FUN_31DDC()
    {
        ushort uVar2;
        ushort uVar3;
        Vehicle puVar4;
        VigObject oVar7;

        puVar4 = FUN_31D30(state, vData, DAT_1A, (flags & 4) << 1) as Vehicle;
        puVar4.state = (_VEHICLE_TYPE)state;
        uVar2 = maxHalfHealth;
        uVar3 = maxFullHealth;
        puVar4.flags |= flags;
        puVar4.id = id;
        puVar4.tags = tags;
        puVar4.screen = screen;
        puVar4.vr = vr;
        puVar4.DAT_19 = DAT_19;

        if (uVar2 != 0 || uVar3 != 0)
        {
            oVar7 = puVar4.child2;
            puVar4.maxHalfHealth = uVar2;
            puVar4.maxFullHealth = uVar3;

            while (oVar7 != null)
            {
                oVar7.maxHalfHealth = uVar2;
                oVar7 = oVar7.child;
            }
        }

        puVar4.FUN_2D1DC();
        puVar4.FUN_2C958();
        return puVar4;
    }
}
