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
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundObject.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(soundObject.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
        s.playingLoop = false;
        // s.coroutineRunning = false;
    }

    public IEnumerator PlayLoop(string name, float delay, float waitFirst) {
        yield return new WaitForSeconds(waitFirst);
        Sound s = Array.Find(soundObject.sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            yield break;
        }
        if(s.coroutineRunning) yield break;
        s.coroutineRunning = true;
        s.playingLoop = true;
        s.loopDelay = delay;
        while(s.playingLoop)
        {
            // Debug.Log(name);
            Play(name);
            yield return new WaitForSeconds(s.loopDelay);
            if (!s.playingLoop) s.coroutineRunning = false;
        }
        s.coroutineRunning = false;
    }

    public void SetLoopDelay(string name, float newDelay)
    {
        Sound s = Array.Find(soundObject.sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.loopDelay = newDelay;
    }

    public Sound getSound(string name)
    {
        return Array.Find(soundObject.sounds, sound => sound.name == name);
    }
}