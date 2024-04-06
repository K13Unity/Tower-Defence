using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerbtn : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TowerType _towerType;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private UpdateBtnConfig _updateBtnConfig;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private CoinManager _coinManager;

    public event Action<TowerType> onButtonDown;

    private int _clickCount = 0;
    private int _maxUpgradeTower = 4;
    



    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClic);
        _level.text = "L." + (_clickCount + 1).ToString();
        _price.text = _updateBtnConfig.Prices[_clickCount] .ToString();

    }

    private void OnButtonClic()
    {
        if(_coinManager.currentCoins < _updateBtnConfig.Prices[_clickCount])
        {
            return;
        }
        if(_clickCount < _maxUpgradeTower)
        {
            _coinManager.currentCoins -= _updateBtnConfig.Prices[_clickCount];
            _clickCount++;
            _price.text = _updateBtnConfig.Prices[_clickCount].ToString();
            _level.text = "L." + (_clickCount + 1).ToString();
            onButtonDown?.Invoke(_towerType);
            if (_clickCount == _maxUpgradeTower)
            {
                _price.text = "Max.".ToString();
                _level.text = "L.5".ToString();
                return;
            }
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClic);
    }
}
