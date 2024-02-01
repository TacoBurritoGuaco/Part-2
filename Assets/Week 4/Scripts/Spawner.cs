using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject planePrefab;
    float randTime = Random.Range(1, 5);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randTime -= Time.deltaTime;
        if (randTime <= 0) {
            Instantiate(planePrefab);
            randTime += Random.Range(1, 5);
        }
    }
}
