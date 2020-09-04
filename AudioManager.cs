using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public GameMaster gameMaster;

    private static bool keepFadingIn;
    private static bool keepFadingOut;

    void Awake()
    {
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        if (gameMaster.isMusicEnabled)
        {
            PlayAudio("MainTheme");
        }
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){ return; }
        if (s.name != "MainTheme")
        {
            if (!gameMaster.areSoundsEnabled)
            {
                return;
            }
        }
        s.source.Play();
    }
    public void StopAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { return; }
        s.source.Stop();
    }

    static IEnumerator FadeIn(string name, float speed, float maxVolume, Sound[] sounds)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.volume = 0;
        float audioVolume = s.source.volume;

        while (s.source.volume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            s.source.volume = audioVolume;

            yield return new WaitForSeconds(0.1f);
        }
    }
    static IEnumerator FadeOut(string name, float speed, Sound[] sounds)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        float audioVolume = s.source.volume;

        while (s.source.volume >= speed && keepFadingOut)
        {
            audioVolume -= speed;
            s.source.volume = audioVolume;

            yield return new WaitForSeconds(0.1f);
        }
    }
    public void FadeInCaller(string name, float speed, float maxVolume, Sound[] sounds)
    {
        StartCoroutine(FadeIn(name, speed, maxVolume, sounds));
    }
    public void FadeOutCaller(string name, float speed, Sound[] sounds)
    {
        StartCoroutine(FadeOut(name, speed, sounds));
    }
}
