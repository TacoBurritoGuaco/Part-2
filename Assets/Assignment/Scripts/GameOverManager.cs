using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//This script handles all the game over screen related shannanigans, such as setting the text for final score and final time as well as handling buttons
public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreLabel; //The textMesh used in the final score UI
    public TextMeshProUGUI finalTimeLabel; //The textMesh used in the final time UI

    //start sets the finalScoreLabel text as well as the finalTimeLabel text to the previously saved playerPrefs
    private void Start()
    {
        finalTimeLabel.text = "Final Time: " + PlayerPrefs.GetFloat("finalTime"); //sets the final time from playerprefs as text on the game over screen
        finalScoreLabel.text = "Final Score: " + PlayerPrefs.GetFloat("scoreTotal"); //sets the final score from playerprefs as text on the game over screen
    }

    //called by the menu button, which sends the player to the menu screen
    public void Menu()
    {
        SceneManager.LoadScene(0); //load the menu screen
    }
    //called by the restart button, restarting the game
    public void Restart()
    {
        SceneManager.LoadScene(1); //load the menu screen
    }
}
