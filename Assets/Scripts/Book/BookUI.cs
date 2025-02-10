using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookUI : MonoBehaviour
{
    [SerializeField] private GameObject bookPanel;

    [SerializeField] private Transform defaultSlotParent; // �⺻ ������ ���� �θ�
    [SerializeField] private Transform universitySlotParentTemplate; // ���к� ���� �θ� ���ø�
    [SerializeField] private GameObject slotPrefab; // ������ ���� ������

    [SerializeField] private GameObject defaultPage; // �⺻ ������
    [SerializeField] private GameObject universityListPage; // ���� ��� ������
    [SerializeField] private GameObject universityDetailPage; // ���к� ������ ������

    [SerializeField] private Image selectedNoonsongImage; // ���õ� ������ �̹���
    [SerializeField] private TextMeshProUGUI selectedNoonsongNameText; // ���õ� ������ �̸�
    [SerializeField] private TextMeshProUGUI selectedNoonsongDescriptionText; // ���õ� ������ ����
    [SerializeField] private Image[] loveMeterImages; // ȣ���� �̹��� �迭

    [SerializeField] private List<Button> universityButtons; // ���к� ��ư ����Ʈ

    private Dictionary<string, Transform> universitySlotParents; // ���к� ���� �θ� ����
    private Dictionary<string, List<NoonsongEntry>> universityNoonsongs;
    private List<BookSlot> activeSlots = new List<BookSlot>();

    private string currentUniversity;

    public static BookUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenBook(List<NoonsongEntry> discoveredNoonsongs)
    {
        UpdateBook(discoveredNoonsongs);
        bookPanel.SetActive(true);
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
    }

    public void Initialize(List<string> universities, List<NoonsongEntry> discoveredNoonsongs)
    {
        universitySlotParents = new Dictionary<string, Transform>();
        universityNoonsongs = new Dictionary<string, List<NoonsongEntry>>();

        // ���� UI�� ���� ��ư�� ����
        for (int i = 0; i < universities.Count && i < universityButtons.Count; i++)
        {
            string university = universities[i];
            universityButtons[i].onClick.RemoveAllListeners(); // ���� ������ ����
            universityButtons[i].onClick.AddListener(() => ShowUniversityPage(university));

            // ���к� ���� �θ� ����
            Transform slotParent = Instantiate(universitySlotParentTemplate, universitySlotParentTemplate.parent);
            slotParent.gameObject.name = university + " Slots";
            slotParent.gameObject.SetActive(false); // �⺻������ ��Ȱ��ȭ
            universitySlotParents.Add(university, slotParent);
        }
        universitySlotParentTemplate.gameObject.SetActive(false);

        // ������ �ִ� ������ �з�
        foreach (var noonsong in discoveredNoonsongs)
        {
            if (!string.IsNullOrEmpty(noonsong.university))
            {
                if (!universityNoonsongs.ContainsKey(noonsong.university))
                {
                    universityNoonsongs[noonsong.university] = new List<NoonsongEntry>();
                }
                universityNoonsongs[noonsong.university].Add(noonsong);
            }
        }
    }

    public void UpdateBook(List<NoonsongEntry> discoveredNoonsongs)
    {
        // ���� ���� ����
        foreach (var slot in activeSlots)
        {
            Destroy(slot.gameObject);
        }
        activeSlots.Clear();

        // �⺻ ������ ǥ��
        foreach (var noonsong in discoveredNoonsongs)
        {
            Transform targetParent = string.IsNullOrEmpty(noonsong.university)
                ? defaultSlotParent
                : universitySlotParents.TryGetValue(noonsong.university, out var slotParent)
                    ? slotParent
                    : null;

            if (targetParent == null)
            {
                Debug.LogWarning($"��ϵ��� ���� ����: {noonsong.university}");
                continue;
            }

            // ���� ���� �� �ʱ�ȭ
            var slot = Instantiate(slotPrefab, targetParent).GetComponent<BookSlot>();
            slot.Initialize(noonsong, this); // BookUI ����
            activeSlots.Add(slot);
        }
    }

    public void ShowDefaultPage()
    {
        defaultPage.SetActive(true);
        universityListPage.SetActive(false);
        universityDetailPage.SetActive(false);
    }

    public void ShowUniversityListPage()
    {
        defaultPage.SetActive(false);
        universityListPage.SetActive(true);
        universityDetailPage.SetActive(false);
    }

    public void ShowUniversityPage(string university)
    {
        if (!universityNoonsongs.ContainsKey(university))
        {
            Debug.LogWarning($"���� {university}�� ���� �����Ͱ� �����ϴ�.");
            return;
        }

        // ������ ��ȯ
        defaultPage.SetActive(false);
        universityListPage.SetActive(false);
        universityDetailPage.SetActive(true);

        // ���� ���� ������ Ȱ��ȭ
        foreach (var entry in universitySlotParents)
        {
            entry.Value.gameObject.SetActive(entry.Key == university);
        }

        currentUniversity = university;
    }

    public void DisplayNoonsongDetails(NoonsongEntry noonsong)
    {
        selectedNoonsongImage.sprite = noonsong.noonsongSprite;
        selectedNoonsongNameText.text = noonsong.noonsongName;
        selectedNoonsongDescriptionText.text = noonsong.description;

        // ȣ���� UI ������Ʈ
        for (int i = 0; i < loveMeterImages.Length; i++)
        {
            loveMeterImages[i].enabled = (i < noonsong.loveLevel);
        }
    }
}