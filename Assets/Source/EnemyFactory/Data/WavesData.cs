using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WavesData : ScriptableObject
{
  public List<Wave> Waves = new List<Wave>();
}