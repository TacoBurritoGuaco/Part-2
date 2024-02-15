using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSIndex = (currentSIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSIndex);
    }

}
