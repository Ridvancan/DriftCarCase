using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUI : BaseUI
{
	private void OnEnable()
	{
		EventManager.levelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.levelFailEvent.AddListener(OnLevelFail);
	}

	private void OnDisable()
	{
		EventManager.levelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.levelFailEvent.RemoveListener(OnLevelFail);
	}

	private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		SetHidden();
	}

	private void OnLevelFail()
	{
		SetShown();
	}
}
