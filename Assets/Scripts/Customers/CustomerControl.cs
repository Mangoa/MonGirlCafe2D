using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerControl : MonoBehaviour {

    private string monsterType;
	private DishManager.Categories preferredFoodType;
    private DishManager.Categories dislikedFoodType;
    private int qualityThreshold;
    private int fullness;
    private int fullnessThreshold;
    private int spendingLimit;
    private int amountSpent;
    private float eatSpeed;
    private int satisfactionLevel;

    private GameObject entrance;
    private GameObject assignedSeat;
    private float walkSpeed = 3.0f;
    private bool isSitting = false;

    [SerializeField]GameObject foodHolder;
    private DishManager.Dish food;
    private bool wantsToStay = true;
    private bool readyToOrder = true;


    //[SerializeField]private GameObject testSeat;

    void Start(){
        setEntrance();
        Debug.Log("A customer has come!");
        //setAssignedSeat(testSeat);
        //setTestPreferences();
    }

    void Update(){
        if(!isSitting && wantsToStay){
            goToSeat();
        }
        else if(isSitting && wantsToStay && readyToOrder && checkMenu() != null){
            orderDish();
        }
        else if(!wantsToStay){
            leaveCafe();
        }
    }

    private void setTestPreferences(){
        monsterType = "Jellyfish";
        preferredFoodType = DishManager.Categories.Fish;
        dislikedFoodType = DishManager.Categories.Veggie;
        qualityThreshold = 1;
        fullness = 0;
        fullnessThreshold = 10;
        amountSpent = 0;
        spendingLimit = 200;
        eatSpeed = 3.0f;
        satisfactionLevel = 0;
    }

    public void goToSeat(){
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, 
                                                            assignedSeat.transform.position,   
                                                            walkSpeed * Time.deltaTime);
        if(gameObject.transform.position == assignedSeat.transform.position){
            isSitting = true;
        }
        //Debug.Log("Moving to seat!");
    }

    public void leaveCafe(){
        if(gameObject.transform.position == assignedSeat.transform.position){
            assignedSeat.GetComponent<SeatNode>().setOccupied(false);
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, 
                                                            entrance.transform.position, 
                                                            walkSpeed * Time.deltaTime);
        if(gameObject.transform.position == entrance.transform.position){
            Debug.Log("A customer has left!");
            Destroy(gameObject);
        }
    }

    public void setAssignedSeat(GameObject assignedSeat){
        this.assignedSeat = assignedSeat;
    }

    public void setEntrance(){
        entrance = GameObject.FindWithTag("Entrance");
        gameObject.transform.position = entrance.transform.position;
    }

    public void orderDish(){
        food = checkMenu();

        if(!food.canBeMade()){
            wantsToStay = false;
            Debug.Log("The kitchen is out of ingredients for the food they want!");
        }
        else{
            Debug.Log("Food ordered!");
            readyToOrder = false;
            food.orderDish();
            setFoodSprite();
            StartCoroutine(eatFood()); 
        }

    }

    IEnumerator eatFood(){
        yield return new WaitForSeconds(eatSpeed);
        amountSpent += food.sellPrice;
        calculateSatisfaction();
        fullness += 5;
        readyToOrder = true;
        wantsToStay = checkWantsToStay();
        food = null;
        //setFoodSprite();
        foodHolder.GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(2.0f);
    }

    private void setFoodSprite(){
        foodHolder.GetComponent<SpriteRenderer>().sprite
            = food.image;
    }

    private bool checkWantsToStay(){
        bool returnBool = true;

        if(satisfactionLevel < 0){returnBool = false;Debug.Log("Customer is unhappy!");}

        if(amountSpent >= spendingLimit){returnBool = false;Debug.Log("Customer is out of money!");}

        if(checkMenu() == null){returnBool = false;Debug.Log("Customer didn't find anything on the menu they liked!");}

        if(fullness > fullnessThreshold){returnBool = false;Debug.Log("Customer is full!");}

        return returnBool;
    }

    private void calculateSatisfaction(){

        //Check if the main ingredient is liked
        if(food.resourceType1 == preferredFoodType){
            satisfactionLevel += 2;
        }
        else if (food.resourceType1 == dislikedFoodType){
            satisfactionLevel -= 2;
        }

        //Check if the sub ingredient is liked
        if(food.resourceType2 == preferredFoodType){
            satisfactionLevel += 1;
        }
        else if(food.resourceType2 == dislikedFoodType){
            satisfactionLevel -= 1;
        }

        //Check the quality
        satisfactionLevel += food.quality - qualityThreshold;

        //Check fullness //Waiting to have a better implementation of this
        /*
        if(fullness > fullnessThreshold){
            satisfactionLevel += 1;
        }
        else if(fullness < (fullnessThreshold / 2)){
            satisfactionLevel -= 1;
        }
        */

    }

    private DishManager.Dish checkMenu(){
        List<DishManager.Dish> menuItems = DishManager.control.getMenuItems();
        List<DishManager.Dish> potentialChoices = new List<DishManager.Dish>();
        DishManager.Dish finalChoice = null;
        int randomNumber;

        //Check conditions for picking a dish
        foreach(DishManager.Dish dish in menuItems){
            bool wouldPick = false;

            //Is it a preferred food type?
            if(dish.resourceType1 == preferredFoodType || dish.resourceType2 == preferredFoodType){
                wouldPick = true;
            }
            //Is it good enough quality?
            if(dish.quality >= qualityThreshold){
                wouldPick = true;
            }

            if(wouldPick){
                potentialChoices.Add(dish);
            }
        }

        randomNumber = (int)Random.Range(1,101);
        if(potentialChoices.Count > 0){
            finalChoice = potentialChoices[randomNumber % potentialChoices.Count];
        }
        else{
            wantsToStay = false;
            Debug.Log("Customer didn't see anything they like!");
        }

        return finalChoice;

    }

    public void setCustomerSprite(Sprite sprite){
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void setMonsterType(string type){
        monsterType = type;
    }

    public void setPreferredFoodType(DishManager.Categories type){
        preferredFoodType = type;
    }

    public void setDislikedFoodType(DishManager.Categories type){
        dislikedFoodType = type;
    }

    public void setQualityThreshold(int threshold){
        qualityThreshold = threshold;
    }

    public void setFullnessThreshold(int threshold){
        fullnessThreshold = threshold;
    }

    public void setSpendingLimit(int limit){
        spendingLimit = limit;
    }

    public void setEatSpeed(float speed){
        eatSpeed = speed;
    }

}
