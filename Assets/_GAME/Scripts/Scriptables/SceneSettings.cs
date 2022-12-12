using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Settings/Level Settings")]
public class SceneSettings : ScriptableObject
{
    public Material skyboxMaterial;
    public Color fogColor;
}
