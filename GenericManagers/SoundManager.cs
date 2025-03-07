using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    [Header("== Core ==")]
    [SerializeField] private AudioSource[] sources;

    [Header("== Sounds ==")]
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;


    private static SoundManager instance;


    private void Start() {
        instance = this;
        instance.sources = GetComponentsInChildren<AudioSource>();
    }

    private static AudioClip FetchAudioClip(Sound sound) {
        switch (sound) {
            case Sound.Sound1: return instance.sound1;
            case Sound.Sound2: return instance.sound2;
            // Define sound matching patterns here
            default:
                Debug.LogError("Invalid Sound: " + sound);
                return null;
        }
    }

    private void Update() {
        for (int i = 0; i < instance.sources.Length; i++) {
            var currentSource = instance.sources[i];
            if (!currentSource.isPlaying) {
                currentSource.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="sound">The Sound enum value of the sound to play</param>
    /// <param name="delay">How many seconds should the play be delayed by</param>
    /// <param name="pitch">Played sound pitch</param>
    public static void PlaySound(
        Sound sound,
        float delay = 0f,
        float pitch = 1f
    ) {
        for (int i = 0; i < instance.sources.Length; i++) {
            var currentSource = instance.sources[i];
            if (!currentSource.isPlaying) {
                currentSource.pitch = pitch;
                currentSource.clip = FetchAudioClip(sound);
                currentSource.gameObject.SetActive(true);
                currentSource.PlayDelayed(Mathf.Max(0f, delay));
                return;
            }
        }
    }
}
