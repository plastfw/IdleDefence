using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTC.OverlapSugar;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
  [SerializeField] private OverlapSettings _overlap;
  [SerializeField] private MissilePool _pool;
  [SerializeField] private Missile _missile;
  [SerializeField] private float _coolDown;
  [SerializeField] private int _damage = 3;
  [SerializeField] private int _range = 1;
  [SerializeField] public LevelsData _levelsData;

  [SerializeField] private GameObject _model;
  [SerializeField] private GameObject _overlapSize;

  private Enemy _currentEnemy;
  private Missile _currentMissile;
  private bool _canHit = true;
  private int _speedIndex = 0;
  private int _damageIndex = 0;
  private int _rangeIndex = 0;

  public int SpeedIndex => _speedIndex;
  public int DamageIndex => _damageIndex;
  public int RangeIndex => _rangeIndex;

  public int Damage => _damage;
  public float Speed => _coolDown;
  public int Range => _range;

#if true

  public void OnDrawGizmosSelected() => _overlap.TryDrawGizmos();

#endif

  public void UpDamage()
  {
    _damageIndex++;
    _damage = _levelsData.Damages[_damageIndex];
  }

  public void UpSpeed()
  {
    _speedIndex++;
    _coolDown = _levelsData.Speeds[_speedIndex];
  }

  public void UpRange()
  {
    _rangeIndex++;

    Vector3 newModelSize = new Vector3(_levelsData.RangeModel[_rangeIndex], _levelsData.RangeModel[_rangeIndex],
      _levelsData.RangeModel[_rangeIndex]);

    _model.transform.DOScale(newModelSize, .2f);
    _overlap.ChangeRadius(_levelsData.RangeOverlap[_rangeIndex]);
  }

  private void Update() => TryShoot();

  private void TryShoot()
  {
    if (_currentEnemy == null)
    {
      if (_overlap.TryFind(out Enemy enemy))
      {
        enemy.IsDead += ChangeTarget;
        _currentEnemy = enemy;
        if (_currentEnemy.PotentiallyHealth > 0)
        {
          _currentEnemy.GetPotentialDamage(_damage);
          if (_canHit)
          {
            ShootLogic();
          }
        }
      }
    }
    else
    {
      if (_currentEnemy.PotentiallyHealth > 0)
      {
        if (_canHit)
        {
          _currentEnemy.GetPotentialDamage(_damage);
          ShootLogic();
        }
      }
    }
  }

  private void ChangeTarget(Enemy enemy)
  {
    _currentEnemy = null;
    enemy.IsDead -= ChangeTarget;
  }


  private void ShootLogic()
  {
    _currentMissile = _pool.GetMissile();
    if (_currentMissile != null)
    {
      _currentMissile.transform.position = transform.position;
      _currentMissile.gameObject.SetActive(true);
      MoveMissile(_currentEnemy);
    }
    else
    {
      _currentMissile = Instantiate(_missile, transform.position, quaternion.identity);
      _currentMissile.Initialize(this);
      _pool.AddMissile(_currentMissile);
      MoveMissile(_currentEnemy);
    }
  }

  private void MoveMissile(Enemy enemy)
  {
    _canHit = false;

    _currentMissile.Move(_currentEnemy);

    StartCoroutine(CoolDown());
  }

  private IEnumerator CoolDown()
  {
    var coolDownDuration = new WaitForSeconds(_coolDown);
    yield return coolDownDuration;
    _canHit = true;
  }
}