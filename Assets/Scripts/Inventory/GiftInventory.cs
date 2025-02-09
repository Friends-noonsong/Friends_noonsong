using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public Transform inventorySlotContainer;
    public GameObject inventorySlotPrefab;

    public GameObject itemPopupUI; // ������ �˾� UI
    public TextMeshProUGUI itemNameText; // ������ �̸� �ؽ�Ʈ
    public Image itemImage; // ������ �̹���
    public TextMeshProUGUI itemDescriptionText; // ������ ���� �ؽ�Ʈ
    public Button popupGiftButton; // �˾��� �����ϱ� ��ư
    public Button popupCloseButton; // �˾��� �ݱ� ��ư

    private Inventory inventory;
    private Item selectedItem;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }

        popupGiftButton.onClick.AddListener(GiveGift);
        popupCloseButton.onClick.AddListener(CloseItemPopup);
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        UpdateInventoryUI();
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventorySlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.GetItems())
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotContainer);
            Slot slotScript = slot.GetComponent<Slot>();
            slotScript.Setup(item, inventory);

            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() => OpenItemPopup(item));
        }
    }

    public void OpenItemPopup(Item item)
    {
        selectedItem = item;
        itemNameText.text = item.itemName;
        itemImage.sprite = item.itemImage;
        itemDescriptionText.text = item.itemDescription;
        itemPopupUI.SetActive(true);
    }

    public void CloseItemPopup()
    {
        itemPopupUI.SetActive(false);
        selectedItem = null;
    }

    public void GiveGift()
    {
        if (selectedItem != null)
        {
            Debug.Log($"{selectedItem.itemName}��(��) �����߽��ϴ�.");
            selectedItem = null; // ���� �ʱ�ȭ
        }
        else
        {
            Debug.Log("������ �������� �������� �ʾҽ��ϴ�.");
        }
        itemPopupUI.SetActive(false);
        CloseInventory();
    }
}
