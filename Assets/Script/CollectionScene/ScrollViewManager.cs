using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    public GameObject[] scrollViews;  // 3���� ��ũ�Ѻ並 ���� �迭
    public Button[] buttons;          // 3���� ��ư�� ���� �迭
    public Sprite[] buttonSprites;    // �� ��ư�� �Ҵ��� �̹��� (��Ȱ��/Ȱ��)

    void Start()
    {
        // �ʱ� ����: ù ��° ��ũ�Ѻ丸 Ȱ��ȭ
        for (int i = 0; i < scrollViews.Length; i++)
        {
            scrollViews[i].SetActive(i == 0);
        }

        // ��ư Ŭ�� �̺�Ʈ ������ �߰�
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            Debug.Log($"Adding listener to button {index}");
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    void OnButtonClicked(int index)
    {
        Debug.Log($"Button {index} clicked");

        for (int i = 0; i < scrollViews.Length; i++)
        {
            scrollViews[i].SetActive(i == index);
        }

        // ��ư �̹��� ����
        for (int i = 0; i < buttons.Length; i++)
        {
            Image buttonImage = buttons[i].GetComponent<Image>();
            if (buttonImage != null)
            {
                if (i * 2 < buttonSprites.Length)
                {
                    buttonImage.sprite = buttonSprites[i * 2]; // ��Ȱ��ȭ �̹���
                    Debug.Log($"Button {i} set to inactive sprite.");
                }
                else
                {
                    Debug.LogError($"No inactive sprite for button {i}.");
                }
            }
            else
            {
                Debug.LogError($"No Image component found on button {i}.");
            }
        }

        Image activeButtonImage = buttons[index].GetComponent<Image>();
        if (activeButtonImage != null)
        {
            if (index * 2 + 1 < buttonSprites.Length)
            {
                activeButtonImage.sprite = buttonSprites[index * 2 + 1]; // Ȱ��ȭ �̹���
                Debug.Log($"Button {index} set to active sprite.");
            }
            else
            {
                Debug.LogError($"No active sprite for button {index}.");
            }
        }
        else
        {
            Debug.LogError($"No Image component found on button {index}.");
        }
    }
}
