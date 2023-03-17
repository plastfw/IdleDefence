using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
  [SerializeField] private Button _shopButton;
  [SerializeField] private int _price;
  [SerializeField] private TMP_Text _priceText;

  public event Action<int> IsClicked;

  private void OnEnable()
  {
    _priceText.text = Convert.ToString(_price);
    _shopButton.onClick.AddListener(ClickCall);
  }

  private void OnDisable() => _shopButton.onClick.RemoveListener(ClickCall);

  private void ClickCall() => IsClicked?.Invoke(_price);
}