using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DishScroller : MonoBehaviour {

    [SerializeField]private GameObject scrollButton;
    [SerializeField]private GameObject dishManager;
    [SerializeField]private GameObject dishScrollView;
    [SerializeField]private GameObject dishButton;
	public int numberOfDishes;

    void Start(){
        setNumberOfDishes();
        setupButtons();
        //StartCoroutine(LateStart(1.0f));
    }

    IEnumerator LateStart(float waitTime){
        yield return new WaitForSeconds(waitTime);
        setNumberOfDishes();
        setupButtons();
    }

    public void setupButtons(){
        for(int i = 0; i < numberOfDishes; i++){
            float newY, newX;

            GameObject scrollButtonClone = (GameObject) Instantiate(scrollButton);

            newY = gameObject.transform.position.y + (i * -40f) - 20f;
            newX = gameObject.transform.position.x + 20f;

            scrollButtonClone.transform.SetParent(gameObject.transform);

            scrollButtonClone.transform.position = new Vector3(newX, newY, 0);
            scrollButton.GetComponentInChildren<DishScrollButton>().setDishNumber(i);

            scrollButton.GetComponentInChildren<DishScrollButton>().scrollPane = dishScrollView;
            scrollButton.GetComponentInChildren<DishScrollButton>().dishButton = dishButton;

            Debug.Log("Created dish scroll button " + i);
        }
    }

    public void setNumberOfDishes(){
        numberOfDishes = dishManager.GetComponent<DishManager>().numberOfDishes;
    }
}
