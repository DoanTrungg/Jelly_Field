using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Match : MonoBehaviour
{
    private Board _board;
    private int _width;
    private int _height;
    private int _level;
    private HashSet<Dot> listMatch = new HashSet<Dot>();
    private void Start()
    {
        _board = Board.Instance();
        _level = ConfigBoard.Instance().level - 1;
        _height = ConfigBoard.Instance().listLevel[_level].height;
        _width = ConfigBoard.Instance().listLevel[_level].width;
    }

    public void MatchTileDown(BackgroundTile currentTile)
    {
        int row = currentTile.Row;
        int column = currentTile.Column;
        if (column - 1 < 0 || _board.ListBackgroundTile[row, column - 1].Background || _board.ListBackgroundTile[row, column - 1].Hide) return;
        if (currentTile.ListDot[0].Id == _board.ListBackgroundTile[row, column - 1].ListDot[1].Id)
        {
            currentTile.ListDot[0].Match = true;
            _board.ListBackgroundTile[row, column - 1].ListDot[1].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row, column - 1]);
        }

        if (currentTile.ListDot[2].Id == _board.ListBackgroundTile[row, column - 1].ListDot[3].Id)
        {
            currentTile.ListDot[2].Match = true;
            _board.ListBackgroundTile[row, column - 1].ListDot[3].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row, column - 1]);
        }
    }
    public void MatchTileUp(BackgroundTile currentTile)
    {
        int row = currentTile.Row;
        int column = currentTile.Column;
        if(column + 1 == _height || _board.ListBackgroundTile[row , column + 1].Background || _board.ListBackgroundTile[row, column + 1].Hide) return;
        if (currentTile.ListDot[1].Id == _board.ListBackgroundTile[row , column + 1].ListDot[0].Id) // 1 - 0
        {
            currentTile.ListDot[1].Match = true;
            _board.ListBackgroundTile[row , column + 1].ListDot[0].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row , column + 1]);
        }

        if (currentTile.ListDot[3].Id == _board.ListBackgroundTile[row, column + 1].ListDot[2].Id)
        {
            currentTile.ListDot[3].Match = true;
            _board.ListBackgroundTile[row, column + 1].ListDot[2].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row, column + 1]);
        }
    }
    public void MatchTileRight(BackgroundTile currentTile)
    {
        int row = currentTile.Row;
        int column = currentTile.Column;
        if (row + 1 == _width || _board.ListBackgroundTile[row + 1, column].Background || _board.ListBackgroundTile[row + 1, column].Hide) return; 
        if (currentTile.ListDot[0].Id == _board.ListBackgroundTile[row + 1, column].ListDot[2].Id) // 0 - 2
        {
            currentTile.ListDot[0].Match = true;
            _board.ListBackgroundTile[row + 1, column].ListDot[2].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row + 1, column]);
        }

        if (currentTile.ListDot[1].Id == _board.ListBackgroundTile[row + 1, column].ListDot[3].Id) // 1 - 3
        {
            currentTile.ListDot[1].Match = true;
            _board.ListBackgroundTile[row + 1, column].ListDot[3].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row + 1, column]);
        }
    }
    public void MatchTileLeft(BackgroundTile currentTile)
    {
        int row = currentTile.Row;
        int column = currentTile.Column;
        if (row - 1 < 0 || _board.ListBackgroundTile[row - 1, column].Background || _board.ListBackgroundTile[row - 1, column].Hide) return;
        if (currentTile.ListDot[2].Id == _board.ListBackgroundTile[row - 1, column].ListDot[0].Id) // 2 - 0
        {
            currentTile.ListDot[2].Match = true;
            _board.ListBackgroundTile[row - 1, column].ListDot[0].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row - 1, column]);
        }

        if (currentTile.ListDot[3].Id == _board.ListBackgroundTile[row - 1, column].ListDot[1].Id) // 3 - 1
        {
            currentTile.ListDot[3].Match = true;
            _board.ListBackgroundTile[row - 1, column].ListDot[1].Match = true;

            DotsMatch(currentTile);
            DotsMatch(_board.ListBackgroundTile[row - 1, column]);
        }
    }
    private void DotsMatch(BackgroundTile tile)
    {
        /*
        var dimension = tile.Dimension;

        var color0 = tile.ListDot[0].Id;
        var mathc0 = tile.ListDot[0].Match;

        var color1 = tile.ListDot[1].Id;
        var mathc1 = tile.ListDot[1].Match;

        var color2 = tile.ListDot[2].Id;
        var mathc2 = tile.ListDot[2].Match;

        var color3 = tile.ListDot[3].Id;
        var mathc3 = tile.ListDot[3].Match;
        */ // checkDot

        switch (tile.Dimension)
        {
            case Dimension.AllSynch:
                //CheckDone(tile);
                foreach (var dot in tile.ListDot)
                {
                    listMatch.Add(dot);
                }
                break;

            case Dimension.TwoHorizonte:
                if (tile.ListDot[0].Match || tile.ListDot[1].Match)
                {
                    listMatch.Add(tile.ListDot[0]);
                    listMatch.Add(tile.ListDot[1]);
                }
                if (tile.ListDot[2].Match || tile.ListDot[3].Match)
                {
                    listMatch.Add(tile.ListDot[2]);
                    listMatch.Add(tile.ListDot[3]);
                }
                break;

            case Dimension.TwoVertical:
                if (tile.ListDot[0].Match || tile.ListDot[2].Match)
                {
                    listMatch.Add(tile.ListDot[0]);
                    listMatch.Add(tile.ListDot[2]);
                }
                if (tile.ListDot[1].Match || tile.ListDot[3].Match)
                {
                    listMatch.Add(tile.ListDot[1]);
                    listMatch.Add(tile.ListDot[3]);
                }
                break;
            /*
            case Dimension.ThreeSynch_Down_Right:
                if (tile.ListDot[2].Match)
                {
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[2]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[2]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[2]);
                }
                break;

            case Dimension.ThreeSynch_Down_Left:
                if (tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[3]);
                };
                break;
            case Dimension.ThreeSynch_Up_Right:
                if (tile.ListDot[0].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                }
                else
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                break;
            case Dimension.ThreeSynch_Up_Left:
                if (tile.ListDot[1].Match)
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[2]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[1]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[1]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[1]);
                }
                break;
            */ // other Type
            default:
                break;
        }
    }
    private void MatchDotsOfTile(BackgroundTile tile)
    {
        switch (tile.Dimension)
        {
            case Dimension.AllSynch:
                CheckDone(tile);
                break;

            case Dimension.TwoHorizonte:
                if (tile.ListDot[0].Match || tile.ListDot[1].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[3]);

                    if (listMatch.Contains(tile.ListDot[0])) listMatch.Remove(tile.ListDot[0]);
                    if (listMatch.Contains(tile.ListDot[1])) listMatch.Remove(tile.ListDot[1]);
                }
                if (tile.ListDot[2].Match || tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);

                    if (listMatch.Contains(tile.ListDot[2])) listMatch.Remove(tile.ListDot[2]);
                    if (listMatch.Contains(tile.ListDot[3])) listMatch.Remove(tile.ListDot[3]);
                }
                break;

            case Dimension.TwoVertical:
                if (tile.ListDot[0].Match || tile.ListDot[2].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[3]);

                    if (listMatch.Contains(tile.ListDot[0])) listMatch.Remove(tile.ListDot[0]);
                    if (listMatch.Contains(tile.ListDot[2])) listMatch.Remove(tile.ListDot[2]);
                }
                if (tile.ListDot[1].Match || tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);

                    if (listMatch.Contains(tile.ListDot[1])) listMatch.Remove(tile.ListDot[1]);
                    if (listMatch.Contains(tile.ListDot[3])) listMatch.Remove(tile.ListDot[3]);
                }
                break;
            /*
            case Dimension.ThreeSynch_Down_Right:
                if (tile.ListDot[2].Match)
                {
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[2]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[2]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[2]);
                }
                break;

            case Dimension.ThreeSynch_Down_Left:
                if (tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[3]);
                };
                break;
            case Dimension.ThreeSynch_Up_Right:
                if (tile.ListDot[0].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                }
                else
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                break;
            case Dimension.ThreeSynch_Up_Left:
                if (tile.ListDot[1].Match)
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[2]);
                }
                else
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[1]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[1]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[1]);
                }
                break;
            */ // other Type
            default:
                break;
        }
    }
    public void UpdateTileAfterMatch()
    {
        var listMatchCopy = new List<Dot>(listMatch);

        foreach (var dot in listMatchCopy)
        {
            var dotOftile = dot.BackgroundTile;
            MatchDotsOfTile(dotOftile);
        }
        listMatch.Clear();
        Debug.Log(listMatch.Count + "countListMatch");
    }
    private void CheckDone(BackgroundTile tile)
    {
        if (tile.ListDot == null || tile.ListDot.Count == 0)
            return;

        var firstDotId = tile.ListDot[0].Id;
        var match = tile.ListDot[0].Match;
        foreach (var dot in tile.ListDot)
        {
            if (dot.Id != firstDotId && match) return;
        }
        foreach (var item in tile.ListDot)
        {
            if (listMatch.Contains(item)) listMatch.Remove(item);
        }
        tile.Hide = true;
        tile.HideTile(tile.Hide);
    }
    private void ChangeColor(Dot currentDot ,  Dot newDot)
    {
        currentDot.GetComponent<Image>().color = newDot.GetComponent<Image>().color;
        currentDot.Match = false;
        currentDot.Id = newDot.Id;
    }
}
