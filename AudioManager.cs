using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public GameMaster gameMaster;

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
}
