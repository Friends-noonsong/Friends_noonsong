using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    public GameObject alarmPanel; // �˶� �г�
    public Button alarmButton; // �˶� ��ư
    public Button closeButton; // �ݱ� ��ư
    public Text alarmText; // �˶� �ؽ�Ʈ

    void Start()
    {
        // �ʱ� ���¿��� �˶� �г� ��Ȱ��ȭ
        alarmPanel.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ������ �߰�
        alarmButton.onClick.AddListener(ShowAlarm);
        closeButton.onClick.AddListener(CloseAlarm);
    }

    void ShowAlarm()
    {
        // �˶� �޽��� ���� (���ϴ� �޽����� ���� ����)
        alarmText.text = "This is an alarm message!";

        // �˶� �г� Ȱ��ȭ
        alarmPanel.SetActive(true);
    }

    void CloseAlarm()
    {
        // �˶� �г� ��Ȱ��ȭ
        alarmPanel.SetActive(false);
    }
}