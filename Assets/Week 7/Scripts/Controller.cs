using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Slider chargeSlider;
    float charge;
    public float maxCharge = 1;
    Vector2 direction;

    public static float score;
    public TextMeshProUGUI myUiText;

    public static FootballPlayer SelectedPlayer { get; private set; } //variable that holds the currently selected player
    //Belongs to the class controller, rather than an object using this
    //Anyone can access this, but only THIS can set its value
    //Get: Determines whether or not you can get this static variable
    //Set: Determines whether or not you can set this static variable

    //As shown below, you can do this with functions as well! saves a lot of time!
    public static void SetSelectedPlayer(FootballPlayer p)
    {
        if(SelectedPlayer != null) //if there IS a selected player
        {
            SelectedPlayer.Selected(false); //unselect yourself
        }
        //Set the "selected player" to the inputted player
        SelectedPlayer = p;
        //Set the selected player to true for the new inputted player
        SelectedPlayer.Selected(true);
    }
    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            SelectedPlayer.Move(direction);
            direction = Vector2.zero;
            charge = 0;
            chargeSlider.value = charge;
        }
    }
    private void Update()
    {
        myUiText.text = "score: " + score.ToString();
        if (SelectedPlayer == null) return;
        if (Input.GetKeyDown(KeyCode.Space)) {

            charge = 0;
            direction = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            charge += Time.deltaTime;
            charge = Mathf.Clamp(charge, 0, maxCharge);
            chargeSlider.value = charge;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)SelectedPlayer.transform.position).normalized * charge;
        }
    }
}
