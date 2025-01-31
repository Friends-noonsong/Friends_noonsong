using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private NoonsongEntry noonsongEntry; // NoonsongEntry���� �������� �������� ���� ����

    [SerializeField]
    private Transform arCamera; // AR ī�޶��� Transform

    [SerializeField]
    private float distanceFromCamera = 2f; // ī�޶�κ����� �Ÿ�

    void Start()
    {
        if (noonsongEntry != null && noonsongEntry.prefab != null && arCamera != null)
        {
            SpawnPrefabInFrontOfCamera();
        }
        else
        {
            Debug.LogError("NoonsongEntry, prefab, or AR Camera is not assigned.");
        }
    }

    void SpawnPrefabInFrontOfCamera()
    {
        // ī�޶��� ���� ��ġ�� ���
        Vector3 spawnPosition = arCamera.position + arCamera.forward * distanceFromCamera;
        Quaternion spawnRotation = arCamera.rotation;

        // �������� �����ϰ� ��ġ�� ȸ�� ����
        Instantiate(noonsongEntry.prefab, spawnPosition, spawnRotation);
    }
}

