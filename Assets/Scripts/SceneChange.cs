using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void OnPlayButtonClicked(int levelBeingLoaded)
    {
        // Load the level selection scene
        SceneManager.LoadScene(levelBeingLoaded);
    }
}
