using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BlockController Current { get; set; }

    private const int GridSizeX = 10;
    private const int GridSizeY = 20;


    public bool [,] Grid = new bool[GridSizeX,GridSizeY];

    public float GameSpeed => gameSpeed;

    [SerializeField, Range(.1f, 1f)] private float gameSpeed = 1;

    [SerializeField] private List<BlockController> listPrefabs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Spawn();
    }
    public bool IsInside(List<Vector2> listCoordinate)
    {
        

        foreach (var coordinate in listCoordinate)
        {
            int x = Mathf.RoundToInt(coordinate.x);
            int y = Mathf.RoundToInt(coordinate.y);

            if (x < 0 || x >= GridSizeX)
            {
                //Horizontal out
                return false;
            }
            if(y < 0 || y >= GridSizeY)
            {
                //Vertical out
                return false;
            }
            if (Grid[x, y])
            {
                //Hit something
                return false;
            }
        }
        return true;
    }

    public void Spawn()
    {
        var index = Random.Range(0, listPrefabs.Count);
        var blockController = listPrefabs[index];
        var newBlock = Instantiate(blockController);
        Current = newBlock;
    }
}

