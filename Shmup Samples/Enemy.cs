using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletPattern[] patterns;

    private void Start() {
        for (int i = 0; i < patterns.Length; i++) {
            patterns[i].Reset();
        }
    }

    private void Update() {
        for (int i = 0; i < patterns.Length; i++) {
            if (patterns[i] != null && !patterns[i].IsDone()) {
                patterns[i].Step(transform.position);
                if (patterns[i].IsDone() && i+1 < patterns.Length) {
                    patterns[i+1].Reset();
                }
                break;
            }
        }
    }

}
