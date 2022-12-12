using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : BaseManager
{
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private CinemachineVirtualCamera successCamera;
    [SerializeField] private CinemachineVirtualCamera failCamera;

    private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    private void OnEnable()
    {
        EventManager.levelLoadedEvent.AddListener(SelectGameplayCamera);
        EventManager.levelSuccessEvent.AddListener(SelectSuccessCamera);
        EventManager.levelFailEvent.AddListener(SelectFailCamera);
    }

    private void OnDisable()
    {
        EventManager.levelLoadedEvent.RemoveListener(SelectGameplayCamera);
        EventManager.levelSuccessEvent.RemoveListener(SelectSuccessCamera);
        EventManager.levelFailEvent.RemoveListener(SelectFailCamera);
    }

    public override void Init()
    {
        base.Init();
        cameras.Clear();
        cameras.Add(gameplayCamera);
        cameras.Add(successCamera);
        cameras.Add(failCamera);
    }

    public void InitTarget(Transform target)
    {
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.SetTarget(target);
        }
    }

    private void SelectCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.Priority = 10;
        }
        targetCamera.Priority = 11;
    }

    private void SelectGameplayCamera(LevelLoadedEventData arg0)
    {
        SelectGameplayCamera();
    }

    public void SelectGameplayCamera()
    {
        SelectCamera(gameplayCamera);
    }

    public void SelectSuccessCamera()
    {
        SelectCamera(successCamera);
    }

    public void SelectFailCamera()
    {
        SelectCamera(failCamera);
    }
}
