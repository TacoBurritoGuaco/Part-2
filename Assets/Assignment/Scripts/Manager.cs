using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//This class is used as a manager of the score and time values found in the assignment program
//These values both constantly update, but also get used in the 'game over' scene.
public class Manager : MonoBehaviour
{
    public float score; //the player's current score
    public float time; //the player's current time
    public Slider slider; //The slider correspondant with the health bar
    public TextMeshProUGUI scoreLabel; //The textMesh used in the score UI
    public TextMeshProUGUI timeLabel; //The textMesh used in the time UI

    //update is used to continously update the time every second (also done using times.deltatime)
    private void Update()
    {
        time += 1 * Time.deltaTime; //updates the time based on real time
        timeLabel.text = "Time: " + (int)time; //updates time in the UI every time it is increased
        //Time is turned into an integer so that it may be easier to see for the user (and less obnoxious)
    }
    //Function that increases the score using an input value from the enemies when they are defeated
    public void scoreUpdate(float scoreValue)
    {
        score += scoreValue; //update score value 
        scoreLabel.text = "Score: " + score; //updates the score in the UI every time it is increased
    }

    //this function will be called whenever the player reaches 0 hitpoints using sendMessage
    //In this script, this function will save both the current score and time values into playerPrefs to be used in the next scene
    //(also known as the "game over" screen
    public void deathUpdate()
    {
        PlayerPrefs.SetFloat("scoreTotal", score); //saves the score value to a PlayerPrefs 
        //This way, the score value can be pulled in the end screen as the player's total score.
        PlayerPrefs.SetFloat("finalTime", time); //saves the current time to playerPrefs
        //This way, the time value can be pulled in the end screen and displayed as the player's final time
    }
    

    //function called using sendmessage whenever the knight takes damage and updates the slider's size
    public void TakeDamage(float damage)
    {
        slider.value -= damage; //lowers the slider
    }
    //function that sets the slider's value when called
    public void savedValue(float value)
    {
        slider.value = value; //sets the slider value to the value input
    }
}
