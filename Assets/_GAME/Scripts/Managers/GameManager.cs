using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
   
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
    void Update()
    {
        
    }
    public void CheckIsGameEnd()
    {
        if (ManagerHub.Get<PlayersManager>().players.Count == 1) EventManager.levelSuccessEvent?.Invoke();
        
    }
}
