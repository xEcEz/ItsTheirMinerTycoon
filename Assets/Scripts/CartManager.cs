using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MineArea {

    // Managers
    public ElevatorManager EM;

    void Start() {
        capacity = 5;
    }

    public override void CollectGold() {
        if((!active || managed) && EM.areaGold > 0) {
            startTime = Time.time;
            active = true;
            toggleButton.interactable = false;

            int collectedGold = (EM.areaGold >= capacity ? capacity : EM.areaGold);
            bufferGold = collectedGold;
            EM.areaGold -= collectedGold;
        }
    }
}
