using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
  [SerializeField] private List<Missile> _missles = new List<Missile>();


  public Missile GetMissile()
  {
    var selectedMissile = _missles.FirstOrDefault(p => p.gameObject.activeSelf == false);

    if (selectedMissile == null)
      return null;

    return selectedMissile;
  }

  public void DeleteMissile(Missile missile) => _missles.Remove(missile);

  public void AddMissile(Missile missile) => _missles.Add(missile);
}