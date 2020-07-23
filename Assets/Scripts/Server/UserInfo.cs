using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    public static string UserID;
    public static string UserName;
    public static string Level;
    public static string Coins;
    public static int maxHealth =8;
    public static int dealDamage =1;
    public static float speed = 4f;
    public static List<string> itemList = new List<string>();
    public Text nameUser;
    public Text leveUser;
    public Text coinsUser;
    Action<string> _createItemsCallback;
    public GameObject ItemsInventory;
    public GameObject Inventory;
    private void Start()
    {
        Inventory.SetActive(false);
        nameUser.text = "Name: " + UserName;
        leveUser.text = "Level: " + Level;
        coinsUser.text = "Coins: " + Coins ;
    }
    private void Update()
    {
        coinsUser.text = "Coins: " + (Int16.Parse(Coins) + Enemy.coins);
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (ItemsInventory.activeSelf == false)
            {
                clg("i");
                ItemsInventory.SetActive(true);
                Inventory.SetActive(true);
                ItemsInventory.GetComponent<Inventory>().enabled = true;

            }
            else
            {
                Inventory.SetActive(false);
                ItemsInventory.SetActive(false);
                ItemsInventory.GetComponent<Inventory>().enabled = false;
            }
        }
    }
   
    public void CreateItems()
    {
        //StartCoroutine(Main.Instance.Server.GetUserItems(UserInfo.UserID,_createItemsCallback));
        StartCoroutine(GetUserItems("3", _createItemsCallback));

    }

    private void OnEnable()
    {
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };
        CreateItems();

    }
    public IEnumerator GetUserItems(string userID, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/assignment/getuser_items.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                toast(www.error);
                clg(www.error);
            }
            else
            {
                string jsonArray = www.downloadHandler.text;
                callback(jsonArray);

            }
        }
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        itemList.Clear();
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string itemID = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };
            StartCoroutine(GetItem(itemID, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);
            itemList.Add(itemInfoJson["name"]);
        }
        foreach(string item in itemList)
        {
            if(item == "Boot")
            {
                speed = 5f;
            }if(item == "Shield")
            {
                maxHealth = 12;
            }if(item == "Sword")
            {
                dealDamage = 2;
            }
        }
        jsonArray = new JSONArray();
    }
    public IEnumerator GetItem(string itemID, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/assignment/getitem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                toast(www.error);
                clg(www.error);
            }
            else
            {
                string jsonArray = www.downloadHandler.text;
                callback(jsonArray);

            }
        }
    }
   
    private void toast(string s)
    {
        SSTools.ShowMessage(s, SSTools.Position.bottom, SSTools.Time.twoSecond);
    }
    private void clg (string s)
    {
        Debug.Log(s);
    }
}
