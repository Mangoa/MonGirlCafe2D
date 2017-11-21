using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DishScrollButton : MonoBehaviour {

    public GameObject dishButton;
    public GameObject scrollPane;
	public int dishNumber;

    void Update(){
        changeButtonImage();
    }

    public void changeSelectedDish(){
        dishButton.GetComponent<DishSelector>().setCurrentDish(dishNumber);
        Debug.Log("Dish changed to #" + dishNumber);
        scrollPane.SetActive(false);
        DishManager.control.setMenuChanged(true);
    }

    public void changeButtonImage(){
        gameObject.GetComponent<Image>().sprite = DishManager.dishArray[dishNumber].image;
    }

    public void setDishNumber(int dishNum){
        dishNumber = dishNum;
    }

}
