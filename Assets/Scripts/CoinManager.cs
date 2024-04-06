using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private BuildingsSpawner _buildingsSpawner;
    [SerializeField] private BuyButton _controllerButtonUI;
    [SerializeField] private Text _textCurrentCoins;
    [SerializeField] private Text _textBuy;
    [SerializeField] private Enemy _enemyCurrent;
    [SerializeField] private GroundSpawner _groundSpawner;

    public int currentCoins = 100;
    private int _priceForNextTower = 10;


    private void Update()
    {
        _textCurrentCoins.text = currentCoins.ToString();
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            AddCoins(1000);
        }

    }
    public void AddCoins(int coins)
    {
        currentCoins += coins;
    }

    public void SetPriceForNextTower(int price)
    {
        _textBuy.text = price.ToString();
    }

   
    public void BuyTower()
    {
        var tile = _groundSpawner.GetFreeTile();
        if (currentCoins >= _priceForNextTower && tile != null)
        {
            currentCoins -= _priceForNextTower;
            _buildingsSpawner.BildBuilding();
            _priceForNextTower += 10;
        }
        else
        {
            return;
        }
       
        SetPriceForNextTower(_priceForNextTower);
    }

    private void OnEnable()
    {
        _controllerButtonUI.onButtonDown += OnButtonDown;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        AddCoins(enemy._coins);
    }

    private void OnButtonDown(Button button)
    {
        BuyTower();
    }

    private void OnDisable()
    {
        _enemyCurrent.OnEnemyDie -= OnEnemyDeath;
        _controllerButtonUI.onButtonDown -= OnButtonDown;
    }
}
