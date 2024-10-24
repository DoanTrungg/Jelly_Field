using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private BackgroundTile _backgroundTile;
    private ID _id;

    private bool match;

    public BackgroundTile BackgroundTile { get => _backgroundTile; set => _backgroundTile = value; }
    public ID Id { get => _id; set => _id = value; }
    public bool Match { get => match; set => match = value; }
}
public enum ID
{
    Pink,
    Gray,
    Brown,
    Green,
    None
}
