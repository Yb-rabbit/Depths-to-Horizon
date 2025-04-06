using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // 要生成的物体预制体
    public Transform target; // 追踪的目标物体
    public int spawnCount = 5; // 初始生成数量
    public int maxSpawnCount = 20; // 最大生成数量
    public float spawnRadius = 10.0f; // 生成区域半径
    public string customTag; // 手动指定的标签
    public CountdownTimer countdownTimer; // 引用 CountdownTimer

    private int totalSpawnedCount = 0; // 当前总生成数量
    private bool needsImmediateCheck = false; // 标志位，是否需要立即进行检查
    private bool hasCheckedAt20Seconds = false; // 标志位，是否已在剩余 20 秒时进行检查

    private void Start()
    {
        if (countdownTimer == null)
        {
            Debug.LogError("CountdownTimer reference is not set.");
            return;
        }

        countdownTimer.OnHalfTimeReached += () => needsImmediateCheck = true; // 订阅倒计时到一半时的事件

        SpawnObjects(spawnCount);
        StartCoroutine(CheckAndRespawn());
    }

    private void SpawnObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (totalSpawnedCount >= maxSpawnCount)
            {
                break; // 如果已达到最大生成数量，则停止生成
            }

            // 在指定区域内随机生成位置
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius + transform.position;
            randomPos.y = transform.position.y; // 确保生成在同一高度

            // 生成物体并设置追踪目标
            GameObject spawnedObject = Instantiate(objectPrefab, randomPos, Quaternion.identity);
            if (!string.IsNullOrEmpty(customTag))
            {
                spawnedObject.tag = customTag; // 使用手动指定的标签
            }
            else
            {
                spawnedObject.tag = objectPrefab.tag; // 使用预制体本体的标签
            }
            Tracker tracker = spawnedObject.GetComponent<Tracker>();
            if (tracker != null)
            {
                tracker.target = target;
            }

            totalSpawnedCount++; // 更新当前总生成数量
        }
    }

    private IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            // 检查是否为 CountdownTimer 的检查时间
            if (countdownTimer != null && countdownTimer.isTimerEnded)
            {
                // 等待 CountdownTimer 的检查时间结束
                while (countdownTimer.isTimerEnded)
                {
                    yield return null;
                }

                // 计时器重置后立即进行检查
                CheckAndRespawnImmediately();
                hasCheckedAt20Seconds = false; // 重置标志位
            }
            else if (needsImmediateCheck)
            {
                // 如果需要立即进行检查，则进行检查
                CheckAndRespawnImmediately();
                needsImmediateCheck = false; // 重置标志位
            }
            else if (countdownTimer != null && countdownTimer.currentTime <= 20.0f && !hasCheckedAt20Seconds)
            {
                // 如果计时器剩余时间等于 20 秒且尚未进行检查，则立即进行检查
                CheckAndRespawnImmediately();
                hasCheckedAt20Seconds = true; // 设置标志位，表示已在剩余 20 秒时进行检查
            }
            else
            {
                yield return new WaitForSeconds(1.0f); // 每秒检查一次
            }
        }
    }

    private void CheckAndRespawnImmediately()
    {
        // 检测当前生成的物体数量
        int currentCount = GameObject.FindGameObjectsWithTag(string.IsNullOrEmpty(customTag) ? objectPrefab.tag : customTag).Length;

        // 如果数量不足，并且未达到最大生成数量，则生成新的物体
        if (currentCount < spawnCount && totalSpawnedCount < maxSpawnCount)
        {
            int objectsToSpawn = Mathf.Min(spawnCount - currentCount, maxSpawnCount - totalSpawnedCount);
            SpawnObjects(objectsToSpawn);
        }
    }

    // 重置计时器时调用的方法
    public void OnTimerReset()
    {
        needsImmediateCheck = true; // 设置标志位，立即进行检查
        hasCheckedAt20Seconds = false; // 重置标志位
    }
}

