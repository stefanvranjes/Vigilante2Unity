using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoundingBox
{
    public Vector3Int min, max;
}

public struct Radius
{
    public Vector3Int matrixSV;
    public int contactOffset;
}

public class HitDetection
{
    public VigObject self;
    public BinaryReader collider1;
    public BinaryReader collider2;
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
