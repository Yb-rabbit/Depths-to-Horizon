using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    public Text[] texts; // �������ı�Ԫ����ק�����������
    private int currentTextIndex = 0; // ��ǰ��ʾ���ı�����

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            SwitchText();
        }
    }

    void SwitchText()
    {
        // ���ص�ǰ��ʾ���ı�
        if (currentTextIndex < texts.Length)
        {
            texts[currentTextIndex].gameObject.SetActive(false);
        }

        // �л�����һ���ı�
        currentTextIndex = (currentTextIndex + 1) % texts.Length;

        // ��ʾ�µ��ı�
        if (currentTextIndex < texts.Length)
        {
            texts[currentTextIndex].gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // ��ʼ����ֻ��ʾ��һ���ı������������ı�
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(i == 0);
        }
    }
}
