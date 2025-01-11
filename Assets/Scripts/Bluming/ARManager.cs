using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    public Transform arCamera; // AR ī�޶� (�޴����� ī�޶�)
    public Transform mapCamera; // �� ������ ī�޶�

    private Vector3 initialPositionOffset; // AR ī�޶�� �� ī�޶� �� �ʱ� ��ġ ������
    private Quaternion initialRotationOffset; // �ʱ� ȸ�� ������

    void Start()
    {
        // �ʱ� ������ ���� (�ʿ信 ���� �� �����ϸ� �߰�)
        initialPositionOffset = mapCamera.position - arCamera.position;
        initialRotationOffset = Quaternion.Inverse(arCamera.rotation) * mapCamera.rotation;
    }

    void Update()
    {
        // AR ī�޶��� ��ġ�� ȸ���� �� ī�޶� ����
        SyncCameraWithAR();
    }

    private void SyncCameraWithAR()
    {
        float mapScale = 0.1f;

        // �� ī�޶� ��ġ ����ȭ
        mapCamera.position = arCamera.position + initialPositionOffset;

        // �� ī�޶� ȸ�� ����ȭ
        mapCamera.rotation = arCamera.rotation * initialRotationOffset;
    }
}