using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        transform.localPosition = new Vector3(
            (float)vTransform.position.x / GameManager.instance.translateFactor,
            (float)-vTransform.position.y / GameManager.instance.translateFactor,
            (float)vTransform.position.z / GameManager.instance.translateFactor);

        transform.localRotation = vTransform.rotation.Matrix2Quaternion;
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
        transform.localScale = vTransform.rotation.Scale;
    }

    //FUN_393A0
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject oVar1;
        Vehicle vVar2;

        if (arg1 == 5)
        {
            vVar2 = Utilities.FUN_2CD78(this) as Vehicle;
            vVar2.DAT_F6 &= 0xfffe;
            oVar1 = FUN_2CCBC();
            GameManager.instance.FUN_307CC(oVar1);
            return 0xffffffff;
        }
        else
            return 0;
    }
}
