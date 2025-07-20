using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllLevelsConfig", menuName = "Scriptable Objects/All Levels Config")]
public class AllLevelsConfig : ScriptableObject
{
    public List<LevelConfig> levels;
}
