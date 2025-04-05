using UnityEngine;
using UnityEngine.Audio;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // ����AudioSource���
    public AudioMixerGroup mixerGroup; // ����AudioMixerGroup
    public float delayTime = 3.0f; // ��ʱʱ�䣨�룩

    void Start()
    {
        // ȷ��AudioSource�������ȷ��
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // ȷ��AudioSource�������ȷ�󶨻��������
        if (mixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
        }

        // ����PlayDelayed����ʵ����ʱ����
        audioSource.PlayDelayed(delayTime);
    }
}
