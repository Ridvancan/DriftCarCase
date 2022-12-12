using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Settings/LevelData")]
public class LevelData : ScriptableObject
{
	public LevelPrefab levelPrefab;
	public SceneSettings sceneSettings;
}