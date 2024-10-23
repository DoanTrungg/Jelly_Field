using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private BackgroundTile _backgroundTile;
    private ID _id;

    public BackgroundTile BackgroundTile { get => _backgroundTile; set => _backgroundTile = value; }
    public ID Id { get => _id; set => _id = value; }
}
public enum ID
{
    One,
    Two,
    Three,
    Four,
    None
}
