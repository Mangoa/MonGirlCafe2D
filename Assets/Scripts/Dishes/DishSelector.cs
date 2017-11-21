using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class DishSelector : MonoBehaviour {

    [SerializeField]private Text buttonText;
    [SerializeField]private GameObject infoPane;
    [SerializeField]private GameObject scrollPane;
    [SerializeField]private Text itemNameText;
    [SerializeField]private Text ingredient1Text;
    [SerializeField]private Text ingredient2Text;
    [SerializeField]private Text qualityText;
    [SerializeField]private Text sellPriceText;
    [SerializeField]private Text descText;
    public int currentDishNumber;
    private bool showingTooltip;

	void Start () {
       infoPane.SetActive(false);
       showingTooltip = false;
	}
	
	void Update () {
        if(showingTooltip && !scrollPane.activeSelf){
            displayTooltip();
        }
        changeButtonImage();
	}

    public bool checkCanBeMade(){
        bool ingredient1Enough, ingredient2Enough;

        ingredient1Enough = checkIngredientEnough(getCurrentDish().resourceType1, getCurrentDish().resourceCost1);
        ingredient2Enough = checkIngredientEnough(getCurrentDish().resourceType2, getCurrentDish().resourceCost2);

        return ingredient1Enough & ingredient2Enough;
    }

    private bool checkIngredientEnough(DishManager.Categories resourceType, int resourceCost){

        if(resourceCost < PlayerControl.control.getResourceFromCategory(resourceType)){
            return true;
        }else return false;
    }

    public void orderDish(){
        if(checkCanBeMade()){
            PlayerControl.control.changeResourceAmount(getCurrentDish().resourceType1, -getCurrentDish().resourceCost1);
            PlayerControl.control.changeResourceAmount(getCurrentDish().resourceType2, -getCurrentDish().resourceCost2);
            PlayerControl.control.changeMoney(getCurrentDish().sellPrice);
            //return getCurrentDish();
        }//else return null;
    }

    public void OnMouseEnter(){
        //Debug.Log("Working!");
        //displayTooltip();
        if(!scrollPane.activeSelf){
            infoPane.SetActive(true);
        }
        triggerTooltipDisplay();
    }

    public void OnMouseExit(){
        infoPane.SetActive(false);
        triggerTooltipDisplay();
    }

    public void triggerTooltipDisplay(){
        showingTooltip = !showingTooltip;
    }

    public void displayScrollPane(){
        scrollPane.SetActive(true);
    }

    public void displayTooltip(){
        
        itemNameText.text = getItemNameText();
        ingredient1Text.text = getIngredient1Text();
        ingredient2Text.text = getIngredient2Text();
        qualityText.text = getQualityText();
        sellPriceText.text = getSellPriceText();
        descText.text = getDescriptionText();

        followMouse();

    }

    private void followMouse(){
        float newX, newY;
        newX = Input.mousePosition.x + 90;
        newY = Input.mousePosition.y - 60;

        infoPane.transform.position = new Vector3(newX, newY, 0);
    }

    private string getItemNameText(){
        return DishManager.dishArray[currentDishNumber].name;
    }

    private string getIngredient1Text(){
        string returnString = "";

        switch(getCurrentDish().resourceType1){
            case DishManager.Categories.Fish: 
                returnString += "Fish (";
                break;

            case DishManager.Categories.Veggie: 
                returnString += "Veggie (";
                break;
        }

        returnString += getCurrentDish().resourceCost1 + ")";

        return returnString;
    }

    private string getIngredient2Text(){
        string returnString = "";

        if (getCurrentDish().resourceCost2 > 0){
            switch(getCurrentDish().resourceType2){
            case DishManager.Categories.Fish: 
                returnString += "Fish (";
                break;

            case DishManager.Categories.Veggie: 
                returnString += "Veggie (";
                break;
            }

            returnString += getCurrentDish().resourceCost2 + ")";
        }

        return returnString;
    }

    private string getQualityText(){
        string returnString = "(";

        for(int i = 0; i < getCurrentDish().resourceCost1; i++){
            returnString += "★";
        }

        returnString += ")";

        return returnString;

    }

    private string getSellPriceText(){
        string returnString = "Sells for $";

        returnString += getCurrentDish().sellPrice;

        return returnString;
    }

    private string getDescriptionText(){
        return getCurrentDish().descriptionText;
    }

    public DishManager.Dish getCurrentDish(){
        return DishManager.dishArray[currentDishNumber];
    }

    public void setCurrentDish(int currentDishNum){
        currentDishNumber = currentDishNum;
    }

    public void changeButtonImage(){
        gameObject.GetComponent<Image>().sprite = DishManager.dishArray[currentDishNumber].image;
    }
}
