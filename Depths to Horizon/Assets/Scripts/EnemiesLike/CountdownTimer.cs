using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI文本组件引用
    public float startTime = 120.0f; // 倒计时开始时间（秒）
    public string objectsToCheckTag; // 要检查的物体标签
    public int minObjectCount = 5; // 最小物体数量
    public GameObject[] objectsToActivate; // 要激活的物体数组
    public float shrinkDuration = 2.0f; // 缩小持续时间（秒）

    private float currentTime; // 当前时间
    private bool isTimerEnded = false; // 标记倒计时是否结束

    void Start()
    {
        currentTime = startTime; // 初始化当前时间为开始时间
        timerText.text = FormatTime(currentTime); // 显示初始时间
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // 每帧减少时间
            timerText.text = FormatTime(currentTime); // 更新显示时间
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

        // 如果物体数量小于最小值，则激活其他物体
        if (objectCount < minObjectCount)
        {
            Debug.Log("Object count is less than minObjectCount. Activating other objects.");
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            timerText.text = "END!"; // 倒计时结束
        }
        // 如果存在具有指定标签的物体，则重新计时
        else if (objectCount > 0)
        {
            Debug.Log("Objects with the specified tag exist. Resetting timer.");
            currentTime = startTime;
            timerText.text = FormatTime(currentTime); // 更新显示时间
            isTimerEnded = false; // 重置倒计时结束标记
        }
        // 否则，缩小剩余的物体
        else
        {
            timerText.text = "END!"; // 倒计时结束
            Debug.Log("Timer ended, starting ShrinkObjectsAndCheck coroutine.");
            StartCoroutine(ShrinkObjectsAndCheck()); // 开始缩小物体并检查条件
        }
    }

    // 协程：逐渐缩小物体并检查条件
    private IEnumerator ShrinkObjectsAndCheck()
    {
        // 查找所有具有指定标签的物体
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

        // 确保物体完全消失
        foreach (GameObject obj in objectsToCheck)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        Debug.Log("Objects shrunk and destroyed. Checking and activating other objects.");
        // 检查并激活其他物体
        CheckAndActivateObjects();
    }

    // 检查物体数量并激活其他物体
    private void CheckAndActivateObjects()
    {
        // 查找所有具有指定标签的物体
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        Debug.Log("Checking objects. Found " + objectCount + " objects with tag " + objectsToCheckTag);

        // 如果物体数量小于最小值，则激活其他物体
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
