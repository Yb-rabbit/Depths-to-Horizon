using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ChangeToPoint : MonoBehaviour
{
    public string roomSceneName; // Ŀ�귿��ĳ�������
    public Image fadeImage; // ����ȫ����ɫͼ��
    public float fadeDuration = 2.0f; // �������ʱ��

    public void LoadRoom()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    // ���䲢���س�����Э��
    private IEnumerator FadeAndLoadScene()
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

        // ����ָ���ķ��䳡��
        SceneManager.LoadScene(roomSceneName);
    }
}
