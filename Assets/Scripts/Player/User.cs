using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class User : MonoBehaviour
{

    void Start()
    {
        CreateUser();

    }

    public void CreateUser()
    {
        StartCoroutine(GetAllUser());
    }
    public IEnumerator GetAllUser()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/assignment/get_all_user.php"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                clg(www.error);
            }
            else
            {
                string jsonResult = www.downloadHandler.text;
                JSONArray jsonArray = JSON.Parse(jsonResult) as JSONArray;
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    GameObject item = Instantiate(Resources.Load("Prefabs/itemUser") as GameObject);
                    item.transform.SetParent(this.transform);
                    item.transform.localScale = Vector3.one;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.Find("Name").GetComponent<Text>().text = jsonArray[i].AsObject["username"];
                    item.transform.Find("Password").GetComponent<Text>().text = jsonArray[i].AsObject["password"];
                    item.transform.Find("Coins").GetComponent<Text>().text = jsonArray[i].AsObject["coins"];
                }
            }
        }
    }
    private void clg(string s)
    {
        Debug.Log(s);
    }

}
