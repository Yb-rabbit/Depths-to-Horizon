using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCame : MonoBehaviour
{
    public Transform skyboxCamera; // ���������
    public float rotationSpeed = 10.0f; // ��ת�ٶ�
    public Material[] skyboxMaterials; // ����պв������鸳ֵ

    void Update()
    {
        skyboxCamera.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) // �������������
        {
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            RenderSettings.skybox = skyboxMaterials[randomIndex];
        }
    }
}
