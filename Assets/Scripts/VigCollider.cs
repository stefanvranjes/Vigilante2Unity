using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoundingBox
{
    public Vector3Int min, max;
}

public class HitDetection
{
    public VigObject self;
    public VigCollider collider1;
    public VigCollider collider2;
    public VigObject object1;
    public VigObject object2;
}

public class VigCollider : MonoBehaviour
{
    public ushort header; //0x00
    public ushort flags; //0x02
    public BoundingBox bounds; //0x04

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
