using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenWin : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("Screen1");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
