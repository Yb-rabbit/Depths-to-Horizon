using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicList : MonoBehaviour
{
    public List<AudioClip> musicClips; // ��Ƶ�����б�
    public List<AudioMixerGroup> mixerGroups; // ����������б�
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.05f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // ��������˥��ģʽ
        audioSource.minDistance = 1.0f; // ������С����
        audioSource.maxDistance = 50.0f; // ����������

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
            // ��������һ����Ƶ��ѭ�����Ÿ���Ƶ
            PlayCurrentClip();
        }
        else
        {
            // ���򲥷���һ����Ƶ
            currentClipIndex = (currentClipIndex + 1) % musicClips.Count;
            PlayCurrentClip();
        }
    }
}
