using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public GameObject greetingUI; // �λ縻 UI
    public Text greetingText; // �λ縻 �ؽ�Ʈ
    public Button talkButton; // ��ȭ�ϱ� ��ư
    public GameObject talkUI; // ��ȭ�ϱ� ȭ��
    public GameObject interactionButtonsPopup; // ��ȣ�ۿ� ��ư �˾�
    public Button giftButton; // �����ϱ� ��ư
    public Button greetButton; // �λ��ϱ� ��ư
    public Button endTalkButton; // ��ȭ ���� ��ư
    public GameObject inventoryUI; // �κ��丮 UI
    public Button selectCompleteButton; // ������ ���� �Ϸ� ��ư
    public Button closeInventoryButton; // �κ��丮 �ݱ� ��ư
    public GameObject endTalkPopup; // ��ȭ ���� Ȯ�� �˾�
    public Button confirmEndTalkButton; // ��ȭ ���� Ȯ�� ��ư
    public Button cancelEndTalkButton; // ��ȭ ���� ��� ��ư

    private Queue<string> dialogues = new Queue<string>(); // ��ȭ ������ ��� ť
    private Transform characterTransform; // ĳ������ Transform

    // ĳ���ͺ� ��ȭ ������
    private Dictionary<string, List<string>> characterDialogues = new Dictionary<string, List<string>>
    {
        { "ĳ����1", new List<string> { "�ȳ��ϼ���!", "���õ� ���� �Ϸ� �Ǽ���!" } },
        { "ĳ����2", new List<string> { "�ݰ����ϴ�!", "������ ���͵帱���?" } },
        { "ĳ����3", new List<string> { "���� ���̿���!", "�Բ� ��ƿ�!" } }
    };

    private string currentCharacterName;

    void Start()
    {
        talkButton.onClick.AddListener(EnterTalkScreen);
        endTalkButton.onClick.AddListener(ShowEndTalkPopup);
        confirmEndTalkButton.onClick.AddListener(EndConversation);
        cancelEndTalkButton.onClick.AddListener(CloseEndTalkPopup);
        giftButton.onClick.AddListener(OpenInventory);
        closeInventoryButton.onClick.AddListener(CloseInventory);
        selectCompleteButton.onClick.AddListener(GiveGift);

        InitializeGreeting();
    }

    public void SetCharacter(string characterName, Transform character)
    {
        currentCharacterName = characterName;
        characterTransform = character;
        InitializeGreeting();
    }

    private void InitializeGreeting()
    {
        greetingUI.SetActive(true);
        if (characterDialogues.ContainsKey(currentCharacterName))
        {
            greetingText.text = characterDialogues[currentCharacterName][0];
        }
        else
        {
            greetingText.text = "�ȳ��ϼ���!";
        }
        talkUI.SetActive(false);
        interactionButtonsPopup.SetActive(false);
        inventoryUI.SetActive(false);
        endTalkPopup.SetActive(false);
    }

    public void EnterTalkScreen()
    {
        greetingUI.SetActive(false);
        talkUI.SetActive(true);
        CenterCharacter();
        PrepareDialogues();
        ShowNextDialogue();
    }

    private void CenterCharacter()
    {
        if (characterTransform != null)
        {
            characterTransform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.0f; // ��: ȭ�� ���� 2����
        }
    }

    private void PrepareDialogues()
    {
        dialogues.Clear();
        if (characterDialogues.ContainsKey(currentCharacterName))
        {
            foreach (string dialogue in characterDialogues[currentCharacterName])
            {
                dialogues.Enqueue(dialogue);
            }
        }
    }

    private void ShowNextDialogue()
    {
        if (dialogues.Count > 0)
        {
            string nextDialogue = dialogues.Dequeue();
            // ��ȭâ UI�� �ؽ�Ʈ ǥ��
            Debug.Log(nextDialogue);
        }
        else
        {
            Debug.Log("��ȭ�� �������ϴ�.");
        }
    }

    public void ShowEndTalkPopup()
    {
        endTalkPopup.SetActive(true);
    }

    public void CloseEndTalkPopup()
    {
        endTalkPopup.SetActive(false);
    }

    public void EndConversation()
    {
        endTalkPopup.SetActive(false);
        greetingUI.SetActive(false);
        talkUI.SetActive(false);
        interactionButtonsPopup.SetActive(false);
        inventoryUI.SetActive(false);
        Debug.Log("��ȭ ����");
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void GiveGift()
    {
        Debug.Log("�������� �����߽��ϴ�.");
        inventoryUI.SetActive(false);
        talkUI.SetActive(true);
    }
}