using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour {


    [SerializeField]private GameObject customerPrefab;
    [SerializeField]private Sprite[] customerSprites;
    public static List<Customer> customerArray;
    public int numberOfCustomers;
    //private GameObject customer;
    private List<GameObject> seats;

    private float spawnSpeed = 2.5f;
    private float spawnCounter = 0;

    void Start(){
        initSeats();
        initCustomerArray();
    }

    void Update(){
        spawnCounter += Time.deltaTime;

        if(spawnCounter > spawnSpeed && findOpenSeat() != null){
            int customerType = (int)Random.Range(0, 3);
            Debug.Log("Customer type " + customerType + " picked!");
            generateCustomer(customerType);
            spawnCounter = 0;
        }
    }

    private void initCustomerArray(){
        customerArray = new List<Customer>();
        Debug.Log("Setting up customer array!");

        customerArray.Add(new Customer());
        customerArray[0].monsterType = "Jellyfish";
        customerArray[0].preferredFoodType = DishManager.Categories.Veggie;
        customerArray[0].dislikedFoodType = DishManager.Categories.None;
        customerArray[0].qualityThreshold = 1;
        customerArray[0].fullnessThreshold = 6;
        customerArray[0].spendingLimit = 90;
        customerArray[0].eatSpeed = 3.0f;
        customerArray[0].customerSprite = customerSprites[0];
        numberOfCustomers++;

        customerArray.Add(new Customer());
        customerArray[1].monsterType = "Squid";
        customerArray[1].preferredFoodType = DishManager.Categories.Fish;
        customerArray[1].dislikedFoodType = DishManager.Categories.None;
        customerArray[1].qualityThreshold = 2;
        customerArray[1].fullnessThreshold = 10;
        customerArray[1].spendingLimit = 150;
        customerArray[1].eatSpeed = 3.5f;
        customerArray[1].customerSprite = customerSprites[1];
        numberOfCustomers++;

        customerArray.Add(new Customer());
        customerArray[2].monsterType = "Mermaid";
        customerArray[2].preferredFoodType = DishManager.Categories.Treasure;
        customerArray[2].dislikedFoodType = DishManager.Categories.None;
        customerArray[2].qualityThreshold = 3;
        customerArray[2].fullnessThreshold = 15;
        customerArray[2].spendingLimit = 210;
        customerArray[2].eatSpeed = 4.0f;
        customerArray[2].customerSprite = customerSprites[2];
        numberOfCustomers++;

        Debug.Log("Customer array set up!");
    }

    public void generateCustomer(int type){
        GameObject seatToGoTo;

        GameObject customer = (GameObject) Instantiate(customerPrefab);

        customer.GetComponent<CustomerControl>().setMonsterType(customerArray[type].monsterType);
        customer.GetComponent<CustomerControl>().setPreferredFoodType(customerArray[type].preferredFoodType);
        customer.GetComponent<CustomerControl>().setDislikedFoodType(customerArray[type].dislikedFoodType);
        customer.GetComponent<CustomerControl>().setQualityThreshold(customerArray[type].qualityThreshold);
        customer.GetComponent<CustomerControl>().setFullnessThreshold(customerArray[type].fullnessThreshold);
        customer.GetComponent<CustomerControl>().setSpendingLimit(customerArray[type].spendingLimit);
        customer.GetComponent<CustomerControl>().setEatSpeed(customerArray[type].eatSpeed);
        customer.GetComponent<CustomerControl>().setCustomerSprite(customerArray[type].customerSprite);

        seatToGoTo = findOpenSeat();
        customer.GetComponent<CustomerControl>().setAssignedSeat(seatToGoTo);
        seatToGoTo.GetComponent<SeatNode>().setOccupied(true);
    }

    public GameObject findOpenSeat(){
        foreach(GameObject seat in seats){
            if(!seat.GetComponent<SeatNode>().isOccupied()){
                return seat;
            }
        }
        return null;
    }

    public void initSeats(){
        GameObject[] daSeats;
        daSeats = GameObject.FindGameObjectsWithTag("Seat");
        Debug.Log("Found " + daSeats.Length + " seats!");

        seats = new List<GameObject>();
        for(int i = 0; i < daSeats.Length; i++){
            seats.Add(daSeats[i]);
        }
    }

    public class Customer{

        public string monsterType;
        public DishManager.Categories preferredFoodType;
        public DishManager.Categories dislikedFoodType;
        public int qualityThreshold;
        public int fullness;
        public int fullnessThreshold;
        public int spendingLimit;
        public int amountSpent;
        public float eatSpeed;
        public Sprite customerSprite;

    }

}
