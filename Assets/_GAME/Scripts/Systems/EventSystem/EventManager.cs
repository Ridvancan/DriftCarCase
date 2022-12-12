using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoadedEvent : UnityEvent<LevelLoadedEventData> {}
public class LevelStartEvent : UnityEvent {}
public class LevelSuccessEvent : UnityEvent {}
public class LevelFailEvent : UnityEvent {}

public static class EventManager
{
    public static readonly LevelLoadedEvent levelLoadedEvent = new LevelLoadedEvent();
    public static readonly LevelStartEvent levelStartEvent = new LevelStartEvent();
    public static readonly LevelSuccessEvent levelSuccessEvent = new LevelSuccessEvent();
    public static readonly LevelFailEvent levelFailEvent = new LevelFailEvent();
}
