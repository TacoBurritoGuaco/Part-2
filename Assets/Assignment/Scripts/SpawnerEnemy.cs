using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns the hammer and the axe on intervals based on frames
public class SpawnerEnemy : MonoBehaviour
{
    public GameObject hammer; //the hammer prefab
    public GameObject axe; //the axe prefab
    float frames; //the frames that increase every frame (I am so tired : ( 

    private void FixedUpdate()
    {
        //Frames increase by 1 every frame
        frames += 1;

        //Whenever half a second passes
        if (frames % 60 == 0)
        {
            Instantiate(hammer); //instantiates a hammer at t (spawn position)
        }
        //Whenever 1 second passes
        if (frames % 90 == 0)
        {
            Instantiate(axe); //instantiates a hammer at t (spawn position)
        }

    }
}
