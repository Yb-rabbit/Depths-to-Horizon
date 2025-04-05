using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReBuild_Obj : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    public float resetY = -20f; // Ĭ���������Yֵ
    public UnityEvent<GameObject> OnObjectReset; // ���������¼�

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
            ResetObject(); // λ�õ���resetYʱ��������
        }
    }

    void ResetObject()
    {
        string originalName = gameObject.name; // ����ԭʼ���������
        GameObject newObject = Instantiate(gameObject, initialPosition, initialRotation);
        newObject.transform.localScale = initialScale;
        newObject.name = originalName; // �ָ�ԭʼ���������
        OnObjectReset?.Invoke(newObject); // �������������¼�
        gameObject.SetActive(false); // ����ԭ����
    }
}
