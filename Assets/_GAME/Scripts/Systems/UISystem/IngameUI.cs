using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : BaseUI
{
	[SerializeField] private TextMeshProUGUI levelText;

	private void OnEnable()
	{
		EventManager.levelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.AddListener(OnLevelSuccess);
		EventManager.levelFailEvent.AddListener(OnLevelFail);
	}

	private void OnDisable()
	{
		EventManager.levelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.RemoveListener(OnLevelSuccess);
		EventManager.levelFailEvent.RemoveListener(OnLevelFail);
	}

    private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		levelText.text = "LEVEL " + eventData.LevelNo.ToString();
		SetShown();
	}

	private void OnLevelFail()
	{
		SetHidden();
	}

	private void OnLevelSuccess()
	{
		SetHidden();
	}
}
