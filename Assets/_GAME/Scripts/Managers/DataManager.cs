using UnityEngine;
//using GameAnalyticsSDK;

public class DataManager : BaseManager
{
    [SerializeField] private bool debugOnEditor = true;
    //[SerializeField] private bool sendGameAnalytics = true;

    private int levelNo;

    private void OnEnable()
    {
        EventManager.levelLoadedEvent.AddListener(OnLevelLoaded);
        EventManager.levelStartEvent.AddListener(OnLevelStart);
        EventManager.levelSuccessEvent.AddListener(OnLevelSuccess);
        EventManager.levelFailEvent.AddListener(OnLevelFail);
    }

    private void OnDisable()
    {
        EventManager.levelLoadedEvent.RemoveListener(OnLevelLoaded);
        EventManager.levelStartEvent.RemoveListener(OnLevelStart);
        EventManager.levelSuccessEvent.RemoveListener(OnLevelSuccess);
        EventManager.levelFailEvent.RemoveListener(OnLevelFail);
    }

    private void OnLevelLoaded(LevelLoadedEventData eventData)
    {
        levelNo = eventData.LevelNo;
    }

    private void OnLevelStart()
    {
#if UNITY_EDITOR
        if (debugOnEditor)
        {
            Debug.Log("LEVEL START");
        }
        //return;
#endif

        /*if (sendGameAnalytics)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level - " + levelNo);
        }*/
    }

    private void OnLevelSuccess()
    {
#if UNITY_EDITOR
        if (debugOnEditor)
        {
            Debug.Log("LEVEL SUCCESS");
        }
        //return;
#endif

        /*if (sendGameAnalytics)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level - " + levelNo);
        }*/
    }

    private void OnLevelFail()
    {
#if UNITY_EDITOR
        if (debugOnEditor)
        {
            Debug.Log("LEVEL FAIL");
        }
        //return;
#endif

        /*if (sendGameAnalytics)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level - " + levelNo);
        }*/
    }
}
