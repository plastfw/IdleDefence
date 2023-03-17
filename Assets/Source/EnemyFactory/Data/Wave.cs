using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
  public int SlowCount;
  public int FastCount;

  public SpeedType Slow;
  public SpeedType Fast;
}