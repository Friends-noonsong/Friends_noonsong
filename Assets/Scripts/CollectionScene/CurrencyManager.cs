using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int noonsongCurrency;

    // ���� TextMeshProUGUI ������Ʈ�� �����ϱ� ���� ����Ʈ
    [SerializeField] private List<TextMeshProUGUI> currencyTexts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCurrencyUI(); // �ʱ�ȭ �� UI ������Ʈ
    }

    public bool HasEnoughCurrency(int amount)
    {
        return noonsongCurrency >= amount;
    }

    public void UseCurrency(int amount)
    {
        if (HasEnoughCurrency(amount))
        {
            noonsongCurrency -= amount;
            UpdateCurrencyUI(); // ��ȭ ��� �� UI ������Ʈ
        }
    }

    public void AddCurrency(int amount)
    {
        noonsongCurrency += amount;
        UpdateCurrencyUI(); // ��ȭ �߰� �� UI ������Ʈ
    }

    private void UpdateCurrencyUI()
    {
        // ��ϵ� ��� TextMeshProUGUI ������Ʈ�� ������Ʈ
        foreach (var currencyText in currencyTexts)
        {
            if (currencyText != null)
            {
                currencyText.text = noonsongCurrency.ToString();
            }
        }
    }

    // TextMeshProUGUI ������Ʈ�� ����ϴ� �޼���
    public void RegisterCurrencyText(TextMeshProUGUI newCurrencyText)
    {
        if (!currencyTexts.Contains(newCurrencyText))
        {
            currencyTexts.Add(newCurrencyText);
            newCurrencyText.text = noonsongCurrency.ToString(); // ��� �� �ʱ�ȭ
        }
    }
}


