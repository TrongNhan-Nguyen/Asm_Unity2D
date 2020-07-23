using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{

    public void playGame()
    {
        string level = UserInfo.Level;
        if(level == "1")
        {
            SceneManager.LoadScene("Screen1");

        }if(level == "2")
        {
            SceneManager.LoadScene("Screen2");

        }
        if (level == "3")
        {
            SceneManager.LoadScene("Screen3");

        }
    }
}
