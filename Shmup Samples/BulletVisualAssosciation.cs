using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BulletVisualAssosciation {
    [field: SerializeField] public BulletVisual visual { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
}
