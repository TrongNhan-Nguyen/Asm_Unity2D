using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField userName;
    public InputField userPassword;
    public Button btnSubmit;
    public Button btnRegister;
    Action<string> _createUserCallback;
    void Start()
    {
        btnSubmit.onClick.AddListener(() =>
        {
            StartCoroutine(login(userName.text, userPassword.text));
           
        });

        btnRegister.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Register");
        });
    }
    public IEnumerator login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/assignment/login.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                toast(www.error);
                clg(www.error);
            }
            else
            {
                UserInfo.UserName = username;
                UserInfo.UserID = www.downloadHandler.text;
                string result = www.downloadHandler.text;
                if(result == "password incorrect")
                {
                    toast(result);
                }else if( result == "Username doesn't exists")
                {
                    toast(result);

                }
                else
                {
                    toast("Login successfully");
                    _createUserCallback = (jsonObjectString) =>
                    {
                        StartCoroutine(CreateUserRoutine(jsonObjectString));
                    };
                    StartCoroutine(GetUser(UserInfo.UserID, _createUserCallback));
                    

                }

            }
        }

    }
    public IEnumerator GetUser(string userID, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/assignment/getuser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                toast(www.error);
                clg(www.error);
            }
            else
            {
                string jsonObject = www.downloadHandler.text;
                callback(jsonObject);

            }
        }
    }
    IEnumerator CreateUserRoutine(string jsonObjectString)
    {
        JSONObject jsonObject = JSON.Parse(jsonObjectString) as JSONObject;
        bool isDone = false;
        Action<string> getUserInfoCallback = (userInfo) =>
        {
            isDone = true;
            jsonObject = JSON.Parse(userInfo) as JSONObject;
        };
        StartCoroutine(GetUser(UserInfo.UserID, getUserInfoCallback));
        yield return new WaitUntil(() => isDone == true);

      
        UserInfo.Level = jsonObject["level"];
        UserInfo.UserName = jsonObject["username"];
        UserInfo.Coins =  jsonObject["coins"];
        SceneManager.LoadScene("MenuScreen");

    }

    private void toast(string s)
    {
        SSTools.ShowMessage(s, SSTools.Position.bottom, SSTools.Time.twoSecond);
    }
    private void clg(string s)
    {
        Debug.Log(s);
    }
}
