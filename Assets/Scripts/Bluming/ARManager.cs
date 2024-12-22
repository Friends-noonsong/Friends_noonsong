using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    public Transform arCamera;
    public Transform mapCamera;
    public float mapScale = 3.0f;

    private Vector3 initialPositionOffset;
    private Quaternion initialRotationOffset;

    void Start()
    {
        initialPositionOffset = mapCamera.position - arCamera.position;
        initialRotationOffset = Quaternion.Inverse(arCamera.rotation) * mapCamera.rotation;
    }

    void Update()
    {
        SyncCameraWithAR();
    }

    private void SyncCameraWithAR()
    {
        mapCamera.position = arCamera.position + initialPositionOffset;
        Vector3 eulerRotation = arCamera.rotation.eulerAngles;
        mapCamera.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
    }
}