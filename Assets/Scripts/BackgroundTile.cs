using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BackgroundTile : MonoBehaviour
{
    private const int cellSize3x3 = 35;
    private const int cellSizeY4x4 = 30;
    private List<Dot> _listDot = new List<Dot>();
    private int _column;
    private int _row;
    private bool hide;
    public int Column { get => _column; set => _column = value; }
    public int Row { get => _row; set => _row = value; }
    public List<Dot> ListDot { get => _listDot; set => _listDot = value; }
    public bool Hide { get => hide; set => hide = value; }

    public int CellSize3x3 => cellSize3x3;

    public int CellSizeY4x4 => cellSizeY4x4;

    private void Start()
    {
        
    }
    public void SetuupBackgroundTile()
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
        int random = Random.Range(0, ConfigBoard.Instance().listColor.Count);
        dot.GetComponent<Image>().color = ConfigBoard.Instance().listColor[random];
        dot.BackgroundTile = this;
        dot.Id = (ID)random;
    }
    public void HideTile(bool hide)
    {
        Color hideColor = ConfigBoard.Instance().backgroundColor;
        if (hide)
        {
            gameObject.GetComponent<Image>().color = hideColor;
            foreach(var dot  in _listDot)
            {
                dot.GetComponent<Image>().color = hideColor;
            }
        }
    }
}
