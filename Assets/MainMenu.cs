using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Initiate unity scenemanagement
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public void singleplayer
    public void SinglePlayer()
    {
        // Load scene 2
        SceneManager.LoadScene(2);
    }

    // public void multiplayer
    public void MultiPlayer()
    {
        // Load scene 5
        SceneManager.LoadScene(5);
    }

    // public void quit
    public void Quit()
    {
        // Quit game
        Application.Quit();
    }

}
