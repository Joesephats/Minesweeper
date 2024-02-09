using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{

    bool[,] bombGrid = new bool[5, 5];
    GameObject[,] buttonGrid = new GameObject[5, 5];
    [SerializeField] GameObject tile;
    [SerializeField] GameObject adjacentBombUI;
    [SerializeField] GameObject bombUI;
    [SerializeField] Transform canvasTransfrom;

    int tilesChecked = 0;

    bool gameOver = false;

    bool gameWon = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject currentTile = Instantiate(tile, canvasTransfrom, false);

                buttonGrid[i, j] = currentTile;
                currentTile.transform.position = new Vector2((j * 100f) + 350, (i * 100f) + 75);
                Debug.Log($"Tile: ({i}, {j}) at Pos: {currentTile.transform.position.x}, {currentTile.transform.position.y}");
                currentTile.GetComponent<Tile>().row = i;
                currentTile.GetComponent<Tile>().col = j;
                currentTile.GetComponent<Tile>().tilePosition = currentTile.transform.position;

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
        if (gameOver)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (buttonGrid[i, j] != null)
                    {
                        buttonGrid[i, j].GetComponent<Button>().interactable = false;
                    }
                }
            }
        }

        if (tilesChecked == 20)
        {
            gameWon = true;
        }

        if (gameWon)
        {

        }
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
            gameOver = true;
            Debug.Log("GameOver");

            GameObject ui = Instantiate(bombUI, canvasTransfrom, false);
            ui.transform.position = buttonGrid[row, column].GetComponent<Tile>().tilePosition;
        }
        else
        {
            Debug.Log(CheckForAdjacentBombs(row, column));

            GameObject ui = Instantiate(adjacentBombUI, canvasTransfrom, false);
            ui.transform.position = buttonGrid[row, column].GetComponent<Tile>().tilePosition;
            ui.GetComponentInChildren<TMP_Text>().text = $"{CheckForAdjacentBombs(row, column)}";

        }

        buttonGrid[row, column].SetActive(false);
    }

    //will return out of range exception. 
    int CheckForAdjacentBombs(int row, int column)
    {
        int adjacentBombCounter = 0;
        int above = row + 1;
        int below = row - 1;
        int right = column + 1;
        int left = column - 1;

   
        if (above == 5)
        {
            //top right
            if (right == 5)
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, left])
                {
                    adjacentBombCounter++;
                }
            }
            //top left
            else if (left == -1)
            {
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, right])
                {
                    adjacentBombCounter++;
                }
            }
            //top middles
            else
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, right])
                {
                    adjacentBombCounter++;
                }
            }

        }

        else if (below == -1)
        {
            //bottom right
            if (right == 5)
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
            }
            //bottom left
            else if (left == -1)
            {
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
            }
            //bottom middles
            else
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, right])
                {
                    adjacentBombCounter++;
                }
            }

        }

        else if (left == -1)
        {
            //top left
            if (above == 5)
            {
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, right])
                {
                    adjacentBombCounter++;
                }
            }
            //bottom left
            else if (below == -1)
            {
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
            }
            //left middles
            else
            {
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[row, right])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, right])
                {
                    adjacentBombCounter++;
                }
            }
        }

        else if (right == 5)
        {
            //top right
            if (above == 5)
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, left])
                {
                    adjacentBombCounter++;
                }
            }
            //bottom right
            else if (below == -1)
            {
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
            }
            //right middles
            else
            {
                if (bombGrid[below, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[below, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[row, left])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, column])
                {
                    adjacentBombCounter++;
                }
                if (bombGrid[above, left])
                {
                    adjacentBombCounter++;
                }
            }
        }

        else
        {
            if (bombGrid[above, column])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[below, column])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[row, right])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[row, left])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[above, right])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[above, left])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[below, right])
            {
                adjacentBombCounter++;
            }
            if (bombGrid[below, left])
            {
                adjacentBombCounter++;
            }
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
