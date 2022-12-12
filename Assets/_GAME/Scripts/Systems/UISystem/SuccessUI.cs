using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessUI : BaseUI
{
	private void OnEnable()
	{
		EventManager.levelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.AddListener(OnLevelSuccess);
	}

	private void OnDisable()
	{
		EventManager.levelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.RemoveListener(OnLevelSuccess);
	}

	private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		SetHidden();
	}

	private void OnLevelSuccess()
	{
		SetShown();
	}
}
