using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Bgm,
    Effect,
    MaxCount
}

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];

    /* Singleton */
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SoundManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
        Init();
    }

    public void Init()
    {
        GameObject root = GameObject.Find("@SoundManager");

        if (root == null)
        {
            root = new GameObject("@SoundManager");
            Object.DontDestroyOnLoad(root);

            for (int i = 0; i < (int)Sound.MaxCount; i++)
            {
                GameObject go = new GameObject(System.Enum.GetNames(typeof(Sound))[i]);
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    public void Play(AudioClip audioClip, Sound soundtype = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource = _audioSources[(int)soundtype];
        audioSource.pitch = pitch;

        if (soundtype == Sound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }

        if (soundtype == Sound.Effect)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    #region Sound
    public AudioClip bgm;
    public AudioClip gun;

    public void BgmExample()
    {
        this.Play(bgm, Sound.Bgm);
    }

    public void EffectExample()
    {
        this.Play(gun);
    }
    #endregion
}