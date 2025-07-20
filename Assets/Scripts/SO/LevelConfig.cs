using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/Level Config")]
public class LevelConfig : ScriptableObject
{
    [Range(1, 10)]
    public int level;
    public string sceneName;
    public bool status;
}
