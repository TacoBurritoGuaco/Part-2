using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//small script that handles the menu screen.
//It only purpose is to allow the start button to function as intended
public class MenuManager : MonoBehaviour
{
    //called by the start button, which sends the player to the main game screen
    public void Main()
    {
        SceneManager.LoadScene(1); //load the main game screen
    }
}
