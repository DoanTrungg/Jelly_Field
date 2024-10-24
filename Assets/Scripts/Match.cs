using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour
{
    private Board _board;
    private int _width;
    private int _height;
    private int _level;
    private void Start()
    {
        _board = Board.Instance();
        _level = ConfigBoard.Instance().level - 1;
        _height = ConfigBoard.Instance().listLevel[_level].height;
        _width = ConfigBoard.Instance().listLevel[_level].width;
    }
    public void MatchTileRight(BackgroundTile currentTile)
    {
        int row = currentTile.Row;
        int column = currentTile.Column;
        if (row + 1 == _height || _board.ListBackgroundTile[row + 1, column].Background || _board.ListBackgroundTile[row + 1, column].Hide) return; 
        if (currentTile.ListDot[0].Id == _board.ListBackgroundTile[row + 1, column].ListDot[2].Id) // 0 - 2
        {
            currentTile.ListDot[0].Match = true;
            _board.ListBackgroundTile[row + 1, column].ListDot[2].Match = true;

            UpdateTileAfterMatch(currentTile);
            UpdateTileAfterMatch(_board.ListBackgroundTile[row + 1, column]);
        }

        if (currentTile.ListDot[1].Id == _board.ListBackgroundTile[row + 1, column].ListDot[3].Id) // 0 - 3
        {
            currentTile.ListDot[1].Match = true;
            _board.ListBackgroundTile[row + 1, column].ListDot[3].Match = true;

            UpdateTileAfterMatch(currentTile);
            UpdateTileAfterMatch(_board.ListBackgroundTile[row + 1, column]);
        }
    }
    private void UpdateTileAfterMatch(BackgroundTile tile)
    {
        switch (tile.Dimension)
        {
            case Dimension.AllSynch:
                //if(tile.ListDot[0].Match || tile.ListDot[1].Match || tile.ListDot[2].Match || tile.ListDot[3].Match)
                //    tile.GetComponent<Dofade>().FadeOut(0.5f).OnComplete(() =>
                //    {
                //        tile.Hide = true;
                //        tile.HideTile(tile.Hide);
                //    });
                break;

            case Dimension.TwoHorizonte:
                if (tile.ListDot[0].Match || tile.ListDot[1].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[1], tile.ListDot[3]);
                }
                if (tile.ListDot[2].Match || tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[2], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                break;

            case Dimension.TwoVertical:
                if (tile.ListDot[0].Match || tile.ListDot[2].Match)
                {
                    ChangeColor(tile.ListDot[0], tile.ListDot[3]);
                    ChangeColor(tile.ListDot[2], tile.ListDot[3]);
                }
                if (tile.ListDot[1].Match || tile.ListDot[3].Match)
                {
                    ChangeColor(tile.ListDot[1], tile.ListDot[0]);
                    ChangeColor(tile.ListDot[3], tile.ListDot[0]);
                }
                break;

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
                }
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
            default:
                break;
        }
    }
    private void ChangeColor(Dot currentDot ,  Dot newDot)
    {
        currentDot.GetComponent<Image>().color = newDot.GetComponent<Image>().color;
        currentDot.Match = false;
        currentDot.Id = newDot.Id;
    }
}
