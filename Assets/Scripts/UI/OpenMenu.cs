using UnityEngine;
using System.Collections;

public class OpenMenu : MonoBehaviour {

	[SerializeField] private GameObject menuPanel;

    public void toggleMenuVisible(){
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
