using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache<T>
{
    private static Dictionary<Collider2D, T> dictCollider2D = new Dictionary<Collider2D, T>();

    public static T Get(Collider2D collider)
    {
        if (!dictCollider2D.ContainsKey(collider))
        {
            dictCollider2D.Add(collider, collider.GetComponent<T>());
        }

        return dictCollider2D[collider];
    }
}
