public class LevelLoadedEventData
{
    public LevelData LevelData;
    public LevelPrefab LevelPrefab;
    public int LevelNo;

    public LevelLoadedEventData(LevelData levelData, LevelPrefab levelPrefab, int levelNo)
    {
        LevelData = levelData;
        LevelPrefab = levelPrefab;
        LevelNo = levelNo;
    }
}
