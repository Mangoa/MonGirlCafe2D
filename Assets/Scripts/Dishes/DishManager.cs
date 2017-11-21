using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class DishManager : MonoBehaviour {

    public static DishManager control;
    
    [SerializeField]public Sprite[] foodSprites;
    private List<DishManager.Dish> menuItems;
    public static List<Dish> dishArray;
    public enum Categories {Fish, Veggie, Treasure, Essence, Bird, Fruit, Insect, None}
    public int numberOfDishes;
    private bool menuChanged = true;

    void Awake(){
        control = this;
        setupDishArray();
        initMenuItems();
    }

    void Update(){
        if(menuChanged){
            initMenuItems();
        }
    }

//--Category(Main)--|--Category(Sub)--|--Type
// 00####           | ##00##          | Fish
// 01####           | ##01##          | Veggie
// 02####           | ##02##          | Treasure
// 03####           | ##03##          | Essence
// 04####           | ##04##          | Bird
// 05####           | ##05##          | Fruit
// 06####           | ##06##          | Insect

    private void setupDishArray(){
        dishArray = new List<Dish>();
        Debug.Log("Setting up Dish Array!");

        dishArray.Add(new Dish());
        dishArray[0].name = "Tuna Nigiri";
        dishArray[0].descriptionText = "Delicious raw fish on rice.";
        dishArray[0].resourceType1 = Categories.Fish;
        dishArray[0].resourceCost1 = 4;
        dishArray[0].resourceType2 = Categories.Fish;
        dishArray[0].resourceCost2 = 0;
        dishArray[0].quality = 3;
        dishArray[0].sellPrice = 80;
        dishArray[0].image = foodSprites[0];
        numberOfDishes++;
        //Debug.Log("Dish 1 set up!");

        dishArray.Add(new Dish());
        dishArray[1].name = "Paella";
        dishArray[1].descriptionText = "Placeholder.";
        dishArray[1].resourceType1 = Categories.Fish;
        dishArray[1].resourceCost1 = 3;
        dishArray[1].resourceType2 = Categories.Fish;
        dishArray[1].resourceCost2 = 0;
        dishArray[1].quality = 2;
        dishArray[1].sellPrice = 60;
        dishArray[1].image = foodSprites[1];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[2].name = "Fish Curry";
        dishArray[2].descriptionText = "You're not sure if those lines are from the heat or the smell.";
        dishArray[2].resourceType1 = Categories.Fish;
        dishArray[2].resourceCost1 = 2;
        dishArray[2].resourceType2 = Categories.Veggie;
        dishArray[2].resourceCost2 = 1;
        dishArray[2].quality = 2;
        dishArray[2].sellPrice = 40;
        dishArray[2].image = foodSprites[2];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[3].name = "Veggie Curry";
        dishArray[3].descriptionText = "Simple but effective.";
        dishArray[3].resourceType1 = Categories.Veggie;
        dishArray[3].resourceCost1 = 2;
        dishArray[3].resourceType2 = Categories.Veggie;
        dishArray[3].resourceCost2 = 0;
        dishArray[3].quality = 2;
        dishArray[3].sellPrice = 40;
        dishArray[3].image = foodSprites[3];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[4].name = "Salad";
        dishArray[4].descriptionText = "Pretty standard leafy greens.";
        dishArray[4].resourceType1 = Categories.Veggie;
        dishArray[4].resourceCost1 = 2;
        dishArray[4].resourceType2 = Categories.Veggie;
        dishArray[4].resourceCost2 = 0;
        dishArray[4].quality = 1;
        dishArray[4].sellPrice = 30;
        dishArray[4].image = foodSprites[4];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[5].name = "Fish Taco";
        dishArray[5].descriptionText = "You hear they come from some distand land in the West.";
        dishArray[5].resourceType1 = Categories.Fish;
        dishArray[5].resourceCost1 = 2;
        dishArray[5].resourceType2 = Categories.Veggie;
        dishArray[5].resourceCost2 = 1;
        dishArray[5].quality = 1;
        dishArray[5].sellPrice = 30;
        dishArray[5].image = foodSprites[5];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[6].name = "Gold Sack";
        dishArray[6].descriptionText = "You decided to try to sell money.";
        dishArray[6].resourceType1 = Categories.Treasure;
        dishArray[6].resourceCost1 = 35;
        dishArray[6].resourceType2 = Categories.None;
        dishArray[6].resourceCost2 = 0;
        dishArray[6].quality = 2;
        dishArray[6].sellPrice = 50;
        dishArray[6].image = foodSprites[6];
        numberOfDishes++;

        dishArray.Add(new Dish());
        dishArray[numberOfDishes].name = "Treasure Chest";
        dishArray[numberOfDishes].descriptionText = "Fresh from the dungeon!";
        dishArray[numberOfDishes].resourceType1 = Categories.Treasure;
        dishArray[numberOfDishes].resourceCost1 = 90;
        dishArray[numberOfDishes].resourceType2 = Categories.None;
        dishArray[numberOfDishes].resourceCost2 = 0;
        dishArray[numberOfDishes].quality = 3;
        dishArray[numberOfDishes].sellPrice = 100;
        dishArray[numberOfDishes].image = foodSprites[numberOfDishes];
        numberOfDishes++;

        Debug.Log("Dish array set up!");
    }

    private void initMenuItems(){
        menuItems = new List<DishManager.Dish>();
        foreach(GameObject menuItem in GameObject.FindGameObjectsWithTag("DishButton")){
            menuItems.Add(menuItem.GetComponent<DishSelector>().getCurrentDish());
        }
        menuChanged = false;
    }

    public void setMenuChanged(bool changed){
        menuChanged = changed;
    }

    public List<DishManager.Dish> getMenuItems(){
        return menuItems;
    }

	public class Dish{
        public int sellPrice,
        resourceCost1,
        resourceCost2,
        quality;

        public Categories resourceType1,
        resourceType2;

        public string name,
        descriptionText;

        public Sprite image;

        /*
        public Dish(int ID){
            getDishFromID(ID);
        }

        private void getDishFromID(int ID){
            int category1,
            category2,
            dishNum;

            category1 = ID / 10000;
            category2 = (ID % 10000) / 100;
            dishNum = ID % 100;
        }

        private void getDishDetails(int resource1, int resource2, int dishNum){

        }
        */

        private void getTestDish(){
            resourceType1 = Categories.Fish;
            resourceType2 = Categories.Veggie;
            sellPrice = 30;
            resourceCost1 = 3;
            resourceCost2 = 2;
            quality = 1;

            descriptionText = "This is a test dish! Yum?";

            //image = foodSprites[0];
        }

        public void orderDish(){
            PlayerControl.control.changeResourceAmount(resourceType1, -resourceCost1);
            PlayerControl.control.changeResourceAmount(resourceType2, -resourceCost2);
            PlayerControl.control.changeMoney(sellPrice);
        }

        public bool canBeMade(){
            bool ingredient1Enough, ingredient2Enough;

            ingredient1Enough = checkIngredientEnough(resourceType1, resourceCost1);
            ingredient2Enough = checkIngredientEnough(resourceType2, resourceCost2);

            return ingredient1Enough & ingredient2Enough;
        }

        private bool checkIngredientEnough(DishManager.Categories resourceType, int resourceCost){
            if(resourceCost <= PlayerControl.control.getResourceFromCategory(resourceType)){
                return true;
            }else return false;
    }

    }
}
