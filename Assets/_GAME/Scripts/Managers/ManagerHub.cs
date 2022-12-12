using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManagerHub : PersistentSingleton<ManagerHub>
{
    [SerializeField] private List<BaseManager> managersByPriorty;

    private readonly Dictionary<Type, BaseManager> managers = new Dictionary<Type, BaseManager>();

    private void Start()
    {
        PopulateDictionary();
        InitControllers();
    }

    private void PopulateDictionary()
    {
        foreach (BaseManager manager in managersByPriorty)
        {
            managers.Add(manager.GetType(), manager);
        }
    }

    private void InitControllers()
    {
        foreach (BaseManager manager in managers.Values)
        {
            manager.Init();
        }
    }

    public static T Get<T>() where T : BaseManager
    {
        return (T)Instance.managers[typeof(T)];
    }
}
