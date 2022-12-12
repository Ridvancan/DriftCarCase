using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : BaseManager
{
    public override void Init()
    {
        base.Init();
    }

    public int GetLevelNo()
    {
        return PlayerPrefs.GetInt(Consts.PrefKeys.LEVEL_NO, 1);
    }

    public void SaveLevelNo(int levelNo)
    {
        PlayerPrefs.SetInt(Consts.PrefKeys.LEVEL_NO, levelNo);
    }
}
