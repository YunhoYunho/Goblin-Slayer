using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleUtils
{
    public static Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
