using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtils
{
    public static bool DotTest(Transform transform, Transform other, Vector2 testDirection, float threshold = 0.5f)
    {
        Vector2 direction = other.position - transform.position;
        direction.Normalize();
        return Vector2.Dot(direction, testDirection) > threshold;
    }
}
