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
using UnityEngine.SceneManagement;

public class MenuPlayButton : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Minesweeper");
    }
}
