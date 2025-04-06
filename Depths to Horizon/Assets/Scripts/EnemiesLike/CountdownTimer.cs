using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System;


public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI文本组件引用
    public Text statusText; // 状态文本组件引用
    public float startTime = 120.0f; // 倒计时开始时间（秒）
    public string objectsToCheckTag; // 要检查的物体标签
    public int minObjectCount = 5; // 最小物体数量
    public GameObject[] objectsToActivate; // 要激活的物体数组
    public float shrinkDuration = 2.0f; // 缩小持续时间（秒）
    public string textA = "Uh, wait a little longer?"; // 文本A
    public string textB = "Thank you, I have finally found it"; // 文本B

    public float currentTime; // 当前时间
    public bool isTimerEnded = false; // 标记倒计时是否结束
    private bool isTextBDisplayed = false; // 标记文本B是否已显示

    public event Action OnHalfTimeReached; // 倒计时到一半时触发的事件

    void Start()
    {
        currentTime = startTime; // 初始化当前时间为开始时间
        timerText.text = FormatTime(currentTime); // 显示初始时间
        statusText.text = ""; // 初始化状态文本为空
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // 每帧减少时间
            timerText.text = FormatTime(currentTime); // 更新显示时间

            // 当倒计时到一半时触发事件
            if (currentTime <= startTime / 2 && OnHalfTimeReached != null)
            {
                OnHalfTimeReached.Invoke();
                OnHalfTimeReached = null; // 确保事件只触发一次
            }
        }
        else if (!isTimerEnded)
        {
            isTimerEnded = true; // 标记倒计时结束
            CheckAndHandleEndOfTimer(); // 检查并处理倒计时结束
        }
    }

    // 格式化时间显示为秒
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        return seconds.ToString();
    }

    // 检查并处理倒计时结束
    private void CheckAndHandleEndOfTimer()
    {
        // 查找所有具有指定标签的物体
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        Debug.Log("Checking objects. Found " + objectCount + " objects with tag " + objectsToCheckTag);

        // 如果物体数量小于最小值，则激活其他物体并销毁标签物体
        if (objectCount < minObjectCount)
        {
            Debug.Log("Object count is less than minObjectCount. Activating other objects and destroying tagged objects.");
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            StartCoroutine(ShrinkAndDestroyObjects(objectsToCheck));
            timerText.text = "END!"; // 倒计时结束
        }
        // 如果物体数量大于等于最小值，则重置计时器并销毁标签物体
        else
        {
            Debug.Log("Object count is greater than or equal to minObjectCount. Resetting timer and destroying tagged objects.");
            StartCoroutine(ShrinkAndDestroyObjects(objectsToCheck, true));
            if (!isTextBDisplayed)
            {
                statusText.text = textA; // 设置状态文本为文本A
            }
        }
    }

    // 协程：逐渐缩小并销毁物体
    private IEnumerator ShrinkAndDestroyObjects(GameObject[] objectsToCheck, bool resetTimer = false)
    {
        float elapsedTime = 0f;
        string shrinkDurationStr = shrinkDuration.ToString("F1") + " 秒"; // 将缩小持续时间可视化

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

        // 确保物体完全消失
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
            isTimerEnded = false; // 结束检查时间
            if (!isTextBDisplayed)
            {
                statusText.text = textB; // 设置状态文本为文本B
                isTextBDisplayed = true; // 标记文本B已显示
            }
        }
    }

    // 重置计时器
    public void ResetTimer()
    {
        currentTime = startTime;
        timerText.text = FormatTime(currentTime); // 更新显示时间
        isTimerEnded = false; // 重置倒计时结束标记
        if (!isTextBDisplayed)
        {
            statusText.text = textA; // 设置状态文本为文本A
        }
        OnHalfTimeReached?.Invoke(); // 重新触发倒计时到一半时的事件
    }
}
