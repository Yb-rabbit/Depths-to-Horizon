using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeNext : MonoBehaviour
{
    private float timer = 0f;
    public string sceneName; // 指定场景名称

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
