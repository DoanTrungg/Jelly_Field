using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BackgroundTile : MonoBehaviour
{
    private const int cellSize3x3 = 35;
    private const int cellSizeY5x5 = 25;
    private List<Dot> _listDot = new List<Dot>();
    private int _column;
    private int _row;
    private Dimension _dimension;
    public bool hide;
    private bool background;
    private TypeTile typeTile;
    public int Column { get => _column; set => _column = value; }
    public int Row { get => _row; set => _row = value; }
    public List<Dot> ListDot { get => _listDot; set => _listDot = value; }
    public bool Hide { get => hide; set => hide = value; }

    public int CellSize3x3 => cellSize3x3;

    public int CellSizeY5x5 => cellSizeY5x5;

    public Dimension Dimension { get => _dimension; set => _dimension = value; }
    public TypeTile TypeTile { get => typeTile; set => typeTile = value; }
    public bool Background { get => background; set => background = value; }

    private void Awake()
    {
        typeTile = GetComponent<TypeTile>();
    }
    public void SetupBackgroundTile()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ListDot.Add(transform.GetChild(i).GetComponent<Dot>());
            InitializeDot(transform.GetChild(i).GetComponent<Dot>());
            transform.GetChild(i).name = i.ToString();
        }
    }
    public void InitializeDot(Dot dot)
    {
        if (ConfigBoard.Instance().listColor.Count <= 0) return;
        dot.gameObject.SetActive(true);
        dot.BackgroundTile = this;
        if (hide)
        {
            dot.GetComponent<Image>().color = ConfigBoard.Instance().hide;
            dot.Id = ID.None;
            gameObject.GetComponent<Image>().color = ConfigBoard.Instance().hide;
            dot.Match = false;
            return;
        }
        if (Background)
        {
            dot.GetComponent<Image>().color = ConfigBoard.Instance().backgroundColor;
            dot.Id = ID.None;
            gameObject.GetComponent<Image>().color = ConfigBoard.Instance().backgroundColor;
            Destroy(GetComponent<DropArea>());
            dot.Match = false;
            return;
        }
    }
    public void CopyColor(BackgroundTile newTile, BackgroundTile curretTile)
    {
        for (int i = 0; i < curretTile.ListDot.Count; i++)
        {
            curretTile.ListDot[i].GetComponent<Image>().color = newTile.ListDot[i].GetComponent<Image>().color;
            curretTile.ListDot[i].Id = newTile.ListDot[i].Id;
            curretTile.ListDot[i].Match = true;
        }
        curretTile.GetComponent<Image>().color = newTile.GetComponent<Image>().color;
        curretTile.TypeTile = newTile.TypeTile;
        curretTile.hide = false;
    }
    public void HideTile(bool hide)
    {
        Color hideColor = ConfigBoard.Instance().hide;
        if (hide)
        {
            gameObject.GetComponent<Image>().color = hideColor;
            foreach(var dot  in _listDot)
            {
                dot.Id = ID.None;
                dot.Match = false;
                gameObject.GetComponent<Image>().color = ConfigBoard.Instance().hide;
            }
        }
    }
    public void RandomTypeTile(int random)
    {
        if (typeTile == null) Debug.Log("nulllll");
        switch(random)
        {
            case 0:
                typeTile.TwoHorizonte(this);
                break;
            case 1:
                typeTile.MutiDirectionHorizonte(this, true);
                break;
            case 2:
                typeTile.MutiDirectionHorizonte(this, false);
                break;
            case 3:
                typeTile.TwoVertical(this);
                break;
            default:
                typeTile.AllSynch(this);
                break;
        }
    }
    public void CheckDimension()
    {
        if (!typeTile.HorizoneType(this))
        {
            typeTile.VerticalType(this);
        }
    }
}
public enum Dimension
{
    TwoHorizonte,
    TwoVertical,

    ThreeSynch_Down_Right,
    ThreeSynch_Down_Left,
    ThreeSynch_Up_Right,
    ThreeSynch_Up_Left,

    AllSynch,

    Mutil

}
