using UnityEngine;
using UnityEngine.Audio;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // 引用AudioSource组件
    public AudioMixerGroup mixerGroup; // 引用AudioMixerGroup
    public float delayTime = 3.0f; // 延时时间（秒）

    void Start()
    {
        // 确保AudioSource组件已正确绑定
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // 确保AudioSource组件已正确绑定混音器轨道
        if (mixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
        }

        // 调用PlayDelayed方法实现延时播放
        audioSource.PlayDelayed(delayTime);
    }
}
