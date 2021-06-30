using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigShadow : MonoBehaviour
{
    public VigMesh vMesh; //0x00
    public VigTransform vTransform; //0x04
    public int DAT_24; //0x24
    public int DAT_28; //0x28

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FUN_4C73C()
    {
        VigTransform auStack24;

        if (GameManager.instance.DAT_DA0 < vTransform.position.z || 
            vTransform.position.y < GameManager.instance.DAT_DB0)
        {
            auStack24 = Utilities.CompMatrixLV(GameManager.instance.DAT_F00, vTransform);
            vMesh.FUN_21F70(auStack24);
        }
    }
}
