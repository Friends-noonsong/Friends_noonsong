using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FriendsManager : MonoBehaviour
{
    public GameObject discoveredEntryPrefab;    // �߰ߵ� �׸� ������
    public GameObject undiscoveredEntryPrefab;  // �߰ߵ��� ���� �׸� ������
    public Transform entryParent;               // ���� �׸��� �θ� ������Ʈ
    public GameObject detailsPanel;             // ��Ʈ�� �� ������ ǥ���� �г�
    public TextMeshProUGUI detailsNameText;     // �� ������ �̸� �ؽ�Ʈ
    public TextMeshProUGUI detailsDescriptionText;
    public Image detailsImage;
    public Camera renderCamera;                // 3D �������� �������� ī�޶�
    public RenderTexture renderTexture;        // 3D �������� �������� Render Texture

    [SerializeField]
    private NoonsongFriendsEntryManager noonsongFriendsEntryManager; // NoonsongFriendsEntryManager ����

    private List<NoonsongFriendsEntry> entries; // ��� ���� �׸� ����Ʈ
    private GameObject lastClickedEntry = null; // ������ Ŭ���� �׸�

    void Start()
    {
        // entries ����Ʈ�� NoonsongFriendsEntryManager���� ������
        if (noonsongFriendsEntryManager != null)
        {
            entries = new List<NoonsongFriendsEntry>(noonsongFriendsEntryManager.GetNoonsongEntries());
        }
        else
        {
            Debug.LogError("NoonsongFriendsEntryManager is not assigned in the inspector!");
        }

        PopulateNoonsong();
    }

    public void PopulateNoonsong()
    {
        // ������ ��� �׸� ����
        foreach (Transform child in entryParent)
        {
            Destroy(child.gameObject);
        }

        // ���ο� �׸� ���� �� UI ������Ʈ
        foreach (var entry in entries)
        {
            GameObject newEntry;
            if (entry.isDiscovered)
            {
                newEntry = Instantiate(discoveredEntryPrefab, entryParent);

                var eventTrigger = newEntry.GetComponent<EventTrigger>();
                if (eventTrigger == null)
                {
                    eventTrigger = newEntry.AddComponent<EventTrigger>();
                }
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerClick, () => OnEntryClick(entry, newEntry));

                var noonsongFriendImage = newEntry.transform.Find("NoonsongFriendImage").GetComponent<Image>();
                if (noonsongFriendImage != null) noonsongFriendImage.sprite = entry.noonsongSprite;
            }
            else
            {
                newEntry = Instantiate(undiscoveredEntryPrefab, entryParent);
            }
        }
    }

    void AddEventTriggerListener(EventTrigger trigger, EventTriggerType eventType, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }

    void OnEntryClick(NoonsongFriendsEntry entry, GameObject newEntry)
    {
        Debug.Log($"OnEntryClick called for entry: {entry.noonsongFriendName}");

        // ������ Ŭ���� �׸��� ������ ���� �̹����� �����Ϸ� �ǵ���
        if (lastClickedEntry != null && lastClickedEntry != newEntry)
        {
            var lastEntryImage = lastClickedEntry.transform.Find("NoonsongFriendImage").GetComponent<Image>();
            if (lastEntryImage != null)
            {
                var lastEntryScript = entries.Find(e => e.noonsongSprite == lastEntryImage.sprite || e.clickedNoonsongSprite == lastEntryImage.sprite);
                if (lastEntryScript != null)
                {
                    lastEntryImage.sprite = lastEntryScript.noonsongSprite;
                    lastEntryImage.rectTransform.localScale = Vector3.one; // �������� ���� ũ��� �ǵ���
                }
            }
        }

        // ���� Ŭ���� �׸��� �̹����� �����ϰ� �������� Ű��
        var currentEntryImage = newEntry.transform.Find("NoonsongFriendImage").GetComponent<Image>();
        if (currentEntryImage != null)
        {
            currentEntryImage.sprite = entry.clickedNoonsongSprite;
            currentEntryImage.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f); // �������� 1.3��� Ű��
        }

        // �� ���� �г� ������Ʈ
        ShowDetails(entry);

        // ���� �׸��� ������ Ŭ�� �׸����� ����
        lastClickedEntry = newEntry;
    }

    public void ShowDetails(NoonsongFriendsEntry entry)
    {
        Debug.Log($"ShowDetails called for entry: {entry.noonsongFriendName}");

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(true);
            if (detailsNameText != null)
            {
                detailsNameText.text = entry.noonsongFriendName;
                detailsDescriptionText.text = entry.description;
                detailsImage.sprite = entry.displaySprite;

                // ������ ����
                detailsImage.rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                Debug.Log($"Details panel updated with entry: {entry.noonsongFriendName}");
            }
        }
        else
        {
            Debug.LogError("DetailsPanel is not set in the inspector!");
        }
    }

}
