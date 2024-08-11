using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int noonsongCurrency;

    [SerializeField] private TextMeshProUGUI currencyText; // TextMeshPro UGUI ������Ʈ

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
        if (currencyText != null)
        {
            currencyText.text = noonsongCurrency.ToString();
        }
    }
}


