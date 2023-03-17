using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private EnemyStateMachine _stateMachine;
  [SerializeField] private int _damage;
  [SerializeField] private float _speed;
  [SerializeField] private int _health;
  [SerializeField] private Rigidbody _rigidbody;
  [SerializeField] private ParticleSystem _particle;

  private int _potentiallyHealth;
  private int _maxHealth;
  private Base _playerBase;
  private Transform _pool;
  private SpeedType _type;

  public SpeedType SpeedType => _type;
  public float Speed => _speed;
  public int Damage => _damage;

  public int PotentiallyHealth => _potentiallyHealth;

  public event Action<Enemy> IsDead;

  public void Initialize(Base PlayerBase, Transform pool)
  {
    _playerBase = PlayerBase;
    _pool = pool;

    _maxHealth = _health;
    _potentiallyHealth = _health;
    _stateMachine.InitializeStateMachine(_playerBase, _damage, _pool, this, _speed, _rigidbody, _particle);
  }

  public void GetDamage(int damage)
  {
    _health -= damage;
    if (_health <= 0)
    {
      _stateMachine.SetState<EnemyDeadState>();
      IsDead?.Invoke(this);
    }
  }

  public void GetPotentialDamage(int damage) => _potentiallyHealth -= damage;

  public void Revive()
  {
    _particle.transform.parent = transform;
    _particle.transform.position = transform.position;
    _potentiallyHealth = _maxHealth;
    _health = _maxHealth;
    gameObject.SetActive(true);
    transform.SetParent(null);
    _stateMachine.SetDefaultState();
  }

  public void Deactivate() => _stateMachine.SetState<EnemyDeadState>();
}