using UnityEngine;

public class Tracker : MonoBehaviour
{
    public Transform target; // 要追踪的目标物体
    public float speed = 5.0f; // 追踪速度
    public float minY = -20.0f; // 最小Y值，低于此值时销毁对象

    void Update()
    {
        if (target != null)
        {
            // 计算追踪方向并移动
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // 检测自身位置，若低于指定Y值，则销毁
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
