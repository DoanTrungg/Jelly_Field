using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RemoteConfig", menuName = "Config/Board")]
public class ConfigBoard : SingletonScriptableObject<ConfigBoard>
{
    [Header("Board Size")]
    public int width;
    public int height;

    [Header("Color")]
    public List<Color> listColor = new List<Color>();
}
