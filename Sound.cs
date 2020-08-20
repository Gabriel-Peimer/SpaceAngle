using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]//slider for volume
    public float volume;

    [Range(.1f, 3f)]//slider for pitch
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
