using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ChangeToPoint : MonoBehaviour
{
    public string roomSceneName; // 目标房间的场景名称
    public Image fadeImage; // 拖入全屏黑色图像
    public float fadeDuration = 2.0f; // 渐变持续时间

    public void LoadRoom()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    // 渐变并加载场景的协程
    private IEnumerator FadeAndLoadScene()
    {
        // 确保黑色图像启用
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

        // 加载指定的房间场景
        SceneManager.LoadScene(roomSceneName);
    }
}
