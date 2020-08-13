using System;
using UnityEngine;
using UnityEngine.Audio;

public class audioManagerScript : MonoBehaviour
{

    [Header("AUDIO")]
    public soundClassScript[] sounds;
    public static audioManagerScript instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (soundClassScript s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        playSound("BGM");
    }
    public void playSound(string name)
    {
        soundClassScript s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

}
