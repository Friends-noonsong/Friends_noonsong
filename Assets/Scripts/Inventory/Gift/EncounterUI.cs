using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncounterUI : MonoBehaviour
{
    [SerializeField] private GameObject encounterPanel;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI greetingText;

    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private GameObject dialoguePopup;

    [SerializeField] private GiftInventory giftInventory;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject giftPopup;
    [SerializeField] private TextMeshProUGUI giftItemName;
    [SerializeField] private Image giftItemImage;
    [SerializeField] private TextMeshProUGUI giftItemDescription;

    private NoonsongEntry currentCharacter;
    private System.Action onCloseCallback;

    

    public void Show(NoonsongEntry character, System.Action onClose)
    {
        currentCharacter = character;
        onCloseCallback = onClose;

        characterImage.sprite = character.noonsongSprite;
        greetingText.text = $"{character.noonsongName}: �ȳ�! ���� {character.university} ����̾�!";

        encounterPanel.SetActive(true);
    }

    public void OpenDialogueWindow()
    {
        dialogueWindow.SetActive(true);
    }

    public void CloseDialogueWindow()
    {
        dialogueWindow.SetActive(false);
        dialoguePopup.SetActive(false);
    }

    public void ShowDialoguePopup()
    {
        dialoguePopup.SetActive(true);
    }

    public void CloseDialoguePopup()
    {
        dialoguePopup.SetActive(false);
    }

    public void OpenGiftInventory()
    {
        if (playerInventory == null)
        {
            Debug.LogError("�÷��̾� �κ��丮�� null�Դϴ�! �ʱ�ȭ ������ Ȯ���ϼ���.");
            return;
        }

        giftInventory.Initialize(playerInventory, this);
        giftInventory.SyncWithPlayerInventory();
        giftInventory.ToggleGiftInventory();
    }

    public void ShowGiftPopup(Item item)
    {
        giftItemName.text = item.itemName;
        giftItemImage.sprite = item.itemImage;
        giftItemDescription.text = item.itemDescription;
        giftPopup.SetActive(true);
    }

    public void CloseGiftPopup()
    {
        giftPopup.SetActive(false);
    }

    public void GiveGift(Item item)
    {
        Debug.Log($"{item.itemName}��(��) ������");
        giftInventory.SyncWithPlayerInventory();
        giftPopup.SetActive(false);
    }

    public void CloseEncounter()
    {
        gameObject.SetActive(false);
        onCloseCallback?.Invoke();
    }
}
