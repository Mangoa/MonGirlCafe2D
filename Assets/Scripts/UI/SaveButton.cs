using UnityEngine;
using System.Collections;

public class SaveButton : MonoBehaviour {

    public void OnClick(){
        PlayerControl.control.savePlayerData();
    }
}
