using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGame : MonoBehaviour
{
    public Image fadeImage; // ����ȫ����ɫͼ��
    public float fadeDuration = 2.0f; // �������ʱ��
    public float delayBeforeExit = 0.5f; // ��ȫ��ں��ӳ��˳�ʱ��

    // ��ť�¼�������
    public void OnExitButtonPressed()
    {
        StartCoroutine(FadeAndExit());
    }

    // ���䲢�˳���Ϸ��Э��
    private IEnumerator FadeAndExit()
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

        // �ȴ�һ��ʱ����ȷ��ͼ����ȫ���
        yield return new WaitForSeconds(delayBeforeExit);

        // �˳���Ϸ
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
