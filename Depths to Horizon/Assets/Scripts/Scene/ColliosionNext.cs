using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliosionNext : MonoBehaviour
{
    public AudioClip collisionSound; // 碰撞时播放的音频
    public string nextSceneName; // 要加载的下一个场景名称
    public string targetTag = "Player"; // 指定碰撞物体的标签 "Player"
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSound;
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
