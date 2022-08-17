using System;
using System.Numerics;
using UnityEngine;

public static class Expansions
{
    public static System.Numerics.Vector3 Parse(this UnityEngine.Vector3 vector)
    {
        return new System.Numerics.Vector3(vector.x,vector.y,vector.z);
    }

    public static UnityEngine.Vector3 Parse(this System.Numerics.Vector3 vector)
    {
        return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
    }

    public static UnityEngine.Quaternion Parse(this System.Numerics.Quaternion quaternion)
    {
        return new UnityEngine.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }

    public static System.Numerics.Quaternion Parse(this UnityEngine.Quaternion quaternion)
    {
        return new System.Numerics.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }

}
