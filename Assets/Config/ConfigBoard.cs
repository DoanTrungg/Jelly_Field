using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RemoteConfig", menuName = "Config/Board")]
public class ConfigBoard : SingletonScriptableObject<ConfigBoard>
{
    public int level = 1;

    public List<LevelConfig> listLevel;

    [Header("Color")]
    public List<Color> listColor = new List<Color>();
    public Color backgroundColor;
    public Color hide;
}

[System.Serializable]
public class LevelConfig
{
    public int width;
    public int height;
}
