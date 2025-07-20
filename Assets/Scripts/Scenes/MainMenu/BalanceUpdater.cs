using TMPro;
using UnityEngine;

public class BalanceUpdater : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;

    [SerializeField] private TextMeshProUGUI _text;


    private void Awake()
    {
        UpdateBalance();
        _storeConfig._storeUpdateBalanceEvent += UpdateBalance;
    }

    private void UpdateBalance()
    {
        _text.text = _storeConfig.money.ToString();
    }
}
