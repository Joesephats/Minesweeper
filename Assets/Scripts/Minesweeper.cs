//////////////////////////////////////////////
//Assignment/Lab/Project: Minesweeper
//Name: Tristin Gatt
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/09/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    //stores location of bombs
    bool[,] bombGrid = new bool[5, 5];
    //array of tile buttons
    GameObject[,] buttonGrid = new GameObject[5, 5];


    [SerializeField] GameObject tile;   //tile prefab
    [SerializeField] GameObject adjacentBombUI; //prefab with text comp that displays # of adjacent bombs
    [SerializeField] GameObject bombUI; //bomb image
    [SerializeField] Transform canvasTransfrom; //transform of canvas used to set parent for instantiated tiles

    //ui variables
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TMP_Text gameEndTitle;
    [SerializeField] TMP_Text tileCounter;

    //how many safe tiles have been clicked
    int tilesChecked = 0;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //instantiates grid of tile prefabs and buttonGrid array
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //instantiates tile and adds it to buttonGrid array
                GameObject currentTile = Instantiate(tile, canvasTransfrom, false);
                buttonGrid[i, j] = currentTile;

                //sets position of object based on index and screenspace values that seemed good.
                //these values are optimized for the final build window. 
                currentTile.transform.position = new Vector2((j * 180f) + 650, (i * 180f) + 200);

                Debug.Log($"Tile: ({i}, {j}) at Pos: {currentTile.transform.position.x}, {currentTile.transform.position.y}");

                //see Tile.cs
                currentTile.GetComponent<Tile>().row = i;
                currentTile.GetComponent<Tile>().col = j;
                currentTile.GetComponent<Tile>().tilePosition = currentTile.transform.position;

                //adds the TileClicked method to the tile button component with it's index as parameters 
                currentTile.GetComponent<Button>().onClick.AddListener(() => TileClicked(currentTile.GetComponent<Tile>().row, currentTile.GetComponent<Tile>().col));
            }
        }

        
        InitializeBoolArray(bombGrid);

        Debug.Log("-----------------------------------------------------------------------------");

        //randomly picks positions in bombGrid array to place mines
        PlaceMines(bombGrid);
    }

    // Update is called once per frame
    void Update()
    {
        //checks if game lost
        if (gameOver)
        {
            //disables all tiles
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

            //displays end game screen
            gameEndPanel.SetActive(true);
            gameEndTitle.text = "You Lose";

        }

        //check if game won
        if (tilesChecked == 20)
        {
            //displays game end screen
            gameEndPanel.SetActive(true);
            gameEndTitle.text = "You Win";
        }
    }

    void InitializeBoolArray(bool[,] array)
    {
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                array[i, j] = false;
            }
        }
    }

    //randomly places 5 mines. spots with mines hold true value
    void PlaceMines(bool[,] array)
    {
        for (int i = 0; i < 5; i++)
        {
            //randomly picks a row and column to place mine, repeats if that position already has a mine.
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

    //when a tile is clicked
    public void TileClicked(int row, int column)
    {
        Debug.Log($"{row}  :  {column}");

        //checks if the tile is on top of a mine
        if (CheckBombUnderTile(row, column))
        {
            gameOver = true;
            Debug.Log("GameOver");

            //intantiates mine image
            GameObject ui = Instantiate(bombUI, canvasTransfrom, false);
            ui.transform.position = buttonGrid[row, column].GetComponent<Tile>().tilePosition;
        }
        else  //the tile is safe
        {
            Debug.Log(CheckForAdjacentBombs(row, column));

            //instantiates adjacent bomb ui and uses CheckForAdjacentBombs to set text
            GameObject ui = Instantiate(adjacentBombUI, canvasTransfrom, false);
            ui.transform.position = buttonGrid[row, column].GetComponent<Tile>().tilePosition;
            ui.GetComponentInChildren<TMP_Text>().text = $"{CheckForAdjacentBombs(row, column)}";

            //increments tiles checked and updates tile counter ui
            tilesChecked++;
            tileCounter.text = $"{tilesChecked}/{20}";
        }

        //disables the clicked button
        buttonGrid[row, column].SetActive(false);
    }

    //really tried to make this more elegent with nested for loops but couldn't quite get the logic right.
    int CheckForAdjacentBombs(int row, int column)
    {
        
        int adjacentBombCounter = 0;
        int above = row + 1;
        int below = row - 1;
        int right = column + 1;
        int left = column - 1;

        //top row
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

        //bottom row
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

        //leftmost column
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

        //rightmost column
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

    //checks for mines.
    bool CheckBombUnderTile(int row, int column)
    {
        if (bombGrid[row, column])
        {
            return true;
        }

        return false;
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Minesweeper");
    }
}
