using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Location;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel; // �� �г�
    public Button mapButton; // �� ��ư
    public Button backButton; // �ڷ� ��ư
    public AbstractMap map; // Mapbox ��

    void Start()
    {
        // �ʱ� ���¿��� �� �г� ��Ȱ��ȭ
        mapPanel.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ������ �߰�
        mapButton.onClick.AddListener(ShowMap);
        backButton.onClick.AddListener(HideMap);

        // �� �ʱ�ȭ
        InitializeMap();
    }

    void ShowMap()
    {
        // �� �г� Ȱ��ȭ
        mapPanel.SetActive(true);
        // �� ������Ʈ
        UpdateMapLocation();
    }

    void HideMap()
    {
        // �� �г� ��Ȱ��ȭ
        mapPanel.SetActive(false);
    }

    void InitializeMap()
    {
        // ���� ��ġ�� ������� �� �ʱ�ȭ
        var locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        locationProvider.OnLocationUpdated += OnLocationUpdated;
    }

    void OnLocationUpdated(Location location)
    {
        // ���� ��ġ ������� �� ������Ʈ
        var mapPosition = new Vector2d(location.LatitudeLongitude.x, location.LatitudeLongitude.y);
        map.UpdateMap(mapPosition, map.Zoom);
    }

    void UpdateMapLocation()
    {
        // ���� ��ġ ������� �� ������Ʈ
        var locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        var currentLocation = locationProvider.CurrentLocation.LatitudeLongitude;
        var mapPosition = new Vector2d(currentLocation.x, currentLocation.y);
        map.UpdateMap(mapPosition, map.Zoom);
    }
}