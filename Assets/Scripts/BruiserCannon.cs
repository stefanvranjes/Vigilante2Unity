using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserCannon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // FUN_453E0
    public override void UpdateW()
    {
        VigObject iVar7;
        VigTransform iVar10;
        VigObject iVar13;

        //FUN_42330

        if (owner.target != null)
        {
            iVar13 = vObject.child2;
            iVar10 = GameManager.instance.FUN_2CDF4(vObject);
            iVar7 = owner.target;
        }
    }
}
