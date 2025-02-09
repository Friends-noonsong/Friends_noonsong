using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftInventory : MonoBehaviour
{
    public GameObject giftInventoryUI;
    public GameObject giftSlotPrefab;
    public Transform giftSlotContainer;

    private List<Item> giftItems = new List<Item>();
    private EncounterUI encounterUI;
    private Inventory playerInventory;

    private Item selectedGiftItem;
    public GameObject giftPopup;

    public void Initialize(Inventory inventory, EncounterUI ui)
    {
        playerInventory = inventory;
        encounterUI = ui;
        SyncWithPlayerInventory();
    }

    public void ToggleGiftInventory()
    {
        giftInventoryUI.SetActive(!giftInventoryUI.activeSelf);
        giftPopup.SetActive(false);
    }

    public void ShowGiftPopup(Item item)
    {
        selectedGiftItem = item;
        encounterUI.ShowGiftPopup(item);
    }

    public void GiveGift()
    {
        if (selectedGiftItem != null)
        {
            giftItems.Remove(selectedGiftItem);
            playerInventory.RemoveItem(selectedGiftItem);
            encounterUI.GiveGift(selectedGiftItem);

            SyncWithPlayerInventory();
            UpdateGiftInventoryUI();
        }
    }

    public void SyncWithPlayerInventory()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory�� null�Դϴ�! �ʱ�ȭ Ȯ�� �ʿ�");
            return;
        }

        giftItems = new List<Item>(playerInventory.GetItems());
        UpdateGiftInventoryUI();

        Debug.Log($" ���� �κ��丮 ����ȭ �Ϸ�. ���� ������ ����: {giftItems.Count}");
    }

    private void UpdateGiftInventoryUI()
    {
        foreach (Transform child in giftSlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in giftItems)
        {
            GameObject slot = Instantiate(giftSlotPrefab, giftSlotContainer);
            GiftSlot slotScript = slot.GetComponent<GiftSlot>();
            slotScript.Setup(item, this);
        }
    }
}
