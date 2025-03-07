using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A singleton class for handling camera shaking
/// </summary>
public class CameraShaker : MonoBehaviour {

    /// <summary>
    /// An element of cameraShakes, used to store current values of this shake to be performed on the camera
    /// </summary>
    private class CameraShakeProcess {
        public CameraShakeProcess(Vector2 pDirection, float pIntensity, float pDeltaTimeMultiplier, float startTime) {
            direction = pDirection;
            intensity = pIntensity;
            deltaTimeMultiplier = pDeltaTimeMultiplier;
            currentTime = startTime;
        }
        public Vector2 direction = Vector2.zero;
        public float currentTime = 0f;
        public float intensity = 0f;
        public float deltaTimeMultiplier = 1f;
    }


    private static CameraShaker instance = null;

    /// <summary>
    /// Animation curve representing screenshake offset distance (y axis) over time (x axis)
    /// I typically use a bouncing curve that goes from X and Y values of 0 to 1
    /// </summary>
    [SerializeField] private AnimationCurve offsetBounce = null;

    /// <summary>
    /// How much velocity affects the cameras Z position
    /// </summary>
    [SerializeField] private float maxShake = 2f;
    private List<CameraShakeProcess> cameraShakes;



    /// <summary>
    /// Add a new shake to the camera.
    /// </summary>
    /// <param name="direction">Direction for the camera to shake in</param>
    /// <param name="intensity">How intense the shake is</param>
    /// <param name="deltaTimeMultiplier">How quickly the bounce animation will play</param>
    /// <param name="startTime">Local starting time for when the shake will occur (defaults to 0, starting immediately)</param>
    public static void AddCameraShake(
        Vector2 direction,
        float intensity = 1f,
        float deltaTimeMultiplier = 1f,
        float startTime = 0f
    ) {
        instance.cameraShakes.Add(new CameraShakeProcess(direction.normalized, intensity, deltaTimeMultiplier, -startTime));
    }


    private void Start() {
        instance = this;
        cameraShakes = new List<CameraShakeProcess>();

#if UNITY_EDITOR
        bool foundCamera = false;
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).GetComponent<Camera>() != null) {
                foundCamera = true;
                break;
            }
        }

        if (!foundCamera) {
            Debug.LogError("CameraShaker \"" + gameObject.name + "\" does not have any children with a CameraController!");
        }
#endif

    }


    private void Update () {
        transform.localPosition = Vector3.zero;

        // Reverse looping allows in loop removal without affecting the index
        for (int i = cameraShakes.Count - 1; i >= 0; i--) {
            cameraShakes[i].currentTime += Time.deltaTime * cameraShakes[i].deltaTimeMultiplier;

            if (cameraShakes[i].currentTime > 0f) {
                float offsetBounceValue = offsetBounce.Evaluate(cameraShakes[i].currentTime);

                transform.localPosition += new Vector3(
                    cameraShakes[i].direction.x * cameraShakes[i].intensity * offsetBounceValue,
                    cameraShakes[i].direction.y * cameraShakes[i].intensity * offsetBounceValue,
                    0f
                );

                if (offsetBounce.keys.Length > 0 && cameraShakes[i].currentTime > offsetBounce.keys[offsetBounce.keys.Length - 1].time) {
                    cameraShakes.RemoveAt(i);
                }
            }
        }

        if (transform.localPosition.magnitude > maxShake) {
            transform.localPosition = transform.localPosition.normalized * maxShake;
        }
    }
}
