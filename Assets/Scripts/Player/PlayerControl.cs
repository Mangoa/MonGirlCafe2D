using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerControl : MonoBehaviour {

    public static PlayerControl control;
    private static PlayerData playerData;
    private static string accountNumber;
    private static MainMenuLogin.AccountData accountData;
    private string playerName;

    void Awake(){
        if(control == null){
            DontDestroyOnLoad(gameObject);
            control = this;
            //playerData = new PlayerData();
        }
        else if (control!= this){
            Destroy(gameObject);
        }
            playerData = new PlayerData();
    }

    public void loadProfile(string num, PlayerData pd, MainMenuLogin.AccountData ad){
        playerData.loadPlayerData(pd);
        accountData = ad;
        setPlayerAccountNumber(num);
        Debug.Log("Set player account number to " + num);
    }

/*
    void OnGUI(){
        GUI.Label(new Rect(10, 10, 150, 30), "Fish: " + playerData.fish);
        GUI.Label(new Rect(10, 40, 150, 30), "Veggies: " + playerData.fish); 
        GUI.Label(new Rect(10, 70, 150, 30), "Money: " + playerData.money); 

    }
    */

    public void setPlayerAccountNumber(string num){
        accountNumber = num;
    }

    public void setPlayerName(string name){
        playerName = name;
    }

    public PlayerData getPlayerData(){
        return playerData;
    }

    public void savePlayerData(){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/accounts/" + accountNumber + ".dat");
        MainMenuLogin.AccountData data = accountData;
        data._data = playerData;

        formatter.Serialize(file, data);
        Debug.Log("Account saved at " + Application.persistentDataPath + "/accounts/" + accountNumber + ".dat");

        file.Close();
    }

    public void changeResourceAmount(DishManager.Categories category, int amount){
        switch(category){
            case DishManager.Categories.Fish:
                playerData.fish += amount;
                break;
            case DishManager.Categories.Veggie:
                playerData.veggies += amount;
                break;
            case DishManager.Categories.Treasure:
                playerData.money += amount;
                break;
        }
    }

    public void changeMoney(int amount){
        playerData.money += amount;
    }

    public int getResourceFromCategory(DishManager.Categories category){
        switch(category){
            case DishManager.Categories.Fish:
                return getPlayerData().fish;
            case DishManager.Categories.Veggie:
                return getPlayerData().veggies;  
            case DishManager.Categories.Treasure:
                return getPlayerData().money;         
        }
        return 0;
    }

[Serializable]
	public class PlayerData{
        public int fish,
        veggies;

        public int money;

        public PlayerData(){
            fish = 10;
            veggies = 10;

            money = 2000;
        }

        public PlayerData(bool isAdmin){
            if(isAdmin){
                fish = 999;
                veggies = 999;
                money = 99999;
            }
            else{
                fish = 0;
                veggies = 0;
                money = 0;
            }
        }

        public void loadPlayerData(PlayerData oldData){
            fish = oldData.fish;
            veggies = oldData.veggies;
            money = oldData.money;
        }
    }
}
