using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserList : MonoBehaviour
{
    public Button btnLogin;
    public Button btnRegister;

    void Start()
    {
        btnRegister.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Register");
        });
        btnLogin.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Login");
        });
    }
}
