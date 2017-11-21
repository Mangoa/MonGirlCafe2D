using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class MainMenuLogin : MonoBehaviour {

[SerializeField]GameObject accountInput;
[SerializeField]GameObject passwordInput;

private string accountNumber, password;

    void Awake(){
        createDirectory();
        //createTestAccount();
    }

	public void login(){
        accountNumber = accountInput.GetComponent<Text>().text.Trim();
        password = passwordInput.GetComponent<Text>().text.Trim();

        if(isAccountNumberEntered() && isPasswordValid()){
            if(checkAccountExists()){
                if(checkExistingAccountPassword()){
                    loginExistingAccount();
                    goToGameScene();
                }
                else{
                    displayError("Password incorrect!");
                }
            }
            else{
                createNewAccount();
                goToGameScene();
            }

        }
        else{
            displayError("Username or Password not entered!");
        }
    }

    bool isAccountNumberEntered(){
        if(accountNumber != ""){return true;}
        else return false;
    }

    bool isPasswordValid(){
        if(password != ""){return true;}
        else return false;
    }

    void displayError(string errorText){
        Debug.Log(errorText);
    }

    bool checkAccountExists(){
        if(File.Exists(Application.persistentDataPath + "/accounts/" + accountNumber + ".dat")){
            return true;
        }
        else return false;
    }

    void createDirectory(){
        Directory.CreateDirectory(Application.persistentDataPath + "/accounts/");
    }

    void createTestAccount(){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/accounts/test.dat");
        AccountData data = new AccountData();

        data._accountNumber = "test";
        data._password = "1234";
        data._data = new PlayerControl.PlayerData(true);

        formatter.Serialize(file, data);
        Debug.Log("Test account created at " + Application.persistentDataPath + "/accounts/test.dat");

        file.Close();       
    }

    void createNewAccount(){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/accounts/" + accountNumber + ".dat");
        AccountData data = new AccountData();

        data._accountNumber = accountNumber;
        data._password = password;
        data._data = new PlayerControl.PlayerData();

        formatter.Serialize(file, data);
        Debug.Log("Account created at " + Application.persistentDataPath + "/accounts/" + accountNumber + ".dat");

        file.Close();
    }

    void loginExistingAccount(){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/accounts/" + accountNumber + ".dat", FileMode.Open);
        AccountData data = (AccountData)formatter.Deserialize(file);

        file.Close();
        Debug.Log("Account loaded from " + Application.persistentDataPath + "/accounts/" + accountNumber + ".dat");

        PlayerControl.control.loadProfile(accountNumber, data._data, data);
    }

    bool checkExistingAccountPassword(){
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/accounts/" + accountNumber + ".dat", FileMode.Open);
        AccountData data = (AccountData)formatter.Deserialize(file);

        file.Close();

        if(data._password == password){
            return true;
        }
        else return false;
    }

    void goToGameScene(){
        //Application.LoadLevel("Test Scene");
        SceneManager.LoadScene("Test Scene");
    }

    [Serializable]
    public class AccountData{
        public string _accountNumber;
        public string _password;

        public PlayerControl.PlayerData _data;
    }
}
