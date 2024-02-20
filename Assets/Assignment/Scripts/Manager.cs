using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float score;
    public float time;

    private void Update()
    {
        time += 1 * Time.deltaTime; //updates the time based on real time
    }

    public void scoreUpdate(float scoreValue)
    {
        score += scoreValue;
    }
}
