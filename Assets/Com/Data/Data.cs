using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class Data
{
    public Player GetPlayerdata()
    {
        string json;

        if (PlayerPrefs.GetString("data") != "")
        {

            json = PlayerPrefs.GetString("data");
        }
        else
        {
            TextAsset file = Resources.Load("config") as TextAsset;
            json = file.text;
        }

        Player player = JsonConvert.DeserializeObject<Player>(json);//convert json text as object list
        return player;

    }

    //Updating json data
    public void SetData(Player player)
    {
        
        string data = JsonConvert.SerializeObject(player);
        PlayerPrefs.SetString("data", data);
    }
}
