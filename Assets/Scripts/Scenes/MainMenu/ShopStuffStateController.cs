using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopStuffStateController : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;
    [SerializeField] private StoreStuff _storeStuff;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _costText;

    [SerializeField] private GameObject _isNotBuy;
    [SerializeField] private GameObject _isSelected;
    [SerializeField] private GameObject _isNotSelected;

    [SerializeField] private List<ShopStuffStateController> _controllers;


    private void Awake()
    {
        _costText.text = _storeStuff.price.ToString();
        _image.sprite = _storeStuff._sprite;
        _storeConfig._storeUpdateStatesEvent += UpdateState;   
    }

    private void OnEnable()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (_storeStuff != null)
        {
            if (_storeStuff.isSelected)
            {
                _isNotBuy.SetActive(false);
                _isSelected.SetActive(true);
                _isNotSelected.SetActive(false);
            }
            else
            {
                if (_storeStuff.isBuy)
                {
                    _isNotBuy.SetActive(false);
                    _isSelected.SetActive(false);
                    _isNotSelected.SetActive(true);
                }
                else
                {
                    _isNotBuy.SetActive(true);
                    _isSelected.SetActive(false);
                    _isNotSelected.SetActive(false);
                }
            }
        }
        SaveLoadConfigsService.Instance.SaveAll();
    }

    public void Buy() 
    {
        if (!_storeStuff.isBuy)
        {
            if (_storeConfig.money >= _storeStuff.price)
            {
                _storeConfig.money -= _storeStuff.price;
                _storeConfig.PerformUpdateBalance();
                _storeStuff.isBuy = true;
                _storeConfig.PerformUpdateStates();
                UpdateState();

                foreach (var controller in _controllers)
                    controller.UpdateState();
            }
        }
        SaveLoadConfigsService.Instance.SaveAll();
    }

    public void Select()
    {
        if (!_storeStuff.isSelected)
        {
            _storeStuff.isSelected = true;
            foreach (var sib in _storeStuff.siblings)
                sib.isSelected = false;
            UpdateState();            
        }
        _storeConfig.PerformUpdateStates();
        foreach (var controller in _controllers)
            controller.UpdateState();
        SaveLoadConfigsService.Instance.SaveAll();
    }
}
