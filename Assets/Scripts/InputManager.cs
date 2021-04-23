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

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static Controller[] controllers;

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
}
