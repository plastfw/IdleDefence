using System;
using NTC.OverlapSugar;
using UnityEngine;

public class Base : MonoBehaviour
{
  [SerializeField] private OverlapSettings _overlap;

  public Action<int> IsDamaged;

#if true
  public void OnDrawGizmosSelected() => _overlap.TryDrawGizmos();
#endif

  private void Update()
  {
    if (_overlap.TryFind(out Enemy enemy))
    {
      IsDamaged?.Invoke(enemy.Damage);
      enemy.GetDamage(10);
    }
  }
}