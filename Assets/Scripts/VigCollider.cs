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
    public VigObject self; //0x00
    public BufferedBinaryReader collider1; //0x04
    public BufferedBinaryReader collider2; //0x08
    public VigObject object1; //0x0C
    public VigObject object2; //0x10
    public Vector3Int position; //0x14
    public Vector3Int normal1; //0x20
    public Vector3Int normal2; //0x28
    public int distance; //0x30

    public HitDetection(byte[] b)
    {
        self = null;
        collider1 = new BufferedBinaryReader(b);
        collider2 = new BufferedBinaryReader(b);
        object1 = null;
        object2 = null;
        position = new Vector3Int();
        normal1 = new Vector3Int();
        normal2 = new Vector3Int();
        distance = 0;
    }
}

[System.Serializable]
public class VigCollider
{
    public byte[] buffer;
    public BufferedBinaryReader reader;

    public VigCollider(byte[] b)
    {
        buffer = b;
    }
}
