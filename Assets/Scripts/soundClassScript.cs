using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class soundClassScript
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    public bool loop;
    [HideInInspector] public AudioSource source;
}
