using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Ҫ���ɵ�����Ԥ����
    public Transform target; // ׷�ٵ�Ŀ������
    public int spawnCount = 5; // ��ʼ��������
    public int maxSpawnCount = 20; // �����������
    public float spawnRadius = 10.0f; // ��������뾶
    public string customTag; // �ֶ�ָ���ı�ǩ
    public CountdownTimer countdownTimer; // ���� CountdownTimer

    private int totalSpawnedCount = 0; // ��ǰ����������
    private bool needsImmediateCheck = false; // ��־λ���Ƿ���Ҫ�������м��
    private bool hasCheckedAt20Seconds = false; // ��־λ���Ƿ�����ʣ�� 20 ��ʱ���м��

    private void Start()
    {
        if (countdownTimer == null)
        {
            Debug.LogError("CountdownTimer reference is not set.");
            return;
        }

        countdownTimer.OnHalfTimeReached += () => needsImmediateCheck = true; // ���ĵ���ʱ��һ��ʱ���¼�

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
            if (!string.IsNullOrEmpty(customTag))
            {
                spawnedObject.tag = customTag; // ʹ���ֶ�ָ���ı�ǩ
            }
            else
            {
                spawnedObject.tag = objectPrefab.tag; // ʹ��Ԥ���屾��ı�ǩ
            }
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
            // ����Ƿ�Ϊ CountdownTimer �ļ��ʱ��
            if (countdownTimer != null && countdownTimer.isTimerEnded)
            {
                // �ȴ� CountdownTimer �ļ��ʱ�����
                while (countdownTimer.isTimerEnded)
                {
                    yield return null;
                }

                // ��ʱ�����ú��������м��
                CheckAndRespawnImmediately();
                hasCheckedAt20Seconds = false; // ���ñ�־λ
            }
            else if (needsImmediateCheck)
            {
                // �����Ҫ�������м�飬����м��
                CheckAndRespawnImmediately();
                needsImmediateCheck = false; // ���ñ�־λ
            }
            else if (countdownTimer != null && countdownTimer.currentTime <= 20.0f && !hasCheckedAt20Seconds)
            {
                // �����ʱ��ʣ��ʱ����� 20 ������δ���м�飬���������м��
                CheckAndRespawnImmediately();
                hasCheckedAt20Seconds = true; // ���ñ�־λ����ʾ����ʣ�� 20 ��ʱ���м��
            }
            else
            {
                yield return new WaitForSeconds(1.0f); // ÿ����һ��
            }
        }
    }

    private void CheckAndRespawnImmediately()
    {
        // ��⵱ǰ���ɵ���������
        int currentCount = GameObject.FindGameObjectsWithTag(string.IsNullOrEmpty(customTag) ? objectPrefab.tag : customTag).Length;

        // ����������㣬����δ�ﵽ��������������������µ�����
        if (currentCount < spawnCount && totalSpawnedCount < maxSpawnCount)
        {
            int objectsToSpawn = Mathf.Min(spawnCount - currentCount, maxSpawnCount - totalSpawnedCount);
            SpawnObjects(objectsToSpawn);
        }
    }

    // ���ü�ʱ��ʱ���õķ���
    public void OnTimerReset()
    {
        needsImmediateCheck = true; // ���ñ�־λ���������м��
        hasCheckedAt20Seconds = false; // ���ñ�־λ
    }
}

