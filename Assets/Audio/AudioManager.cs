using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
   public Sound[] sounds;

   public static AudioManager instance;
   
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }



        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.ignoreListenerPause = true;

        }
    }

    private void Start()
    {
        Play("Theme");

    }

    public void Play(string name)
    {
       Sound s = Array.Find(sounds,sound => sound.name == name);
        if (s == null) return;
        if (!s.loop)
            s.source.PlayOneShot(s.clip);
        else
            s.source.Play();


    }

    public void SetVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }



}
