using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    // Khóa lưu PlayerPrefs
    public const string VolumeKey = "MasterVolume";

    // Lưu lại volume hiện tại (0..1)
    public float MasterVolume { get; private set; } = 0.5f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        // Tạo AudioSource cho từng sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.ignoreListenerPause = true;
        }

        // ---- Load volume đã lưu (mặc định 0.5) & áp dụng ngay
        float saved = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        SetVolume(saved, save: false);
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        if (!s.loop)
            s.source.PlayOneShot(s.clip, MasterVolume); // dùng volume hiện tại
        else
        {
            s.source.volume = MasterVolume;
            s.source.Play();
        }
    }

    /// <summary>
    /// Set volume 0..1, mặc định sẽ lưu PlayerPrefs
    /// </summary>
    public void SetVolume(float volume, bool save = true)
    {
        MasterVolume = Mathf.Clamp01(volume);

        foreach (Sound s in sounds)
        {
            // nếu clip đang phát lặp, cập nhật trực tiếp
            if (s.source != null && s.loop)
                s.source.volume = MasterVolume;
        }

        if (save)
        {
            PlayerPrefs.SetFloat(VolumeKey, MasterVolume);
            PlayerPrefs.Save();
        }
    }
}
