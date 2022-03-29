using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    enum Audio {
    BGM,
    Effect
    }

    private AudioSource[] audioSources;

    /* Singleton */
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (AudioManager._instance == null)
            {
                AudioManager._instance = new AudioManager();
            }
            return AudioManager._instance;
        }
    }

    void Awake()
    {
        AudioManager._instance = this;

        this.Initialize();
    }

    public void Initialize()
    {
        GameObject root = GameObject.Find("Audio");
        
        if (root == null)
        {
            root = new GameObject("Audio");

            for (int i = 0; i < Audio.MaxCount; i++)
            {
                GameObject go = new GameObject(Audio[i]);
                go.transform.parent = root.transform;
                this.audioSources.push(go.AddComponent<AudioSource>());
            }

            this.audioSources[Audio.Bgm].loop = true;
        }
    }

    public void Play(AudioClip audioClip, Audio audioType = Audio.Effect, float pitch = 1)
    {
        if (!audioClip) return;

        AudioSource audioSource = this.audioSources[audioType];
        audioSource.pitch = pitch;

        if (audioType == Audio.BGM)
        {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }

        if (audioType == Audio.Effect)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    // public AudioClip end;
    
    // public void BGMMain() {
    //     Play(end, Audio.BGM);
    // }

    // public void EffectGun() {
    //     Play(end);
    // }
}