using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoardChoice : MonoBehaviour
{
    public int amout;
    private BackgroundTile[] _listBackgroundTile;
    [SerializeField] private BackgroundTile tilePrefab;
    int level;

    public BackgroundTile[] ListBackgroundTile { get => _listBackgroundTile; set => _listBackgroundTile = value; }

    private void Start()
    {
        level = ConfigBoard.Instance().level;
        ListBackgroundTile = new BackgroundTile[amout];
        SetupBoard();
    }

    public void SetupBoard()
    {

        for (int i = 0; i < amout; i++)
        {
            ListBackgroundTile[i] = Instantiate(tilePrefab, transform.position, Quaternion.identity);
            ListBackgroundTile[i].hide = false;
            ListBackgroundTile[i].transform.SetParent(transform, false);
            ListBackgroundTile[i].transform.localPosition = Vector2.zero;
            SetBackgroundTile(ListBackgroundTile[i], transform);
        }
    }
    private void SetBackgroundTile(BackgroundTile backgroundTile, Transform transform)
    {
        backgroundTile.SetupBackgroundTile();
        backgroundTile.transform.SetParent(transform);
        if (level == 3)
        {
            backgroundTile.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            backgroundTile.GetComponent<GridLayoutGroup>().cellSize = new Vector2(25, 25);
        }
        else
        {
            backgroundTile.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            backgroundTile.GetComponent<GridLayoutGroup>().cellSize = new Vector2(30, 30);
        }
    }
}
