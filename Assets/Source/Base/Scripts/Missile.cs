using NTC.OverlapSugar;
using UnityEngine;

public class Missile : MonoBehaviour
{
  [SerializeField] private OverlapSettings _overlap;
  [SerializeField] private Rigidbody _rigidbodyMissile;
  [SerializeField] private TrailRenderer _renderer;
  [SerializeField] private Shooter _shooter;

  private float _speed = 5f;

  public Rigidbody RigidbodyMissile => _rigidbodyMissile;

#if UNITY_EDITOR
  private void OnDrawGizmos() => _overlap.TryDrawGizmos();
#endif


  public void Initialize(Shooter shooter) => _shooter = shooter;

  private void Update()
  {
    if (_overlap.TryFind(out Enemy enemy))
    {
      enemy.GetDamage(_shooter.Damage);
      gameObject.SetActive(false);
    }
  }

  public void Move(Enemy enemy)
  {
    var direction = enemy.transform.position - transform.position;

    _rigidbodyMissile.velocity = direction.normalized * _speed;
  }
}