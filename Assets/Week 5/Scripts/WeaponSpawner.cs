using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject macePrefab;

    public void SpawnMace()
    {
        Instantiate(macePrefab);
    }
}
