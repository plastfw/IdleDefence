using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
  [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
  [SerializeField] private Pool _pool;

  public event Action IsEmpty;
  public event Action EnemyIsDie;

  public void AddEnemy(Enemy enemy)
  {
    _enemies.Add(enemy);
    enemy.IsDead += DeleteEnemy;
  }

  private void DeleteEnemy(Enemy enemy)
  {
    enemy.IsDead -= DeleteEnemy;
    EnemyIsDie?.Invoke();

    _enemies.Remove(enemy);
    _pool.AddEnemy(enemy);

    if (_enemies.Count == 0)
      IsEmpty?.Invoke();
  }

  public void DeactivateEnemies()
  {
    foreach (var enemy in _enemies)
      enemy.Deactivate();
  }
}