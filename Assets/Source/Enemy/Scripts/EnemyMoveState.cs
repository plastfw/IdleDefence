using UnityEngine;

public class EnemyMoveState : IEnemyState
{
  private Base _playerBase;
  private Transform _transform;
  private float _speed;
  private Rigidbody _rigidbody;

  public EnemyMoveState(Base playerBase, Transform transform, float speed, Rigidbody rigidbody)
  {
    _playerBase = playerBase;
    _transform = transform;
    _speed = speed;
    _rigidbody = rigidbody;
  }

  public void Enter() => Move();

  public void Exit()
  {
  }

  private void Move()
  {
    var direction = _playerBase.transform.position - _transform.position;

    _rigidbody.velocity = direction.normalized * _speed;
  }
}