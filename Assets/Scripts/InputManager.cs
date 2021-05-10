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
    public byte[] stick; //0x10
    public byte[] DAT_14; //0x14
}

public struct PSXInput
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
    public static PSXInput[,] inputs;
    public static byte[,] axisData; 

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
        controllers = new Controller[2];
        inputs = new PSXInput[2, 9];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            string player = "P" + i + 1 + "_";
            inputs[i, 0].DAT_02 = 0xff;
            inputs[i, 1].DAT_03 = 0xff;

            if (Input.GetButton(player + "CROSS"))
                inputs[i, 0].DAT_02 &= 0xbf;

            if (Input.GetButton(player + "CIRCLE"))
                inputs[i, 0].DAT_02 &= 0xdf;

            if (Input.GetButton(player + "SQUARE"))
                inputs[i, 0].DAT_02 &= 0x7f;

            if (Input.GetButton(player + "TRIANGLE"))
                inputs[i, 0].DAT_02 &= 0xef;

            if (Input.GetButton(player + "R1"))
                inputs[i, 0].DAT_02 &= 0xf7;

            if (Input.GetButton(player + "L1"))
                inputs[i, 0].DAT_02 &= 0xfb;

            if (Input.GetButton(player + "R2"))
                inputs[i, 0].DAT_02 &= 0xfd;

            if (Input.GetButton(player + "L2"))
                inputs[i, 0].DAT_02 &= 0xfe;

            if (Input.GetButton(player + "UP"))
                inputs[i, 0].DAT_03 &= 0xef;

            if (Input.GetButton(player + "DOWN"))
                inputs[i, 0].DAT_03 &= 0xbf;

            if (Input.GetButton(player + "RIGHT"))
                inputs[i, 0].DAT_03 &= 0xdf;

            if (Input.GetButton(player + "LEFT"))
                inputs[i, 0].DAT_03 &= 0x7f;

            if (Input.GetButton(player + "START"))
                inputs[i, 0].DAT_03 &= 0xf7;

            if (Input.GetButton(player + "SELECT"))
                inputs[i, 0].DAT_03 &= 0xfe;

            inputs[i, 0].DAT_04 = (byte)((int)(Input.GetAxis(player + "ANALOG_RX") * 0x7f) + 0x7f);
            inputs[i, 0].DAT_05 = (byte)((int)(Input.GetAxis(player + "ANALOG_RY") * 0x7f) + 0x7f);
            inputs[i, 0].DAT_06 = (byte)((int)(Input.GetAxis(player + "ANALOG_LX") * 0x7f) + 0x7f);
            inputs[i, 0].DAT_07 = (byte)((int)(Input.GetAxis(player + "ANALOG_LY") * 0x7f) + 0x7f);

            for (int j = 1; j < 9; j++)
            {
                inputs[i, j].DAT_00 = inputs[i, 0].DAT_00;
                inputs[i, j].DAT_01 = inputs[i, 0].DAT_01;
                inputs[i, j].DAT_02 = inputs[i, 0].DAT_02;
                inputs[i, j].DAT_03 = inputs[i, 0].DAT_03;
                inputs[i, j].DAT_04 = inputs[i, 0].DAT_04;
                inputs[i, j].DAT_05 = inputs[i, 0].DAT_05;
                inputs[i, j].DAT_06 = inputs[i, 0].DAT_06;
                inputs[i, j].DAT_07 = inputs[i, 0].DAT_07;
            }
        }
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

            GameManager.instance.DAT_D18[i] = (byte)uVar4;
            GameManager.instance.DAT_D19[i] = (byte)(uVar4 >> 8);
            GameManager.instance.DAT_D1A[i] = (byte)(uVar4 >> 16);
            GameManager.instance.DAT_D1B[i] = (byte)(uVar4 >> 24);
            controllers[i].buttons = (byte)uVar4;
            controllers[i].dpad = (byte)(uVar4 >> 8);
            controllers[i].DAT_E = (byte)(uVar4 >> 16);
            controllers[i].DAT_F = (byte)(uVar4 >> 24);
            GameManager.instance.DAT_CF0[i] = (ushort)uVar6;
            uVar6 = FUN_150B4(uVar4 & 0xf7fff7ff, GameManager.DAT_637DC[i, (byte)controllers[i].type],
                            GameManager.DAT_637E0[i, (byte)controllers[i].type]);
            controllers[i].actions = (byte)uVar6;
            controllers[i].steering = (byte)(uVar6 >> 8);
            controllers[i].DAT_A = (byte)(uVar6 >> 16);
            controllers[i].DAT_B = (byte)(uVar6 >> 24);
            GameManager.instance.DAT_D1B[i] = (byte)(GameManager.DAT_637E0[i, (byte)controllers[i].type] >> 12 & 1);
            uVar6 = uVar4 & 0xf0000000;

            if (_CONTROLLER_TYPE.JoypadDigital < controllers[i].type) //need to check this
            {
                uVar1 = GameManager.instance.DAT_08[i, (byte)controllers[i].type];
                controllers[i].DAT_14[0] = controllers[i].stick[0];
                controllers[i].DAT_14[1] = controllers[i].stick[1];
                controllers[i].DAT_14[2] = controllers[i].stick[2];
                controllers[i].DAT_14[3] = controllers[i].stick[3];
                controllers[i].stick[0] = 128;
                controllers[i].stick[1] = 128;
                controllers[i].stick[2] = 128;
                controllers[i].stick[3] = 128;

                if ((uVar1 & 15) != 0)
                    controllers[i].stick[(uVar1 & 0xf) - 1] = axisData[i, inputs[i, pcVar7].DAT_04];

                if ((uVar1 >> 4 & 15) != 0)
                    controllers[i].stick[(uVar1 >> 4 & 0xf) - 1] = axisData[i, inputs[i, pcVar7].DAT_05];

                if ((uVar1 >> 8 & 15) != 0)
                    controllers[i].stick[(uVar1 >> 8 & 0xf) - 1] = axisData[i, inputs[i, pcVar7].DAT_06];

                if (uVar1 >> 12 != 0)
                    controllers[i].stick[(uVar1 >> 12) - 1] = axisData[i, inputs[i, pcVar7].DAT_07];

                uVar4 = 0;

                if (inputs[i, pcVar7].DAT_06 - 128 < -96)
                    uVar4 = (uint)((GameManager.instance.DAT_CF4[i] - 128 < -96 ? 1 : 0) ^ 1) << 31;

                if (96 < inputs[i, pcVar7].DAT_06 - 128)
                    if (GameManager.instance.DAT_CF4[i] - 128 < 97)
                        uVar4 |= 0x20000000;

                if (inputs[i, pcVar7].DAT_07 - 128 < -96)
                    if (-97 < GameManager.instance.DAT_CF5[i] - 128)
                        uVar4 |= 0x10000000;

                if (96 < inputs[i, pcVar7].DAT_07 - 128)
                    if (GameManager.instance.DAT_CF5[i] - 128 < 97)
                        uVar6 |= 0x40000000;

                uVar6 |= uVar4;
                GameManager.instance.DAT_CF4[i] = inputs[i, pcVar7].DAT_06;
            }

            if (uVar6 == 0)
            {
                sVar2 = controllers[i].delay;

                if (controllers[i].delay != 0)
                {
                    controllers[i].delay = (short)(sVar2 - 1);

                    if (sVar2 == 1)
                    {
                        controllers[i].sequence[0] = 0;
                        controllers[i].sequence[1] = 0;
                        controllers[i].sequence[2] = 0;
                        controllers[i].sequence[3] = 0;
                    }
                }
            }
            else
            {
                uVar6 = (uint)Utilities.LeadingZeros((int)(uVar6 >> 1));
                uint seq = (uint)(controllers[i].sequence[3] << 24 | controllers[i].sequence[2] << 16 | 
                            controllers[i].sequence[1] << 8 | controllers[i].sequence[0]) << 4 | uVar6;
                controllers[i].sequence[0] = (byte)seq;
                controllers[i].sequence[1] = (byte)(seq >> 8);
                controllers[i].sequence[2] = (byte)(seq >> 16);
                controllers[i].sequence[3] = (byte)(seq >> 24);
                controllers[i].delay = 20;
            }
        }

        int p1 = GameManager.instance.DAT_D18[0] << 24 | GameManager.instance.DAT_D19[0] << 16 |
                 GameManager.instance.DAT_D1A[0] << 8 | GameManager.instance.DAT_D1B[0];
        int p2 = GameManager.instance.DAT_D18[1] << 24 | GameManager.instance.DAT_D19[1] << 16 |
                 GameManager.instance.DAT_D1A[1] << 8 | GameManager.instance.DAT_D1B[1];
        GameManager.instance.DAT_CFC[0] = (byte)(p1 | p2);
        GameManager.instance.DAT_CFC[1] = (byte)((p1 | p2) >> 8);
        GameManager.instance.DAT_CFC[2] = (byte)((p1 | p2) >> 16);
        GameManager.instance.DAT_CFC[3] = (byte)((p1 | p2) >> 24);
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
