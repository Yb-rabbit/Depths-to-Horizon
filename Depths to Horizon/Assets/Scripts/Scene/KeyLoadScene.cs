using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyLoadScene : MonoBehaviour
{
    public string targetSceneName;// Ŀ�곡������

    void Update()
    {
        // ����Ƿ��������
        if (Input.anyKeyDown)
        {
            // ����Ŀ�곡��
            SceneManager.LoadScene(targetSceneName);
        }
    }
}

