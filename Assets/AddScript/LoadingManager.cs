using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingPanel; // �ε� ȭ�� �г�
    public float loadingDuration = 3f; // �ε� ȭ�� ǥ�� �ð�
    public GameObject studentCard; // �̹��� �л��� ������Ʈ

    void Start()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    IEnumerator ShowLoadingScreen()
    {
        // �ε� ȭ�� ǥ��
        loadingPanel.SetActive(true);
        studentCard.SetActive(false);

        // ������ �ð� ���� �ε� ȭ�� ����
        yield return new WaitForSeconds(loadingDuration);

        // �ε� ȭ�� ����� �л��� ����
        loadingPanel.SetActive(false);
        studentCard.SetActive(true);

        // �л��� ���� �ִϸ��̼� ����
        StartCoroutine(ShowStudentCard());
    }

    IEnumerator ShowStudentCard()
    {
        // �л��� ���� �ִϸ��̼� ���� (��: ��ġ �̵�)
        Vector3 startPosition = new Vector3(0, -5, 0);
        Vector3 endPosition = Vector3.zero;
        float animationDuration = 2f;
        float elapsedTime = 0f;

        studentCard.transform.position = startPosition;

        while (elapsedTime < animationDuration)
        {
            studentCard.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        studentCard.transform.position = endPosition;
    }
}