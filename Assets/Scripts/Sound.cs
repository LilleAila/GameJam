using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1;
    [Range(0.1f, 3f)] public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    public bool playingLoop = false;

    [HideInInspector]
    public float loopDelay;

    [HideInInspector]
    public bool coroutineRunning = false;
}