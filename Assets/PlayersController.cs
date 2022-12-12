using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : BaseManager
{
    public List<GameObject> players;

    public Transform NearestEnemyTransform(Transform selfTransform)
    {
        float distance = -1;
        Transform tempTransform = null;
        for (int i = 0; i < players.Count; i++)
        {
            if (selfTransform != players[i].transform)
            {
                if (distance < Vector3.Distance(selfTransform.position, players[i].transform.position))
                {
                    tempTransform = players[i].transform;
                    distance = Vector3.Distance(selfTransform.position, tempTransform.position);
                }
            }
        }
        return tempTransform;

    }
}
