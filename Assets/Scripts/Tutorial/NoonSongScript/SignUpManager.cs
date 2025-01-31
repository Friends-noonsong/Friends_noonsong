using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class SignUpManager : MonoBehaviour
{
    public Toggle agreeToggle;
    public Button agreeButton;

    public InputField idInputField;
    public TMP_Text idResultText;
    public InputField passwordInputField;
    public TMP_Text passwordResultText;
    public InputField passwordConfirmInputField;
    public TMP_Text passwordConfirmResultText;
    public Button signUpButton;
    public GameObject privatePolicyPopup;
    public GameObject signUpPopup;
    public GameObject signUpCompletionPopup;

    private bool isIdValid = false;
    private bool isPasswordValid = false;
    private bool isPasswordsMatch = false;

    void Start()
    {
        SetupPrivacyAgreementToggle();
        SetupSignUpPage();
    }

    //������������ó�� �˾� ���� �ʱ�ȭ
    void SetupPrivacyAgreementToggle()
    {
        agreeButton.interactable = false;
        agreeToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    //���� üũ�ڽ� ���ý� Ȯ�ι�ư Ȱ��ȭ
    void OnToggleValueChanged(bool isOn)
    {
        agreeButton.interactable = isOn;
    }

    //ȸ������������ ���� �ʱ�ȭ. ��ư ��Ȱ��ȭ�ϰ� �Է� �ʵ忡 ������ ����
    void SetupSignUpPage()
    {
        signUpButton.interactable = false;
        idInputField.onValueChanged.AddListener(OnIDFieldEndEdit);
        passwordInputField.onValueChanged.AddListener(OnPasswordFieldEndEdit);
        passwordConfirmInputField.onValueChanged.AddListener(OnPasswordConfirmFieldEndEdit);
        signUpButton.onClick.AddListener(OnSignUpButtonClicked);
    }

    //ID�ʵ忡�� �Է� ������ �� ���̵� ��ȿ���� Ȯ���ϰ�, Ȯ�� ���� ������Ʈ
    void OnIDFieldEndEdit(string id)
    {
        isIdValid = ValidateId(id);
        UpdateIdResultText(id);
        CheckAllConditions();
    }

    //��ȿ ���̵� ����: ���� 6�� �̻� 20�� ����, ���� �ҹ��ڿ� ���ڸ�
    bool ValidateId(string id)
    {
        return Regex.IsMatch(id, @"^[a-z0-9]{6,20}$");
    }

    //���̵� ��ȿ �˻翡 ���� �ؽ�Ʈ ������Ʈ
    void UpdateIdResultText(string id)
    {
        if (!isIdValid)
        {
            idResultText.text = "���̵�� 6�� �̻� 20�� ���Ϸ� ���� �ҹ��ڿ� ���ڸ� ��� �����մϴ�.";
            idResultText.color = Color.red;
        }
        else
        {
            idResultText.text = "��� ������ ���̵��Դϴ�.";
            idResultText.color = Color.green;
        }
    }

    //PW1�ʵ忡�� �Է� ������ �� PW ��ȿ���� Ȯ���ϰ�, Ȯ�� ���� ������Ʈ
    void OnPasswordFieldEndEdit(string password)
    {
        isPasswordValid = ValidatePassword(password);
        UpdatePasswordResultText(password);
        CheckAllConditions();
    }

    //��ȿ PW ����: 6�� �̻�, ���� ����
    bool ValidatePassword(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*\d).{6,}$");
    }

    //PW ��ȿ �˻翡 ���� �ؽ�Ʈ ������Ʈ
    void UpdatePasswordResultText(string password)
    {
        if (!isPasswordValid)
        {
            passwordResultText.text = "��й�ȣ�� 6�� �̻�, ���ڸ� �����ؾ� �մϴ�.";
            passwordResultText.color = Color.red;
        }
        else
        {
            passwordResultText.text = "��� ������ ��й�ȣ�Դϴ�.";
            passwordResultText.color = Color.green;
        }
    }

    //PW2�ʵ忡�� �Է� ������ �� PW1�� ��ġ�ϴ��� ���ϰ� Ȯ�ι��� ������Ʈ
    void OnPasswordConfirmFieldEndEdit(string confirmPassword)
    {
        isPasswordsMatch = passwordInputField.text == confirmPassword;
        UpdateConfirmPasswordResultText();
        CheckAllConditions();
    }

    //PW2 ��ȿ �˻翡 ���� �ؽ�Ʈ ������Ʈ
    void UpdateConfirmPasswordResultText()
    {
        if (!isPasswordsMatch)
        {
            passwordConfirmResultText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            passwordConfirmResultText.color = Color.red;
        }
        else
        {
            passwordConfirmResultText.text = "��й�ȣ�� ��ġ�մϴ�.";
            passwordConfirmResultText.color = Color.green;
        }
    }

    //���̵�, PW1, PW2�� ��� ��ȿ���� Ȯ�� �� �������� ��ư Ȱ��ȭ
    void CheckAllConditions()
    {
        if (isIdValid && isPasswordValid && isPasswordsMatch)
        {
            signUpButton.interactable = true;
        }
        else
        {
            signUpButton.interactable = false;
        }
    }

    //ȸ������ ���� �� �˾� ���.
    public void OnSignUpButtonClicked()
    {
        string id = idInputField.text;
        string password = passwordInputField.text;

        var bro = Backend.BMember.CustomSignUp(id, password);

        if (bro.IsSuccess())
        {
            privatePolicyPopup.SetActive(false);
            signUpPopup.SetActive(false);
            signUpCompletionPopup.SetActive(true);
        }
        else
        {
            if (bro.GetErrorCode() == "DuplicatedParameterException")
            {
                idResultText.text = "�̹� �����ϴ� ���̵��Դϴ�.";
                idResultText.color = Color.red;
            }
            else
            {
                idResultText.text = "ȸ�����Կ� �����߽��ϴ�. �ٽ� �õ����ּ���.";
                idResultText.color = Color.red;
            }
        }
    }




}
