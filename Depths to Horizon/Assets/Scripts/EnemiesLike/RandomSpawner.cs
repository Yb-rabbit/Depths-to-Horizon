using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Ҫ���ɵ�����Ԥ����
    public Transform target; // ׷�ٵ�Ŀ������
    public int spawnCount = 5; // ��ʼ��������
    public int maxSpawnCount = 20; // �����������
    public float spawnRadius = 10.0f; // ��������뾶
    public float checkInterval = 20.0f; // �����ʱ�䣨�룩

    private int totalSpawnedCount = 0; // ��ǰ����������

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
                break; // ����Ѵﵽ���������������ֹͣ����
            }

            // ��ָ���������������λ��
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius + transform.position;
            randomPos.y = transform.position.y; // ȷ��������ͬһ�߶�

            // �������岢����׷��Ŀ��
            GameObject spawnedObject = Instantiate(objectPrefab, randomPos, Quaternion.identity);
            Tracker tracker = spawnedObject.GetComponent<Tracker>();
            if (tracker != null)
            {
                tracker.target = target;
            }

            totalSpawnedCount++; // ���µ�ǰ����������
        }
    }

    private IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // ��⵱ǰ���ɵ���������
            int currentCount = GameObject.FindGameObjectsWithTag(objectPrefab.tag).Length;

            // ����������㣬����δ�ﵽ��������������������µ�����
            if (currentCount < spawnCount && totalSpawnedCount < maxSpawnCount)
            {
                int objectsToSpawn = Mathf.Min(spawnCount - currentCount, maxSpawnCount - totalSpawnedCount);
                SpawnObjects(objectsToSpawn);
            }
        }
    }
}
