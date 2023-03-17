using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
  private List<Enemy> _enemies = new List<Enemy>();

  public Enemy GetEnemy(SpeedType type)
  {
    var selectedEnemy = _enemies.FirstOrDefault(p => p.SpeedType == type);

    if (selectedEnemy == null)
      return null;

    return selectedEnemy;
  }

  public void DeleteEnemy(Enemy enemy) => _enemies.Remove(enemy);

  public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);
}