using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class TypeTile : MonoBehaviour
{
    private BackgroundTile backgroundTile;

    public BackgroundTile SynchHorizonte(BackgroundTile tile, bool synchUp)
    {
        int random = Random.Range(0, ConfigBoard.Instance().listColor.Count);

        if (synchUp)
        {
            //up
            ChangeColorDot(tile, 0, random);
            ChangeColorDot(tile, 1, random);
        }
        else
        {
            // down
            ChangeColorDot(tile, 2, random);
            ChangeColorDot(tile, 3, random);
        }

        return tile;
    }
    public BackgroundTile AllSynch(BackgroundTile tile)
    {
        int random = Random.Range(0, ConfigBoard.Instance().listColor.Count);

        ChangeColorDot(tile, 0, random);
        ChangeColorDot(tile, 1, random);

        ChangeColorDot(tile, 2, random);
        ChangeColorDot(tile, 3, random);

        return tile;
    }
    public BackgroundTile TwoHorizonte(BackgroundTile tile)
    {
        tile.TypeTile.SynchHorizonte(tile, true);
        tile.TypeTile.SynchHorizonte(tile, false);

        return tile;
    }
    public BackgroundTile TwoVertical(BackgroundTile tile)
    {
        int first = Random.Range(0, ConfigBoard.Instance().listColor.Count);
        int second;
        if (first == ConfigBoard.Instance().listColor.Count - 1)
        {
            second = first - 1;
        }
        else
        {
            second = first + 1;
        }

        // right
        ChangeColorDot(tile, 0, first);
        ChangeColorDot(tile, 2, first);

        // left
        ChangeColorDot(tile, 1, second);
        ChangeColorDot(tile, 3, second);

        return tile;
    }
    public BackgroundTile MutiDirectionHorizonte(BackgroundTile tile, bool multiUp)
    {
        int first = Random.Range(0, ConfigBoard.Instance().listColor.Count);
        int second;
        if (first == ConfigBoard.Instance().listColor.Count - 1)
        {
            second = first - 1;
        }
        else
        {
            second = first + 1;
        }
        if (multiUp)
        {
            //up
            ChangeColorDot(tile, 0, first);
            ChangeColorDot(tile, 1, second);

            tile.TypeTile.SynchHorizonte(tile, false);
        }
        else
        {
            // down
            ChangeColorDot(tile, 2, first);
            ChangeColorDot(tile, 3, second);
            tile.TypeTile.SynchHorizonte(tile, true);
        }
        return tile;
    }
    private void ChangeColorDot(BackgroundTile tile, int indexListDot, int radom)
    {
        tile.ListDot[indexListDot].GetComponent<Image>().color = ConfigBoard.Instance().listColor[radom];
        tile.ListDot[indexListDot].Id = (ID)radom;
    }
    public bool HorizoneType(BackgroundTile tile)
    {
        List<Dot> listDot = tile.ListDot;

        ID id0 = listDot[0].Id;
        ID id1 = listDot[1].Id;
        ID id2 = listDot[2].Id;
        ID id3 = listDot[3].Id;

        if(id0 == id1 && id0 == id2 && id0 == id3) 
        {
            tile.Dimension = Dimension.AllSynch;
            return true; 
        }
        if (id0 == id1)
        {
            if (id0 != id2 && id0 != id3)
            {
                if (id2 == id3)
                {
                    tile.Dimension = Dimension.TwoHorizonte;
                }
                else
                {
                    tile.Dimension = Dimension.Mutil;
                }
            }
            else
            {
                if (id0 == id2)
                {
                    tile.Dimension = Dimension.ThreeSynch_Down_Left;
                }
                if (id0 == id3)
                {
                    tile.Dimension = Dimension.ThreeSynch_Down_Right;
                }
            }

            return true;
        }
        else
        {
            if (id2 == id3)
            {
                if (id2 == id0)
                {
                    tile.Dimension = Dimension.ThreeSynch_Up_Left;
                }
                else if (id2 == id1)
                {
                    tile.Dimension = Dimension.ThreeSynch_Up_Right;
                }
                else
                {
                    tile.Dimension = Dimension.Mutil;
                }
                return true;
            }
            else
            {
                tile.Dimension = Dimension.Mutil;
                return false;
            }
        }

    }

    public bool VerticalType(BackgroundTile tile)
    {
        List<Dot> listDot = tile.ListDot;

        ID id0 = listDot[0].Id;
        ID id1 = listDot[1].Id;
        ID id2 = listDot[2].Id;
        ID id3 = listDot[3].Id;

        if(id0 == id2 && id1 == id3)
        {
            tile.Dimension = Dimension.TwoVertical;
            return true;
        }
        return false;
    }
}
