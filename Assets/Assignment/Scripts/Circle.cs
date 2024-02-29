using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Animator animator;
    public void weaponDefeated()
    {
        animator.SetTrigger("Beaten");
    }
}
