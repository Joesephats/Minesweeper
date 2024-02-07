using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{

    bool[,] bombGrid = new bool[5, 5];
    GameObject[,] buttonGrid = new GameObject[5, 5];
    [SerializeField] GameObject tile;
    [SerializeField] Transform canvasTransfrom;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject currentTile = Instantiate(tile, canvasTransfrom, false);

                buttonGrid[i, j] = currentTile;
                currentTile.transform.position = new Vector2((i * 100f) + 350, (j * 100f) + 75);
                Debug.Log($"Tile: ({i}, {j}) at Pos: {currentTile.transform.position.x}, {currentTile.transform.position.y}");
                currentTile.GetComponent<Tile>().row = i;
                currentTile.GetComponent<Tile>().col = j;

                //zzz
                currentTile.GetComponent<Button>().onClick.AddListener(() => TileClicked(currentTile.GetComponent<Tile>().row, currentTile.GetComponent<Tile>().col));
            }
        }


        InitializeBoolArray(bombGrid);

        Debug.Log("-----------------------------------------------------------------------------");

        PlaceMines(bombGrid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeBoolArray(bool[,] array)
    {
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                array[i, j] = false;
                Debug.Log($"{array[i, j]}, at {i},{j}");
            }
        }
    }

    void PlaceMines(bool[,] array)
    {
        for (int i = 0; i < 5; i++)
        {
            int randomRow = Random.Range(0, 5);
            int randomColumn = Random.Range(0, 5);
            while (array[randomRow, randomColumn] == true)
            {
                randomRow = Random.Range(0, 5);
                randomColumn = Random.Range(0, 5);
            }

            array[randomRow, randomColumn] = true;

            Debug.Log($"Mine at Index [{randomRow},{randomColumn}]");
        }

    }

    public void TileClicked(int row, int column)
    {
        Debug.Log($"{row}  :  {column}");

        if (CheckBombUnderTile(row, column))
        {
            Debug.Log("GameOver");
        }

        Debug.Log(CheckForAdjacentBombs(row, column));
    }

        //will return out of range exception. 
    int CheckForAdjacentBombs(int row, int column)
    {
        int adjacentBombCounter = 0;

        if (bombGrid[row+1, column])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row-1, column]) 
        {
            adjacentBombCounter++;        
        }
        if (bombGrid[row, column + 1])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row, column - 1])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row+1, column + 1])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row+1, column - 1])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row-1, column + 1])
        {
            adjacentBombCounter++;
        }
        if (bombGrid[row-1, column - 1])
        {
            adjacentBombCounter++;
        }

        return adjacentBombCounter;
    }

    bool CheckBombUnderTile(int row, int column)
    {
        if (bombGrid[row, column])
        {
            return true;
        }

        return false;
    }
}
