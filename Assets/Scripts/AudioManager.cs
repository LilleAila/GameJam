using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundObject soundObject;

    public static AudioManager instance;

    public bool dontDestroyOnLoad = true;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if(dontDestroyOnLoad) DontDestroyOnLoad(gameObject);

        foreach(Sound s in soundObject.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clips[0];

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = getSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        s.playing = true;
    }

    public void Stop(string name)
    {
        Sound s = getSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
        s.playingLoop = false;
        // s.coroutineRunning = false;

        s.playing = false;
    }

    public void SetClip(string name, int clipIndex)
    {
        Sound s = getSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.clip = s.clips[clipIndex];
    }

    public Sound getSound(string name)
    {
        return Array.Find(soundObject.sounds, sound => sound.name == name);
    }
}