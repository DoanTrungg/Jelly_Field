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
    public bool hide;
    public bool background;
    public int Column { get => _column; set => _column = value; }
    public int Row { get => _row; set => _row = value; }
    public List<Dot> ListDot { get => _listDot; set => _listDot = value; }
    public bool Hide { get => hide; set => hide = value; }

    public int CellSize3x3 => cellSize3x3;

    public int CellSizeY5x5 => cellSizeY5x5;

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
            return;
        }
        if (background)
        {
            dot.GetComponent<Image>().color = ConfigBoard.Instance().backgroundColor;
            dot.Id = ID.None;
            gameObject.GetComponent<Image>().color = ConfigBoard.Instance().backgroundColor;
            Destroy(GetComponent<DropArea>());
            return;
        }
        int random = Random.Range(0, ConfigBoard.Instance().listColor.Count);
        dot.GetComponent<Image>().color = ConfigBoard.Instance().listColor[random];
        dot.Id = (ID)random;
    }
    public void CopyColor(BackgroundTile newTile, BackgroundTile curretTile)
    {
        Debug.Log(curretTile.ListDot.Count + "countttttttt");
        for (int i = 0; i < curretTile.ListDot.Count; i++)
        {
            curretTile.ListDot[i].GetComponent<Image>().color = newTile.ListDot[i].GetComponent<Image>().color;
            curretTile.ListDot[i].Id = newTile.ListDot[i].Id;
        }
        curretTile.GetComponent<Image>().color = newTile.GetComponent<Image>().color;
        curretTile.hide = false;
    }
    public void HideTile(bool hide)
    {
        Color hideColor = ConfigBoard.Instance().backgroundColor;
        if (hide)
        {
            gameObject.GetComponent<Image>().color = hideColor;
            foreach(var dot  in _listDot)
            {
                dot.GetComponent<Image>().color = ConfigBoard.Instance().hide;
                dot.Id = ID.None;
                gameObject.GetComponent<Image>().color = ConfigBoard.Instance().hide;
            }
        }
    }
}
