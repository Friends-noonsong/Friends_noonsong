using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventStaffController : MonoBehaviour
{
    private int buttonClickCount = 0;

    [SerializeField]
    private Button targetButton;


    private const int requiredClicks = 5; 

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnButtonClick); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �߰����� ������ �ʿ��ϸ� ���⿡ �ۼ�
    }

    private void OnButtonClick()
    {
        buttonClickCount++; 

        if (buttonClickCount >= requiredClicks)
        {
            //��ȭ 999�� �ʱ�ȭ
            Button[] currencyButtons = CurrencyManager.Instance.GetCurrencyButtons();
            CurrencyManager.Instance.SetAllCurrenciesTo999(currencyButtons);

            //���� ��ü �ر�
            NoonsongEntryManager entryManager = FindObjectOfType<NoonsongEntryManager>();  
            if (entryManager != null)
            {
                entryManager.SetAllEntriesDiscovered(); 
            }
            else
            {
                Debug.LogError("NoonsongEntryManager not found in the scene.");
            }

            // Ŭ�� Ƚ�� �ʱ�ȭ
            buttonClickCount = 0;  
        }
    }
}

