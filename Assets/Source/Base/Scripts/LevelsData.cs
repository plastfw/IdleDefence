using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 2)]
public class LevelsData : ScriptableObject
{
  public List<float> Speeds;
  public List<int> Damages;
  public List<float> RangeModel;
  public List<float> RangeOverlap;
}