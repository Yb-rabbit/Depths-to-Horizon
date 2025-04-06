using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ChangeToPointTime : MonoBehaviour
{
    public string roomSceneName; // Ŀ�귿��ĳ�������
    public Image fadeImage; // ����ȫ����ɫͼ��
    public float fadeDuration = 2.0f; // �������ʱ��
    public float delayBeforeLoad = 5.0f; // �ӳ�ʱ��

    void Start()
    {
        StartCoroutine(DelayedLoadRoom());
    }

    private IEnumerator DelayedLoadRoom()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        StartCoroutine(FadeAndLoadScene());
    }

    public void LoadRoom()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    // ���䲢���س�����Э��
    private IEnumerator FadeAndLoadScene()
    {
        if (fadeImage != null)
        {
            // ȷ����ɫͼ������
            fadeImage.gameObject.SetActive(true);

            float elapsedTime = 0f;
            Color color = fadeImage.color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("Fade image is not assigned! Skipping fade effect.");
        }

        // ����ָ���ķ��䳡��
        SceneManager.LoadScene(roomSceneName);
    }
}
