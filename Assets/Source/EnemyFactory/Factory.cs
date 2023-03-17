using UnityEngine;
using Random = UnityEngine.Random;

public class Factory : MonoBehaviour
{
  [SerializeField] private WavesData _wavesData;
  [SerializeField] private Enemy _slowEnemy;
  [SerializeField] private Enemy _fastEnemy;
  [SerializeField] private EnemyGroup _enemyGroup;
  [SerializeField] private Pool _pool;
  [SerializeField] private Base _playerBase;
  [SerializeField] private int _spawnRadius;

  private int _currentIndex = 0;

  private void OnEnable() => _enemyGroup.IsEmpty += ActiveNextWave;

  private void OnDisable() => _enemyGroup.IsEmpty -= ActiveNextWave;

  private void Awake() => InitEnemies();

  private void InitEnemies()
  {
    for (int i = 0; i < _wavesData.Waves[_currentIndex].SlowCount; i++)
    {
      var currentEnemy = Instantiate(_slowEnemy, SetEnemyPosition(), Quaternion.identity);

      currentEnemy.Initialize(_playerBase, _pool.transform);
      _enemyGroup.AddEnemy(currentEnemy);
    }

    for (int i = 0; i < _wavesData.Waves[_currentIndex].FastCount; i++)
    {
      var currentEnemy = Instantiate(_fastEnemy, SetEnemyPosition(), Quaternion.identity);

      currentEnemy.Initialize(_playerBase, _pool.transform);
      _enemyGroup.AddEnemy(currentEnemy);
    }
  }

  private void ActiveNextWave()
  {
    if (_currentIndex != _wavesData.Waves.Count - 1)
      _currentIndex++;

    int slowCount = _wavesData.Waves[_currentIndex].SlowCount;
    int fastCount = _wavesData.Waves[_currentIndex].FastCount;

    for (int i = 0; i < slowCount; i++)
    {
      var currentEnemy = _pool.GetEnemy(SpeedType.Slow);

      if (currentEnemy == null)
        InstantiateEnemy(_slowEnemy);
      else
        ReuseEnemy(currentEnemy);
    }

    for (int i = 0; i < fastCount; i++)
    {
      var currentEnemy = _pool.GetEnemy(SpeedType.Fast);

      if (currentEnemy == null)
        InstantiateEnemy(_fastEnemy);
      else
        ReuseEnemy(currentEnemy);
    }
  }

  private void InstantiateEnemy(Enemy enemy)
  {
    var currentEnemy = Instantiate(enemy, SetEnemyPosition(), Quaternion.identity);

    currentEnemy.Initialize(_playerBase, _pool.transform);
    _enemyGroup.AddEnemy(currentEnemy);
  }

  private void ReuseEnemy(Enemy enemy)
  {
    _enemyGroup.AddEnemy(enemy);
    _pool.DeleteEnemy(enemy);
    enemy.transform.localPosition = Vector3.zero;
    enemy.transform.position = SetEnemyPosition();
    enemy.Revive();
  }

  private Vector2 SetEnemyPosition() => Random.insideUnitCircle * _spawnRadius;
}