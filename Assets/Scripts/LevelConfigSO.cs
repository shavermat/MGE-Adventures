using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName ="Config/Level Config")]
public class LevelConfigSO : ScriptableObject
{

[Header("EnemyPref")]
public List<GameObject> enemyprefabs;

public int levelIndex;
public int levelReward;

}
