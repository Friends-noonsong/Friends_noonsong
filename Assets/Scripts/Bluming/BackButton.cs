using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button moveSceneButton;
    public string sceneToLoad;

    void Start()
    {
        if (moveSceneButton != null)
        {
            moveSceneButton.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("��ư�� ������� �ʾҽ��ϴ�!");
        }
    }

    void OnButtonClicked()
    {
        Debug.Log("��ư Ŭ��! �� �̵�: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}