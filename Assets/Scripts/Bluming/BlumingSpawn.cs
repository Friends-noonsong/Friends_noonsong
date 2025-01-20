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
    private bool iconCreated = false;

    [SerializeField]
    private MapManager mapManager;

    [SerializeField]
    private GameObject confirmationUI;

    [SerializeField]
    private GameObject interactionIconPrefab;

    [SerializeField]
    private Transform iconParent;

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeDuration = 2f;

    private GameObject interactionIcon;

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
            Debug.Log("��ǥ ��ġ�� �����ϰ� ��� ������ �رݵǾ����ϴ�.");
            CreateInteractionIcon();
        }
    }

    bool IsWithinProximity(float currentLat, float currentLon, float targetLat, float targetLon)
    {
        float latDifference = Mathf.Abs(currentLat - targetLat);
        float lonDifference = Mathf.Abs(currentLon - targetLon);
        return latDifference <= proximityRange && lonDifference <= proximityRange;
    }

    void CreateInteractionIcon()
    {
        if (interactionIconPrefab != null && iconParent != null)
        {
            interactionIcon = Instantiate(interactionIconPrefab, iconParent);
            interactionIcon.SetActive(true);
            interactionIcon.GetComponent<Button>().onClick.AddListener(ShowConfirmationUI);
            iconCreated = true;
        }
    }

    public void ShowConfirmationUI()
    {
        if (confirmationUI != null)
        {
            confirmationUI.SetActive(true); // UI Ȱ��ȭ
        }
    }

    public void OnYesButtonPressed()
    {
        Debug.Log("����ڰ� YES ��ư�� �������ϴ�. ���� ��ȯ�մϴ�.");
        StartCoroutine(FadeOutAndLoadScene());
    }

    public void OnNoButtonPressed()
    {
        Debug.Log("����ڰ� NO ��ư�� �������ϴ�. UI�� �ݽ��ϴ�.");
        if (confirmationUI != null)
        {
            confirmationUI.SetActive(false); // UI ��Ȱ��ȭ
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        fadeImage.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;

        SceneManager.LoadScene(nextScene);
    }
}

