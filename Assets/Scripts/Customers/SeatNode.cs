using UnityEngine;
using System.Collections;

public class SeatNode : MonoBehaviour {

	private bool occupied;

    void Start(){
        occupied = false;
    }

    public bool isOccupied(){
        return occupied;
    }

    public void setOccupied(bool occupied){
        this.occupied = occupied;
    }
}
