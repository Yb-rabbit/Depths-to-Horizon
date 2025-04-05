using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReBuild_Obj : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    public float resetY = -20f; // 默认最低重置Y值
    public UnityEvent<GameObject> OnObjectReset; // 物体重置事件

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (transform.position.y < resetY)
        {
            ResetObject(); // 位置低于resetY时重置物体
        }
    }

    void ResetObject()
    {
        string originalName = gameObject.name; // 保存原始物体的名称
        GameObject newObject = Instantiate(gameObject, initialPosition, initialRotation);
        newObject.transform.localScale = initialScale;
        newObject.name = originalName; // 恢复原始物体的名称
        OnObjectReset?.Invoke(newObject); // 触发物体重置事件
        gameObject.SetActive(false); // 禁用原物体
    }
}
