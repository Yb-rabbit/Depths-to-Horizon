using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ColliosionNext : MonoBehaviour
{
    public AudioClip collisionSound; // ��ײʱ���ŵ���Ƶ
    public string nextSceneName; // Ҫ���ص���һ����������
    public string targetTag = "Player"; // ָ����ײ����ı�ǩ "Player"
    public AudioMixerGroup audioMixerGroup; // ��Ƶ��������   
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSound;
        audioSource.outputAudioMixerGroup = audioMixerGroup; // ������Ƶ��������
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag(targetTag) && collisionSound != null)
        {
            Debug.Log("Target tag matched and collision sound is not null.");
            audioSource.Play();
            StartCoroutine(WaitForSoundAndLoadScene());
        }
    }

    IEnumerator WaitForSoundAndLoadScene()
    {
        Debug.Log("Waiting for sound to finish...");
        yield return new WaitForSeconds(audioSource.clip.length);
        Debug.Log("Loading next scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
