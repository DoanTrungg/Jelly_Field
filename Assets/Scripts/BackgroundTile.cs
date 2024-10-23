using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BackgroundTile : MonoBehaviour
{
    private List<Dot> _listDot = new List<Dot>();
    private int _column;
    private int _row;
    public int Column { get => _column; set => _column = value; }
    public int Row { get => _row; set => _row = value; }
    public List<Dot> ListDot { get => _listDot; set => _listDot = value; }

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
}
