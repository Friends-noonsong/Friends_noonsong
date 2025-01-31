using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlumingSpawn : MonoBehaviour
{
    public float targetLatitude;
    public float targetLongitude;
    public float proximityRange = 0.01f;
    public string nextScene;

    private bool locationServiceInitialized = false;

    [SerializeField]
    private MapManager mapManager;

    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS�� ��Ȱ��ȭ�Ǿ� �ֽ��ϴ�!");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("GPS�� �ʱ�ȭ�ϴ� �� �����߽��ϴ�.");
            yield break;
        }

        Debug.Log("GPS�� ���������� �ʱ�ȭ�Ǿ����ϴ�.");
        locationServiceInitialized = true;
    }

    void Update()
    {
        if (!locationServiceInitialized) return;

        float currentLatitude = Input.location.lastData.latitude;
        float currentLongitude = Input.location.lastData.longitude;

        Debug.Log($"���� ��ġ: {currentLatitude}, {currentLongitude}");

        if (IsWithinProximity(currentLatitude, currentLongitude, targetLatitude, targetLongitude) && mapManager.AreAllRegionsUnlocked())
        {
            Debug.Log("��ǥ ��ġ�� �����ϰ� ��� ������ �رݵǾ����ϴ�. ���� ��ȯ�մϴ�.");
            SceneManager.LoadScene(nextScene);
        }
    }

    bool IsWithinProximity(float currentLat, float currentLon, float targetLat, float targetLon)
    {
        float latDifference = Mathf.Abs(currentLat - targetLat);
        float lonDifference = Mathf.Abs(currentLon - targetLon);
        return latDifference <= proximityRange && lonDifference <= proximityRange;
    }
}
