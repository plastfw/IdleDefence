using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
  private Base _base;
  private int _damage;
  private Transform _pool;
  private Enemy _enemy;
  private Dictionary<Type, IEnemyState> _states;
  private IEnemyState _currentState;
  private float _speed;
  private Rigidbody _rigidbody;
  private ParticleSystem _particle;

  public void InitializeStateMachine(Base playerBase, int damage, Transform pool, Enemy enemy, float speed,
    Rigidbody rigidbody, ParticleSystem particle)
  {
    _base = playerBase;
    _damage = damage;
    _pool = pool;
    _enemy = enemy;
    _speed = speed;
    _rigidbody = rigidbody;
    _particle = particle;

    InitStates();
    SetDefaultState();
  }

  public void SetState<T>() where T : IEnemyState => SetState(GetState<T>());

  public void SetDefaultState() => SetState(GetState<EnemyMoveState>());

  private void InitStates()
  {
    _states = new Dictionary<Type, IEnemyState>
    {
      [typeof(EnemyMoveState)] = new EnemyMoveState(_base, _enemy.transform, _enemy.Speed, _rigidbody),
      [typeof(EnemyDeadState)] = new EnemyDeadState(_pool, _enemy, _particle),
      [typeof(EnemyInactiveState)] = new EnemyInactiveState(_enemy)
    };
  }

  private IEnemyState GetState<T>() where T : IEnemyState
  {
    var state = typeof(T);
    return _states[state];
  }

  private void SetState(IEnemyState state)
  {
    _currentState?.Exit();
    _currentState = state;
    _currentState.Enter();
  }
}