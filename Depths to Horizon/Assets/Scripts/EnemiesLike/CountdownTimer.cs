using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI�ı��������
    public float startTime = 120.0f; // ����ʱ��ʼʱ�䣨�룩
    public string objectsToCheckTag; // Ҫ���������ǩ
    public int minObjectCount = 5; // ��С��������
    public GameObject[] objectsToActivate; // Ҫ�������������
    public float shrinkDuration = 2.0f; // ��С����ʱ�䣨�룩

    private float currentTime; // ��ǰʱ��
    private bool isTimerEnded = false; // ��ǵ���ʱ�Ƿ����

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
        else if (!isTimerEnded)
        {
            isTimerEnded = true; // ��ǵ���ʱ����
            CheckAndHandleEndOfTimer(); // ��鲢������ʱ����
        }
    }

    // ��ʽ��ʱ����ʾΪ��
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        return seconds.ToString();
    }

    // ��鲢������ʱ����
    private void CheckAndHandleEndOfTimer()
    {
        // �������о���ָ����ǩ������
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        Debug.Log("Checking objects. Found " + objectCount + " objects with tag " + objectsToCheckTag);

        // �����������С����Сֵ���򼤻���������
        if (objectCount < minObjectCount)
        {
            Debug.Log("Object count is less than minObjectCount. Activating other objects.");
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            timerText.text = "END!"; // ����ʱ����
        }
        // ������ھ���ָ����ǩ�����壬�����¼�ʱ
        else if (objectCount > 0)
        {
            Debug.Log("Objects with the specified tag exist. Resetting timer.");
            currentTime = startTime;
            timerText.text = FormatTime(currentTime); // ������ʾʱ��
            isTimerEnded = false; // ���õ���ʱ�������
        }
        // ������Сʣ�������
        else
        {
            timerText.text = "END!"; // ����ʱ����
            Debug.Log("Timer ended, starting ShrinkObjectsAndCheck coroutine.");
            StartCoroutine(ShrinkObjectsAndCheck()); // ��ʼ��С���岢�������
        }
    }

    // Э�̣�����С���岢�������
    private IEnumerator ShrinkObjectsAndCheck()
    {
        // �������о���ָ����ǩ������
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 0f, elapsedTime / shrinkDuration);

            foreach (GameObject obj in objectsToCheck)
            {
                if (obj != null)
                {
                    obj.transform.localScale = new Vector3(scale, scale, scale);
                }
            }

            yield return null;
        }

        // ȷ��������ȫ��ʧ
        foreach (GameObject obj in objectsToCheck)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        Debug.Log("Objects shrunk and destroyed. Checking and activating other objects.");
        // ��鲢������������
        CheckAndActivateObjects();
    }

    // �������������������������
    private void CheckAndActivateObjects()
    {
        // �������о���ָ����ǩ������
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        Debug.Log("Checking objects. Found " + objectCount + " objects with tag " + objectsToCheckTag);

        // �����������С����Сֵ���򼤻���������
        if (objectCount < minObjectCount)
        {
            Debug.Log("Object count is less than minObjectCount. Activating other objects.");
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
