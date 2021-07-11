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

[System.Serializable]
public class VigCollider
{
    public byte[] buffer;

    public VigCollider(byte[] b)
    {
        buffer = b;
    }
}
