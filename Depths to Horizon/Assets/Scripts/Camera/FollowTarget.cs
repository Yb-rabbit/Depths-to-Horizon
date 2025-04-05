using UnityEngine;
using System.Collections.Generic;

public class FollowTarget : MonoBehaviour
{
    public List<Transform> targets = new(); // Ŀ������Transform����б�
    public Vector3 offset = new Vector3(0, 5, -10); // �������Ŀ������ƫ��������ʼֵΪ(0, 5, -10)
    public float smoothSpeed = 0.125f; // ƽ��������ٶ�
    public float rotationSpeed = 5.0f; // ��ת�ٶ�
    public float minDistance = 2.0f; // ��С����
    public float maxDistance = 20.0f; // ������
    public float zoomSpeed = 2.0f; // �����ٶ�

    private Vector3 velocity = Vector3.zero; // ���ڲ�ֵ������ٶ�
    private float currentYAngle = 0f; // ��ǰY����ת�Ƕ�
    private float targetDistance; // Ŀ�����
    private int currentTargetIndex = 0; // ��ǰĿ����������

    void Start()
    {
        if (targets != null && targets.Count > 0)
        {
            SetTarget(targets[currentTargetIndex]);
        }
    }

    void LateUpdate()
    {
        if (targets == null || targets.Count == 0)
        {
            Debug.LogWarning("FollowTarget: No targets assigned.");
            return;
        }

        if (Input.GetMouseButtonDown(1)) // �Ҽ����
        {
            SwitchTarget();
        }

        HandleZoom();
        HandleRotation();
        HandlePosition();
    }

    private void SetTarget(Transform newTarget)
    {
        if (newTarget != null)
        {
            // ��ʼ�����������ת�Ƕȣ�ʹ����Ŀ�����ķ���һ��
            Vector3 lookPos = newTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;

            // ��ʼ����ǰY����ת�Ƕ�
            currentYAngle = transform.eulerAngles.y;

            // ��ʼ��Ŀ�����
            targetDistance = offset.magnitude;
        }
    }

    public void SwitchTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
        SetTarget(targets[currentTargetIndex]);
    }

    private void HandleZoom()
    {
        // ��ȡ����������
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance - scroll, minDistance, maxDistance);

        // ƽ������������ľ���
        float currentDistance = offset.magnitude;
        float newDistance = Mathf.Lerp(currentDistance, targetDistance, smoothSpeed);
        offset = offset.normalized * newDistance;
    }

    private void HandleRotation()
    {
        // ��ȡ�û�����
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;

        // ��ת�����
        currentYAngle += horizontal;

        // Ӧ����ת
        Quaternion rotation = Quaternion.Euler(0, currentYAngle, 0);
        transform.rotation = rotation;
    }

    private void HandlePosition()
    {
        // ƽ������Ŀ�����
        Vector3 targetPosition = targets[currentTargetIndex].position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        // ����Ŀ��������Ӷ���İ�Χ������
        Vector3 lookAtPosition = CalculateBoundsCenter(targets[currentTargetIndex]);

        // ʹ�����ʼ�ճ���Ŀ��������Ӷ���İ�Χ������
        transform.LookAt(lookAtPosition);
    }

    private Vector3 CalculateBoundsCenter(Transform target)
    {
        Bounds bounds = new Bounds(target.position, Vector3.zero);
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds.center;
    }
}
