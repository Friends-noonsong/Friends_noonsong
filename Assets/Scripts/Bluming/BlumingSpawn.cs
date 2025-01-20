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
            Debug.LogError("GPS가 비활성화되어 있습니다!");
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
            Debug.LogError("GPS를 초기화하는 데 실패했습니다.");
            yield break;
        }

        Debug.Log("GPS가 성공적으로 초기화되었습니다.");
        locationServiceInitialized = true;
    }

    void Update()
    {
        if (!locationServiceInitialized) return;

        float currentLatitude = Input.location.lastData.latitude;
        float currentLongitude = Input.location.lastData.longitude;

        Debug.Log($"현재 위치: {currentLatitude}, {currentLongitude}");

        if (IsWithinProximity(currentLatitude, currentLongitude, targetLatitude, targetLongitude) && mapManager.AreAllRegionsUnlocked())
        {
            Debug.Log("목표 위치에 도달하고 모든 구역이 해금되었습니다.");
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
            confirmationUI.SetActive(true); // UI 활성화
        }
    }

    public void OnYesButtonPressed()
    {
        Debug.Log("사용자가 YES 버튼을 눌렀습니다. 씬을 전환합니다.");
        StartCoroutine(FadeOutAndLoadScene());
    }

    public void OnNoButtonPressed()
    {
        Debug.Log("사용자가 NO 버튼을 눌렀습니다. UI를 닫습니다.");
        if (confirmationUI != null)
        {
            confirmationUI.SetActive(false); // UI 비활성화
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

