//////////////////////////////////////////////
//Assignment/Lab/Project: Minesweeper
//Name: Tristin Gatt
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/09/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//holds information about each instantiated tile
public class Tile : MonoBehaviour
{
    //stores tile's location for checking if on a mine and number of adjacent mines
    public int row;
    public int col;

    //stores tile's position to instantiate mine image or adjacent mines text
    public Vector2 tilePosition;
}
