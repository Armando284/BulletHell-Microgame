using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();
    }

    private Sound Find(string name) => Array.Find(sounds, sound => sound.name == name);

    public void Play(string name)
    {
        Sound s = Find(name);
        if (s == null)
            return;

        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;
        source.Play();
    }

    public void Stop()
    {
        Sound s = Find(name);
        if (s == null)
            return;

        source.Stop();
    }
}
