using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class GPSManager : MonoBehaviour
{
    public GameObject arObject; // ǥ���� AR ������Ʈ
    public float targetLatitude = 37.7749f; // ��ǥ ����
    public float targetLongitude = -122.4194f; // ��ǥ �浵
    public float threshold = 0.0001f; // �Ÿ� ��� ����

    private ARSession arSession;

    void Start()
    {
        arSession = FindObjectOfType<ARSession>();
        arObject.SetActive(false); // ó���� ������Ʈ�� ��Ȱ��ȭ
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS is not enabled by the user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            yield break;
        }

        while (true)
        {
            CheckLocation();
            yield return new WaitForSeconds(5); // 5�ʸ��� ��ġ Ȯ��
        }
    }

    void CheckLocation()
    {
        float currentLatitude = Input.location.lastData.latitude;
        float currentLongitude = Input.location.lastData.longitude;

        if (IsWithinThreshold(currentLatitude, currentLongitude, targetLatitude, targetLongitude, threshold))
        {
            arObject.SetActive(true); // ��ǥ ��ġ�� �����ϸ� ������Ʈ Ȱ��ȭ
        }
        else
        {
            arObject.SetActive(false); // ��ǥ ��ġ���� ����� ������Ʈ ��Ȱ��ȭ
        }
    }

    bool IsWithinThreshold(float currentLat, float currentLon, float targetLat, float targetLon, float threshold)
    {
        float latDiff = Mathf.Abs(currentLat - targetLat);
        float lonDiff = Mathf.Abs(currentLon - targetLon);
        return latDiff < threshold && lonDiff < threshold;
    }
}