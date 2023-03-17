using UnityEngine;

public class EnemyDeadState : IEnemyState
{
  private Transform _pool;
  private Enemy _enemy;
  private ParticleSystem _particle;

  public EnemyDeadState(Transform pool, Enemy enemy, ParticleSystem particle)
  {
    _particle = particle;
    _pool = pool;
    _enemy = enemy;
  }

  public void Enter() => Die();

  public void Exit()
  {
  }

  private void Die()
  {
    _particle.Play();
    _particle.transform.parent = null;
    _enemy.gameObject.SetActive(false);
    _enemy.transform.SetParent(_pool);
  }
}