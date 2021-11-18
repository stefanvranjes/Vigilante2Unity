using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChild : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        transform.localEulerAngles += eulerAngles;
    }

    public Vector3 eulerAngles;
}
