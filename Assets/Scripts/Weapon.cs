using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public VigObject vObject;
    public Vehicle owner;
    public short ammo;
    public short maxAmmo;

    private void Awake()
    {
        vObject = GetComponent<VigObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void UpdateW()
    {
        
    }

    public virtual void ComboW(uint combo)
    {

    }

    private void FUN_42330()
    {
        int iVar1;

        iVar1 = 0;

        if ((vObject.flags & 0x1000000) != 0)
        {
            //...
        }
    }
}
