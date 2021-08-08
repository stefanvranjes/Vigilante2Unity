using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _THROWAWAY_TYPE
{
    Spawnable,
    Unspawnable
}

public class Throwaway : VigObject
{
    public byte DAT_87;
    public _THROWAWAY_TYPE state;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


}
