using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZoneA : MonoBehaviour
{
    public GameObject targetObject; // 指定的目标物体
    public GameObject objectToActivate; // 需要激活的物体

    void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // 确保开始时目标物体是未激活状态
        }
    }


    // 当另一个碰撞体进入触发器时调用此方法
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // 激活目标物体
            }
        }
    }
}
