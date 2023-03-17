using System;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
  [SerializeField] private Shooter _shooter;
  [SerializeField] private Player _player;
  [SerializeField] private Slider _slider;
  [SerializeField] private TMP_Text _damage;
  [SerializeField] private TMP_Text _speed;
  [SerializeField] private TMP_Text _range;
  [SerializeField] private TMP_Text _cash;
  [SerializeField] private TMP_Text _sliderText;
  [Header("Buttons")] [SerializeField] private ShopButton _rangeShopButton;
  [SerializeField] private ShopButton _speedShopButton;
  [SerializeField] private ShopButton _damageShopButton;

  private void Start() => InitUI();

  public event Action<int> RangeButtonIsClicked;
  public event Action<int> SpeedButtonIsClicked;
  public event Action<int> DamageButtonIsClicked;

  private void OnEnable()
  {
    _player.HealthChanged += ChangeSliderValue;
    _player.CashChanged += ChangeCashValue;
    _player.RangeChanged += ChangeRangeValue;
    _player.DamageChanged += ChangeDamageValue;
    _player.SpeedChanged += ChangeSpeedValue;
    _rangeShopButton.IsClicked += RangeUpCall;
    _damageShopButton.IsClicked += DamageUpCall;
    _speedShopButton.IsClicked += SpeedUpCall;
  }

  private void OnDisable()
  {
    _player.HealthChanged -= ChangeSliderValue;
    _player.CashChanged -= ChangeCashValue;
    _player.RangeChanged -= ChangeRangeValue;
    _player.DamageChanged -= ChangeDamageValue;
    _player.SpeedChanged -= ChangeSpeedValue;
    _rangeShopButton.IsClicked -= RangeUpCall;
    _damageShopButton.IsClicked -= DamageUpCall;
    _speedShopButton.IsClicked -= SpeedUpCall;
  }

  private void ChangeCashValue(int value) => _cash.text = Convert.ToString(value);

  private void RangeUpCall(int price) => RangeButtonIsClicked?.Invoke(price);

  private void DamageUpCall(int price) => DamageButtonIsClicked?.Invoke(price);

  private void SpeedUpCall(int price) => SpeedButtonIsClicked?.Invoke(price);

  private void InitUI()
  {
    _slider.maxValue = _player.Health;
    _damage.text = Convert.ToString(_player.Damage);
    _speed.text = Convert.ToString(_player.Speed);
    _range.text = Convert.ToString(_player.Range);
    _cash.text = Convert.ToString(_player.Cash);
  }

  private void ChangeSliderValue(int value)
  {
    var newValue = _slider.value -= value;

    _slider
      .DOValue(newValue, .2f)
      .SetEase(Ease.Flash);

    _sliderText.text = Convert.ToString(newValue);
  }

  private void ChangeRangeValue(int value) => _range.text = Convert.ToString(value);

  private void ChangeDamageValue(int value) => _damage.text = Convert.ToString(value);

  private void ChangeSpeedValue(float value) => _speed.text = Convert.ToString(value);
}