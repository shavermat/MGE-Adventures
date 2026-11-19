using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLocationConfig", menuName ="Config/Location Config")]
public class LocationConfig : ScriptableObject
{
    public List<LevelConfigSO> levels = new List<LevelConfigSO>();
  public bool isCompleted;
  public String ID;
  public String locationNumber;
  
}
