/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       管理音频
****************************************************************************************/


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        public AudioMixerGroup mixerGroup;
        [Range(0, 1)]
        public float volume;
        public bool loop;
        public bool playOnAwake;
    }

    public List<Sound> soundList;
    public Dictionary<string, AudioSource> audioSources;
    public AudioMixer audioMixer;

    protected override void Awake()
    {
        base.Awake();

        audioSources = new Dictionary<string, AudioSource>();

        // 初始化所有音频
        foreach (var sound in soundList)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform, false);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.outputAudioMixerGroup = sound.mixerGroup;
            source.volume = sound.volume;
            source.loop = sound.loop;
            if (sound.playOnAwake)
            {
                source.Play();
            }

            audioSources.Add(sound.clip.name, source);
        }
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="name"></param>     // 音频的名字
    /// <param name="rePlay"></param>   // 是否重新播放音频
    public static void PlayAudio(string name, bool rePlay = false)
    {
        if (Instance == null) return;

        if (!Instance.audioSources.ContainsKey(name))
        {
            Debug.LogError($"AudioManager have no {name}");
            return;
        }

        if (!rePlay)
        {
            Instance.audioSources[name].Play();
        }
        else if (!Instance.audioSources[name].isPlaying)
        {
            Instance.audioSources[name].Play();
        }
    }

    /// <summary>
    /// 停止播放音频
    /// </summary>
    /// <param name="name"></param>
    public static void StopAudio(string name)
    {
        if (Instance == null) return;

        if (!Instance.audioSources.ContainsKey(name))
        {
            Debug.LogError($"AudioManager have no {name}");
            return;
        }
        Instance.audioSources[name].Stop();
    }

    public static bool IsPlaying(string name)
    {
        if (Instance == null) return false;
        return Instance.audioSources[name].isPlaying;
    }

    public static AudioSource GetAudioSource(string name)
    {
        return Instance.audioSources[name];
    }

    public static void CloseMasterAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("Master", -80);
    }

    public static void OpenMasterAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("Master", 0);
    }

    public static void CloseBGMAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("BGM", -80);
    }

    public static void OpenBGMAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("BGM", 0);
    }

    public static void CloseFXAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("FX", -80);
    }

    public static void OpenFXAudio()
    {
        if (Instance == null) return;

        Instance.audioMixer.SetFloat("FX", 0);
    }
}
