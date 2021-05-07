using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _CONTROLLER_TYPE
{
    None,
    Unsupported,
    JoypadDigital,
    SteeringWheel,
    JoystickAnalog,
    JoypadAnalog,
}

public enum _CONTROLLER_ACTIONS
{
    None,
    CameraReset,
    SteerRight,
    SteerLeft,
    HandBrake,
    BrakeAndReverse,
    Accelerate,
    ViewSelect,
    RearView,
    TargetSelect,
    PrevWeapon,
    NextWeapon,
    Fire,
    MachineGun
}

public enum _CONTROLLER_STEERING
{
    None,
    Steer,
    AccelAndBreak,
    CameraOrbit,
    CameraDolly
}

public struct Controller
{
    public _CONTROLLER_TYPE type; //0x00
    public short delay; //0x02
    public byte[] sequence; //0x04
    public byte actions; //0x08
    public byte steering; //0x09
    public byte DAT_A; //0x0A
    public byte DAT_B; //0x0B
    public byte buttons; //0x0C
    public byte dpad; //0x0D
    public byte DAT_E; //0x0E
    public byte DAT_F; //0x0F
    public byte leftStickX; //0x10
    public byte leftStickY; //0x11
    public byte rightStickX; //0x12
    public byte rightStickY; //0x13
    public byte DAT_14; //0x14
    public byte DAT_15; //0x15
}

public struct Input
{
    public byte DAT_00; //0x00
    public byte DAT_01; //0x01
    public byte DAT_02; //0x02
    public byte DAT_03; //0x03
    public byte DAT_04; //0x04
    public byte DAT_05; //0x05
    public byte DAT_06; //0x06
    public byte DAT_07; //0x07
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static Controller[] controllers;
    public static Input[,] inputs;

    public _CONTROLLER_ACTIONS[] controllerActions;
    public _CONTROLLER_STEERING[] controllerSteerings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FUN_15540()
    {
        ushort uVar1;
        short sVar2;
        int iVar3;
        uint uVar4;
        uint uVar6;
        int pcVar7;
        int iVar11;
        int iVar12;
        int iVar14;
        int local_28;

        if (GameManager.instance.DAT_D08 != GameManager.instance.DAT_CF8)
            GameManager.instance.DAT_D08 = (byte)(GameManager.instance.DAT_D08 + 1 & 7);

        iVar12 = 0;
        iVar11 = 0;
        iVar14 = 0;
        local_28 = 0;
        pcVar7 = GameManager.instance.DAT_D08 + 1;

        for (int i = 0; i < 2; i++) //two controllers
        {
            uVar6 = 0;

            if (inputs[i, pcVar7].DAT_00 != 255)
                uVar6 = ~(uint)(inputs[i, pcVar7].DAT_02 << 8 | inputs[i, pcVar7].DAT_03); //not sure about this

            controllers[i].type = GetControllerType(i);
            
            if (controllers[i].type == _CONTROLLER_TYPE.SteeringWheel)
            {
                uVar4 = (uint)(inputs[i, pcVar7].DAT_05 == 255 ? 1 : 0) << 6;

                if (inputs[i, pcVar7].DAT_07 == 255)
                    uVar4 |= 4;

                if (inputs[i, pcVar7].DAT_06 == 255)
                    uVar6 = uVar6 & 0xffff | 0x80 | uVar4;
                else
                    uVar6 = uVar6 & 0xffff | uVar4;
            }

            uVar4 = uVar6 & 0xffff |
                    (uVar6 & 0xffff & ~(uint)GameManager.instance.DAT_CF0[i]) << 16;

            GameManager.instance.DAT_D18[i] = uVar4;
            controllers[i].buttons = (byte)uVar4;
            controllers[i].dpad = (byte)(uVar4 >> 8);
            controllers[i].DAT_E = (byte)(uVar4 >> 16);
            controllers[i].DAT_F = (byte)(uVar4 >> 24);
            GameManager.instance.DAT_CF0[i] = (ushort)uVar6;
            uVar6 = FUN_150B4(uVar4 & 0xf7fff7ff, GameManager.DAT_637DC[i, (byte)controllers[i].type],
                            GameManager.DAT_637E0[i, (byte)controllers[i].type]);

        }
    }

    //FUN_154A4
    private _CONTROLLER_TYPE GetControllerType(int param1)
    {
        byte bVar1;

        if (inputs[param1, 0].DAT_00 != 0)
            return _CONTROLLER_TYPE.None;

        bVar1 = inputs[param1, 0].DAT_01;

        if (bVar1 == 83)
            return _CONTROLLER_TYPE.JoystickAnalog;

        if (bVar1 < 84)
        {
            if (bVar1 == 35)
                return _CONTROLLER_TYPE.SteeringWheel;

            if (bVar1 == 65)
                return _CONTROLLER_TYPE.JoypadDigital;
        }
        else
        {
            if (bVar1 == 115)
                return _CONTROLLER_TYPE.JoypadAnalog;
        }

        return _CONTROLLER_TYPE.Unsupported;
    }

    private uint FUN_150B4(uint param1, uint param2, uint param3)
    {
        uint uVar1;
        uint uVar2;

        uVar2 = 0;

        if (param1 != 0)
        {
            uVar1 = param1 & 0x10001;

            do
            {
                uVar2 = uVar2 | uVar1 << (int)(param2 & 15);
                param2 = param2 >> 4 | param3 << 28;
                param3 = param3 >> 4;
                uVar1 = param1 >> 1;
                param1 = uVar1 & 0xffff7fff;
                uVar1 &= 0x10001;
            } while (param1 != 0);
        }

        return uVar2;
    }
}
