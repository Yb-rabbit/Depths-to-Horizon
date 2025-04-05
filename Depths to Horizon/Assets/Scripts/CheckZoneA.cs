using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZoneA : MonoBehaviour
{
    public GameObject targetObject; // ָ����Ŀ������
    public GameObject objectToActivate; // ��Ҫ���������

    void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // ȷ����ʼʱĿ��������δ����״̬
        }

        if (targetObject != null)
        {
            if (targetObject.TryGetComponent<ReBuild_Obj>(out ReBuild_Obj rebuildObj))
            {
                rebuildObj.OnObjectReset.AddListener(OnTargetObjectReset);
            }
        }
    }

    void OnTargetObjectReset(GameObject newTargetObject)
    {
        targetObject = newTargetObject;
    }

    // ����һ����ײ����봥����ʱ���ô˷���
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // ����Ŀ������
            }
        }
    }
}
