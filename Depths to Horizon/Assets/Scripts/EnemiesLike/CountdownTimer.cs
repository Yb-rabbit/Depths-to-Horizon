using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText; // UI文本组件引用
    public float startTime = 120.0f; // 倒计时开始时间（秒）
    public string objectsToCheckTag; // 要检查的物体标签
    public int minObjectCount = 5; // 最小物体数量
    public int maxObjectCount = 10; // 最大物体数量
    public GameObject[] objectsToActivate; // 要激活的物体数组

    private float currentTime; // 当前时间

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
        else
        {
            timerText.text = "END!"; // 倒计时结束
            CheckAndActivateObjects(); // 检查并激活物体
        }
    }

    // 格式化时间显示为秒
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        return seconds.ToString();
    }

    // 检查物体数量并激活其他物体
    private void CheckAndActivateObjects()
    {
        // 查找所有具有指定标签的物体
        GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectsToCheckTag);
        int objectCount = objectsToCheck.Length;

        // 如果物体数量小于最小值，则激活其他物体
        if (objectCount < minObjectCount)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
        // 如果物体数量大于最大值，则重新计时
        else if (objectCount > maxObjectCount)
        {
            currentTime = startTime;
            timerText.text = FormatTime(currentTime); // 更新显示时间
        }
    }
}
