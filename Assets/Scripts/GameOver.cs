using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    string currentScrren;
    
    void Start()
    {
        currentScrren = Movement.currentScreen;

    }

    public void restart()
    {
        switch (currentScrren)
        {
            case "Screen1":
                SceneManager.LoadScene("Screen1");
                break;
            case "Screen2":
                SceneManager.LoadScene("Screen2");
                break;
            case "Screen3":
                SceneManager.LoadScene("Screen3");
                break;

        }
    }
    public void exitGame()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
