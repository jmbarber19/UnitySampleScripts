using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The classic "HitStop" effect
/// Use sparingly on powerful sudden moments
/// </summary>
public class ScreenFreezer : MonoBehaviour
{
    [SerializeField] private float timeScaleRestore = 5f;

    public static ScreenFreezer instance;
    private float timeFrozen = 0f;

    /// <summary>
    /// Set the current temporary timescale
    /// </summary>
    /// <param name="newTimeScale">How slow time should be set to (Recommended low values eg. 0f-0.2f)</param>
    /// <param name="pTimeFrozen">How long the time will be slowed (Recommended low values eg. 0.1f)</param>
    public void SetTimeScale(float newTimeScale, float pTimeFrozen)
    {
        Time.timeScale = newTimeScale;
        timeFrozen = pTimeFrozen;
    }


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (timeFrozen >= 0f)
        {
            timeFrozen -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, timeScaleRestore * Time.unscaledDeltaTime);
        }
    }

}
