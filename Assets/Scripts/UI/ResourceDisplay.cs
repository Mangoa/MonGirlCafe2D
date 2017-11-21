using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceDisplay : MonoBehaviour {

    private PlayerControl.PlayerData data;
    public int resourceType;
    private string objectText;

	void Start () {
	   data = PlayerControl.control.getPlayerData();
       objectText = gameObject.GetComponent<Text>().text;
	}

    void Update(){
        switch(resourceType){
            case 0: objectText = data.money.ToString();
            break;

            case 1: objectText = data.fish.ToString();
            break;

            case 2: objectText = data.veggies.ToString();
            break;
        }

        gameObject.GetComponent<Text>().text = objectText;
    }
}
