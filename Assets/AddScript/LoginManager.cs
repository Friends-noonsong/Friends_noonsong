using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    //�α��� ȭ�� Root 
    public GameObject LoginView;

    public InputField inputField_ID;
    public InputField inputField_PW;
    public Button Button_Login;

    //Test�� ���� ���Ƿ� ����� ������ �߰�����
    private string user = "User";
    private string password = "1234";

    /// <summary>
    /// �α��� ��ư Ŭ���� ����
    /// </summary>
    public void LoginButtonClick()
    {
        if (inputField_ID.text == user && inputField_PW.text == password)
        {
            Debug.Log("�α��� ����");
            //�α��� ������ �α��� â ����
            LoginView.SetActive(false);
        }
        else
        {
            Debug.Log("�α��� ����");
        }
    }
}