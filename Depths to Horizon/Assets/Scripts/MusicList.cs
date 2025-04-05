using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicList : MonoBehaviour
{
    public List<AudioClip> musicClips; // 音频剪辑列表
    public List<AudioMixerGroup> mixerGroups; // 混音器轨道列表
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.05f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // 设置音量衰减模式
        audioSource.minDistance = 1.0f; // 设置最小距离
        audioSource.maxDistance = 50.0f; // 设置最大距离

        if (musicClips.Count > 0)
        {
            PlayCurrentClip();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    void PlayCurrentClip()
    {
        audioSource.clip = musicClips[currentClipIndex];
        if (currentClipIndex < mixerGroups.Count)
        {
            audioSource.outputAudioMixerGroup = mixerGroups[currentClipIndex];
        }
        audioSource.Play();
    }

    void PlayNextClip()
    {
        if (currentClipIndex == musicClips.Count - 1)
        {
            // 如果是最后一个音频，循环播放该音频
            PlayCurrentClip();
        }
        else
        {
            // 否则播放下一个音频
            currentClipIndex = (currentClipIndex + 1) % musicClips.Count;
            PlayCurrentClip();
        }
    }
}
