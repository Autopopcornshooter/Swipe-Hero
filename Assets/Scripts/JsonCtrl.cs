using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class JsonCtrl : MonoBehaviour
{
   
    private static string path="";

    private static JsonCtrl instance;

    private void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + "SaveData";
    }
    private void Start()
    {
        
    }
    public static JsonCtrl Instance()
    {
        return instance;
    }
    public void SaveData()
    {
        GameData package = GameInfo.gamedata;
        string data=JsonUtility.ToJson(package);
        File.WriteAllText(path, data);
    }
    public void LoadData()
    {
        if (!File.Exists(path))
        {
            SaveData();
            return;
        }
        string data= File.ReadAllText(path);
        if (data != null) {
            GameInfo.gamedata=JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            Debug.Log("NULL DATA");

        }
    }
    public void ResetData()
    {
        GameInfo.gamedata = new GameData();
        SaveData();
    }
}
