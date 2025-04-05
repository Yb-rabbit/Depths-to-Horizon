using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI�ı��������
    public float startTime = 120.0f; // ����ʱ��ʼʱ�䣨�룩
    public string objectsToCheckTag; // Ҫ���������ǩ
    public int minObjectCount = 5; // ��С��������
    public int maxObjectCount = 10; // �����������
    public GameObject[] objectsToActivate; // Ҫ�������������

    private float currentTime; // ��ǰʱ��

    void Start()
    {
        currentTime = startTime; // ��ʼ����ǰʱ��Ϊ��ʼʱ��
        timerText.text = FormatTime(currentTime); // ��ʾ��ʼʱ��
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // ÿ֡����ʱ��
            timerText.text = FormatTime(currentTime); // ������ʾʱ��
        }
        else
        {
            timerText.text = "END!"; // ����ʱ����
            CheckAndActivateObjects(); // ��鲢��������
        }
    }

    // ��ʽ��ʱ����ʾΪ��
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        return seconds.ToString();
    }

    // �������������������������
    private void CheckAndActivateObjects()
    {
        // �������о���ָ����ǩ������
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        // �����������С����Сֵ���򼤻���������
        if (objectCount < minObjectCount)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
        // ������������������ֵ�������¼�ʱ
        else if (objectCount > maxObjectCount)
        {
            currentTime = startTime;
            timerText.text = FormatTime(currentTime); // ������ʾʱ��
        }
    }
}
