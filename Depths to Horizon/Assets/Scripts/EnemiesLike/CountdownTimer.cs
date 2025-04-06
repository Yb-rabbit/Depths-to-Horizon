using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System;


public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI�ı��������
    public Text statusText; // ״̬�ı��������
    public float startTime = 120.0f; // ����ʱ��ʼʱ�䣨�룩
    public string objectsToCheckTag; // Ҫ���������ǩ
    public int minObjectCount = 5; // ��С��������
    public GameObject[] objectsToActivate; // Ҫ�������������
    public float shrinkDuration = 2.0f; // ��С����ʱ�䣨�룩
    public string textA = "Uh, wait a little longer?"; // �ı�A
    public string textB = "Thank you, I have finally found it"; // �ı�B

    public float currentTime; // ��ǰʱ��
    public bool isTimerEnded = false; // ��ǵ���ʱ�Ƿ����
    private bool isTextBDisplayed = false; // ����ı�B�Ƿ�����ʾ

    public event Action OnHalfTimeReached; // ����ʱ��һ��ʱ�������¼�

    void Start()
    {
        currentTime = startTime; // ��ʼ����ǰʱ��Ϊ��ʼʱ��
        timerText.text = FormatTime(currentTime); // ��ʾ��ʼʱ��
        statusText.text = ""; // ��ʼ��״̬�ı�Ϊ��
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // ÿ֡����ʱ��
            timerText.text = FormatTime(currentTime); // ������ʾʱ��

            // ������ʱ��һ��ʱ�����¼�
            if (currentTime <= startTime / 2 && OnHalfTimeReached != null)
            {
                OnHalfTimeReached.Invoke();
                OnHalfTimeReached = null; // ȷ���¼�ֻ����һ��
            }
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

        // �����������С����Сֵ���򼤻��������岢���ٱ�ǩ����
        if (objectCount < minObjectCount)
        {
            Debug.Log("Object count is less than minObjectCount. Activating other objects and destroying tagged objects.");
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            StartCoroutine(ShrinkAndDestroyObjects(objectsToCheck));
            timerText.text = "END!"; // ����ʱ����
        }
        // ��������������ڵ�����Сֵ�������ü�ʱ�������ٱ�ǩ����
        else
        {
            Debug.Log("Object count is greater than or equal to minObjectCount. Resetting timer and destroying tagged objects.");
            StartCoroutine(ShrinkAndDestroyObjects(objectsToCheck, true));
            if (!isTextBDisplayed)
            {
                statusText.text = textA; // ����״̬�ı�Ϊ�ı�A
            }
        }
    }

    // Э�̣�����С����������
    private IEnumerator ShrinkAndDestroyObjects(GameObject[] objectsToCheck, bool resetTimer = false)
    {
        float elapsedTime = 0f;
        string shrinkDurationStr = shrinkDuration.ToString("F1") + " ��"; // ����С����ʱ����ӻ�

        Debug.Log("Starting to shrink objects over " + shrinkDurationStr);

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

        Debug.Log("Objects shrunk and destroyed.");

        if (resetTimer)
        {
            ResetTimer();
        }
        else
        {
            isTimerEnded = false; // �������ʱ��
            if (!isTextBDisplayed)
            {
                statusText.text = textB; // ����״̬�ı�Ϊ�ı�B
                isTextBDisplayed = true; // ����ı�B����ʾ
            }
        }
    }

    // ���ü�ʱ��
    public void ResetTimer()
    {
        currentTime = startTime;
        timerText.text = FormatTime(currentTime); // ������ʾʱ��
        isTimerEnded = false; // ���õ���ʱ�������
        if (!isTextBDisplayed)
        {
            statusText.text = textA; // ����״̬�ı�Ϊ�ı�A
        }
        OnHalfTimeReached?.Invoke(); // ���´�������ʱ��һ��ʱ���¼�
    }
}
