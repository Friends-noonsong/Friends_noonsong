using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    public GameObject greetingUI;
    public TextMeshProUGUI greetingText;
    public GameObject talkUI;
    public GameObject endTalkPopup;

    private Queue<string> dialogues = new Queue<string>();
    private string currentCharacterName;

    private Dictionary<string, List<string>> characterDialogues = new Dictionary<string, List<string>>
    {
        { "ĳ����1", new List<string> { "�ȳ��ϼ���!", "���õ� ���� �Ϸ� �Ǽ���!" } },
        { "ĳ����2", new List<string> { "�ݰ����ϴ�!", "������ ���͵帱���?" } },
        { "ĳ����3", new List<string> { "���� ���̿���!", "�Բ� ��ƿ�!" } }
    };

    public void SetCharacter(string characterName)
    {
        currentCharacterName = characterName;
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
        endTalkPopup.SetActive(false);
    }

    public void EnterTalkScreen()
    {
        greetingUI.SetActive(false);
        talkUI.SetActive(true);
        PrepareDialogues();
        ShowNextDialogue();
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
        Debug.Log("��ȭ ����");
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
            Debug.Log(nextDialogue);
        }
        else
        {
            Debug.Log("��ȭ�� �������ϴ�.");
        }
    }
}
