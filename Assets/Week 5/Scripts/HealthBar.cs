using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Does not conflict because it is from a different class
    public void TakeDamage(float damage)
    {
        slider.value -= damage;
    }
}
