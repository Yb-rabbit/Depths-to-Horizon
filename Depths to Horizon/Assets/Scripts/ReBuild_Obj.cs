using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBuild_Obj : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    public float resetY = -10f; // 默认最低重置Y值

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
            ResetObject();//销毁后重置
        }
    }

    void ResetObject()
    {
        GameObject newObject = Instantiate(gameObject, initialPosition, initialRotation);
        newObject.transform.localScale = initialScale;
        Destroy(gameObject);
    }
}
