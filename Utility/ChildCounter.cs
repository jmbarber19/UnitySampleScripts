using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Counts the number of children in a given object
/// Useful for GameObject holders that manage pooled GameObjects
/// </summary>
public class ChildCounter : MonoBehaviour
{
    [SerializeField] private int children = 0;


    void Update()
    {
        children = transform.childCount;
    }
}
