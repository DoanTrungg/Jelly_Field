using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    private int _height;
    private int _width;
    private BackgroundTile[,] _listBackgroundTile;
    [SerializeField] private BackgroundTile tilePrefab;
    public BackgroundTile[,] ListBackgroundTile { get => _listBackgroundTile; set => _listBackgroundTile = value; }
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }

    private void Start()
    {
        Height = ConfigBoard.Instance().height;
        Width = ConfigBoard.Instance().width;
        ListBackgroundTile = new BackgroundTile[Width, Height];
        SetupBoard();
    }

    public void SetupBoard()
    {

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                ListBackgroundTile[i, j] = Instantiate(tilePrefab, transform.position, Quaternion.identity);
                ListBackgroundTile[i, j].transform.SetParent(transform, false);
                ListBackgroundTile[i, j].transform.position = Vector2.zero;
                SetBackgroundTile(ListBackgroundTile[i, j], i, j, transform);
            }
        }
    }

    private void SetBackgroundTile(BackgroundTile backgroundTile, int width, int height, Transform transform)
    {
        backgroundTile.SetuupBackgroundTile();
        backgroundTile.gameObject.name = "( W : " + width + ", H :" + height + ")";
        backgroundTile.Row = width;
        backgroundTile.Column = height;
        backgroundTile.transform.SetParent(transform);
    }
}
