using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private Button _buyBtn;
   


    public event Action<Button> onButtonDown;
  

   
    private void OnEnable()
    {
        _buyBtn.onClick.AddListener(OnButtonClic);
    }

    private void OnButtonClic()
    {
        onButtonDown?.Invoke(_buyBtn);
    }

    private void OnDisable()
    {
        _buyBtn.onClick.RemoveListener(OnButtonClic);
    }
}
