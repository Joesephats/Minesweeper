using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{

    bool[,] grid = new bool[5, 5];
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoolArray(grid);

        Debug.Log("-----------------------------------------------------------------------------");

        PlaceMines(grid);
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
            int randomColumn = Random.Range(0, 5);
            array[i, randomColumn] = true;

            Debug.Log($"Mine at Index [{i},{randomColumn}]");
        }

    }

    bool CheckTile(int row, int column)
    {
        if (grid[row, column])
        {
            return true;
        }

        return false;
    }
}
