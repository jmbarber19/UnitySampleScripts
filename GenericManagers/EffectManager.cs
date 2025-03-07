using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("=== Core ===")]
    [SerializeField] private GameObject holder;

    [Header("=== VFX ===")]
    [SerializeField] private GameObject skipEffect;
    [SerializeField] private GameObject splashEffect;
    [SerializeField] private GameObject skipSplashEffect;
    [SerializeField] private GameObject bobEffect;
    [SerializeField] private GameObject catchEffect;
    [SerializeField] private GameObject retrieveEffect;
    [SerializeField] private GameObject waterRippleEffect;
    [SerializeField] private GameObject smallRippleEffect;

    private static EffectManager instance;

    // Start is called before the first frame update
    private void Start() {
        instance = this;
    }

    public static void Skip(Vector3 position) {
        Instantiate(instance.skipEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
        Instantiate(instance.skipSplashEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void Splash(Vector3 position) {
        Instantiate(instance.splashEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void Bob(Vector3 position) {
        Instantiate(instance.bobEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void Catch(Vector3 position) {
        Instantiate(instance.catchEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void Retrieve(Vector3 position) {
        Instantiate(instance.retrieveEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void Ripple(Vector3 position) {
        Instantiate(instance.waterRippleEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

    public static void SmallRipple(Vector3 position) {
        Instantiate(instance.smallRippleEffect, position, Quaternion.identity).transform.parent = instance.holder.transform;
    }

}
