using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AirdropController : MonoBehaviour
{
    [SerializeField] GameObject airDropObject;
    [SerializeField] GameObject boosterBox;
    [Header("AirDropParameters")]
    [SerializeField] float airDropSpawnRate;
    [SerializeField] float airDropSpawnCounter;
    [SerializeField] float airDropSpeed = 5;

    void Start()
    {
        airDropSpawnCounter = 10;
    }
    void ParachuteResetPosition()
    {
        float arenaRange = ManagerHub.Get<ArenaController>().CurrentMaxRange();
        airDropObject.transform.position = new Vector3(Random.Range(-arenaRange, arenaRange), 20, Random.Range(-arenaRange, arenaRange));
    }
    void AirDropCooldown()
    {
        airDropSpawnCounter -= Time.deltaTime;
        if (airDropSpawnCounter <= 0)
        {
            airDropSpawnCounter = airDropSpawnRate;
            SpawnAirDrop();
        }
    }
    void SpawnAirDrop()
    {
        ParachuteResetPosition();
        airDropObject.SetActive(true);
        airDropObject.transform.DOMoveY(4, airDropSpeed).OnComplete(() => AirDropArrived());
    }

    void Update()
    {
        AirDropCooldown();
    }

    void AirDropArrived()
    {
        airDropObject.SetActive(false);
        boosterBox.SetActive(true);
        boosterBox.transform.position = airDropObject.transform.position + Vector3.up;
    }

}
