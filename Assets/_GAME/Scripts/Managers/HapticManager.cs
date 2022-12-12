using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticManager : BaseManager
{
    [SerializeField] private bool hapticsOn = true;
    [SerializeField] private bool debugOnEditor = true;

    private void OnEnable()
    {
        EventManager.levelSuccessEvent.AddListener(PlaySuccessHaptic);
        EventManager.levelFailEvent.AddListener(PlayFailHaptic);
    }

    private void OnDisable()
    {
        EventManager.levelSuccessEvent.RemoveListener(PlaySuccessHaptic);
        EventManager.levelFailEvent.RemoveListener(PlayFailHaptic);
    }

    public void PlaySuccessHaptic()
    {
        PlayHaptic(HapticTypes.Success);
    }

    public void PlayFailHaptic()
    {
        PlayHaptic(HapticTypes.HeavyImpact);
    }

    public void PlaySelectionHaptic()
    {
        PlayHaptic(HapticTypes.Selection);
    }

    public void PlayHaptic(HapticTypes type)
    {
        if (!hapticsOn) return;
        MMVibrationManager.Haptic(type);
#if UNITY_EDITOR
        if (debugOnEditor)
        {
            Debug.Log("Haptic Played: " + type);
        }
#endif
    }
}
