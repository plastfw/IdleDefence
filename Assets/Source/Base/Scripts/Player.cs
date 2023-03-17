using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  private const int Zero = 0;

  [SerializeField] private int _maxHealth;
  [SerializeField] private Base _base;
  [SerializeField] private Shooter _shooter;
  [SerializeField] private StatsUI _ui;
  [SerializeField] private EnemyGroup _enemyGroup;

  private int _cash = 0;
  private int _currentHealth;

  public event Action<int> HealthChanged;
  public event Action<int> CashChanged;
  public event Action<int> DamageChanged;
  public event Action<float> SpeedChanged;
  public event Action<int> RangeChanged;

  public int Health => _maxHealth;
  public int Damage => _shooter.Damage;
  public int Range => _shooter.Range;
  public float Speed => _shooter.Speed;
  public int Cash => _cash;

  private void Awake() => _currentHealth = _maxHealth;

  private void OnEnable()
  {
    _enemyGroup.EnemyIsDie += GetCash;
    _ui.DamageButtonIsClicked += UpDamageLevel;
    _ui.RangeButtonIsClicked += UpRangeLevel;
    _ui.SpeedButtonIsClicked += UpSpeedLevel;
    _base.IsDamaged += GetDamage;
  }

  private void OnDisable()
  {
    _enemyGroup.EnemyIsDie -= GetCash;
    _ui.DamageButtonIsClicked -= UpDamageLevel;
    _ui.RangeButtonIsClicked -= UpRangeLevel;
    _ui.SpeedButtonIsClicked -= UpSpeedLevel;
    _base.IsDamaged -= GetDamage;
  }

  public void GetDamage(int damage)
  {
    _currentHealth -= damage;
    HealthChanged?.Invoke(damage);
    if (_currentHealth <= Zero)
      Die();
  }

  private void GetCash()
  {
    _cash += 3;
    CashChanged?.Invoke(_cash);
  }

  private void Die()
  {
  }

  private void UpDamageLevel(int price)
  {
    if (_cash >= price && _shooter.DamageIndex != _shooter._levelsData.Damages.Count - 1)
    {
      _cash -= price;
      _shooter.UpDamage();
      DamageChanged?.Invoke(_shooter._levelsData.Damages[_shooter.DamageIndex]);
      CashChanged?.Invoke(_cash);
    }
  }

  private void UpRangeLevel(int price)
  {
    if (_cash >= price && _shooter.RangeIndex != _shooter._levelsData.RangeModel.Count - 1)
    {
      _cash -= price;
      _shooter.UpRange();
      RangeChanged?.Invoke(_shooter.RangeIndex + 1);
      CashChanged?.Invoke(_cash);
    }
  }

  private void UpSpeedLevel(int price)
  {
    if (_cash >= price && _shooter.SpeedIndex != _shooter._levelsData.Speeds.Count - 1)
    {
      _cash -= price;
      _shooter.UpSpeed();
      SpeedChanged?.Invoke(_shooter._levelsData.Speeds[_shooter.SpeedIndex]);
      CashChanged?.Invoke(_cash);
    }
  }
}