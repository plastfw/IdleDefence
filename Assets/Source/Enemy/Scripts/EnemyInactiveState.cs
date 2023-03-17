public class EnemyInactiveState : IEnemyState
{
  private Enemy _enemy;
  
  public EnemyInactiveState(Enemy enemy) => _enemy = enemy;

  public void Enter() => Deactivate();

  public void Exit()
  {
  }

  private void Deactivate() => _enemy.gameObject.SetActive(false);
}