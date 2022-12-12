using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseManager
{
    private SaveManager saveManager;

    [SerializeField] private Transform levelParent;

    [SerializeField] private LevelData[] allLevels;
    [SerializeField] private LevelData[] repetitiveLevels;
    [SerializeField] private bool repeatRandomly = false;
    [SerializeField] private bool keyboardControl = true;

    private LevelData levelData;
    public LevelData LevelData { get => levelData; }
    private LevelPrefab levelPrefab;
    public LevelPrefab LevelPrefab { get => levelPrefab; }
    private int levelNo;
    public int LevelNo { get => levelNo; }

    private void OnEnable()
    {
        EventManager.levelSuccessEvent.AddListener(OnLevelSuccess);
    }

    private void OnDisable()
    {
        EventManager.levelSuccessEvent.RemoveListener(OnLevelSuccess);
    }

    public override void Init()
    {
        base.Init();
        saveManager = ManagerHub.Get<SaveManager>();
        LoadLevel();
    }

    public void LoadLevel()
    {
        ResetLevel();
        levelData = GetLevelData();
        UpdateSceneSettings();
        PrepareLevel();
    }

    private LevelData GetLevelData()
    {
        levelNo = saveManager.GetLevelNo();
        int levelIndex = GetLevelIndex(allLevels.Length, repetitiveLevels.Length, repeatRandomly);
        if (levelNo - 1 < allLevels.Length)
        {
            return allLevels[levelIndex];
        }
        return repetitiveLevels[levelIndex];
    }

    private int GetLevelIndex(int levelCount, int repetitiveCount, bool randomly)
    {
        int levelIndex = levelNo - 1;
        if (levelIndex < levelCount)
        {
            return levelIndex;
        }

        if (randomly)
        {
            return Random.Range(0, repetitiveCount);
        }

        return (levelIndex - levelCount) % repetitiveCount;
    }

    private void UpdateSceneSettings()
    {
        if (levelData.sceneSettings == null)
        {
            return;
        }
        RenderSettings.skybox = levelData.sceneSettings.skyboxMaterial;
        RenderSettings.fogColor = levelData.sceneSettings.fogColor;
    }

    private void OnLevelSuccess()
    {
        IncreaseLevelNo();
        ManagerHub.Get<CarController>().currentGameState = GameState.success;
    }

    private void IncreaseLevelNo()
    {
        saveManager.SaveLevelNo(levelNo + 1);
    }

    private LevelPrefab InstantiateLevel(LevelPrefab obj)
    {
        LevelPrefab result = Instantiate(obj, levelParent);
        return result;
    }

    private void PrepareLevel()
    {
        levelPrefab = InstantiateLevel(levelData.levelPrefab);
        LevelLoadedEventData data = new LevelLoadedEventData(levelData, levelPrefab, levelNo);
        EventManager.levelLoadedEvent.Invoke(data);
    }

    private void ResetLevel()
    {
        if (levelPrefab == null)
        {
            return;
        }
        Destroy(levelPrefab.gameObject);
    }

    private void Update()
    {
        KeyboardControl();
    }

    private void KeyboardControl()
    {
        if (!keyboardControl)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.levelStartEvent.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.levelSuccessEvent.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager.levelFailEvent.Invoke();
        }
    }
}
