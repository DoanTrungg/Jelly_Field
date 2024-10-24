using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Board : Singleton<Board>
{
    private const int cellSize3x3 = 70;
    private const int cellSizeY5x5 = 50;
    private int _height;
    private int _width;
    private BackgroundTile[,] _listBackgroundTile;
    [SerializeField] private BackgroundTile tilePrefab;
    [SerializeField] private Match match;
    public int level;

    public BackgroundTile[,] ListBackgroundTile { get => _listBackgroundTile; set => _listBackgroundTile = value; }
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }

    private void Start()
    {
        level = ConfigBoard.Instance().level;
        SetupLevelBoard(level);
        ListBackgroundTile = new BackgroundTile[Width, Height];
        SetupBoard();
    }

    private void Update()
    {
        foreach (var tile in ListBackgroundTile)
        {
            if (tile.Hide || tile.Background) continue;
            tile.CheckDimension();
            match.MatchTileRight(tile);
        }
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
    private void SetupLevelBoard(int level)
    {
        level--;
        _height = ConfigBoard.Instance().listLevel[level].height;
        _width = ConfigBoard.Instance().listLevel[level].width;
        switch (level)
        {
            case 0:
            case 1:
                gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize3x3, cellSize3x3);
                tilePrefab.GetComponent<GridLayoutGroup>().cellSize = new Vector2(tilePrefab.CellSize3x3, tilePrefab.CellSize3x3);
                break;
            default:
                gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSizeY5x5, cellSizeY5x5);
                tilePrefab.GetComponent<GridLayoutGroup>().cellSize = new Vector2(tilePrefab.CellSizeY5x5, tilePrefab.CellSizeY5x5);
                break;
        }
    }
    private void SetBackgroundTile(BackgroundTile backgroundTile, int width, int height, Transform transform)
    {
        backgroundTile.SetupBackgroundTile();
        backgroundTile.gameObject.name = "( W : " + width + ", H :" + height + ")";
        backgroundTile.Row = width;
        backgroundTile.Column = height;
        backgroundTile.transform.SetParent(transform);

        //type
        if (!backgroundTile.hide && !backgroundTile.Background) 
        { 
            backgroundTile.RandomTypeTile(Random.Range(0, 4)); 
            backgroundTile.CheckDimension();
        }
    }
}
