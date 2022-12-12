using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FailArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarController>())
        {
            EventManager.levelFailEvent?.Invoke();
        }
        if (other.GetComponent<AIPlayer>())
        {
            ManagerHub.Get<PlayersManager>().players.Remove(other.gameObject);
            ManagerHub.Get<GameManager>().CheckIsGameEnd();
        }
    }
}
