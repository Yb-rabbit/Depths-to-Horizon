using UnityEngine;

public class Tracker : MonoBehaviour
{
    public Transform target; // Ҫ׷�ٵ�Ŀ������
    public float speed = 5.0f; // ׷���ٶ�
    public float minY = -20.0f; // ��СYֵ�����ڴ�ֵʱ���ٶ���

    void Update()
    {
        if (target != null)
        {
            // ����׷�ٷ����ƶ�
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // �������λ�ã�������ָ��Yֵ��������
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
