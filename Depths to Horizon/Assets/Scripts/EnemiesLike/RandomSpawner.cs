using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // 要生成的物体预制体
    public Transform target; // 追踪的目标物体
    public int spawnCount = 5; // 初始生成数量
    public int maxSpawnCount = 20; // 最大生成数量
    public float spawnRadius = 10.0f; // 生成区域半径
    public float checkInterval = 20.0f; // 检测间隔时间（秒）

    private int totalSpawnedCount = 0; // 当前总生成数量

    private void Start()
    {
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
            yield return new WaitForSeconds(checkInterval);

            // 检测当前生成的物体数量
            int currentCount = GameObject.FindGameObjectsWithTag(objectPrefab.tag).Length;

            // 如果数量不足，并且未达到最大生成数量，则生成新的物体
            if (currentCount < spawnCount && totalSpawnedCount < maxSpawnCount)
            {
                int objectsToSpawn = Mathf.Min(spawnCount - currentCount, maxSpawnCount - totalSpawnedCount);
                SpawnObjects(objectsToSpawn);
            }
        }
    }
}
