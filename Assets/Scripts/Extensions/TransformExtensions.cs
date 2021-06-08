using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetZeroYPosition(this Transform transform)
    {
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }
}
