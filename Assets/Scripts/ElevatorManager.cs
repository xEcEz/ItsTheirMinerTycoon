using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MineArea {

    // Managers
    public ShaftsManager SM;

    void Start() {
        capacity = 5;
    }

    public override void CollectGold() {
        // Collect only if one area has any gold
        if((!active || managed) && SM.shaftManagers.Find(x => x.areaGold > 0)) {
            startTime = Time.time;
            active = true;
            toggleButton.interactable = false;

            foreach(ShaftManager sManager in SM.shaftManagers) {
                if(bufferGold < capacity) {
                    int collectedGold = ((bufferGold + sManager.areaGold) >= capacity ? (capacity - bufferGold) : sManager.areaGold);
                    bufferGold += collectedGold;
                    sManager.areaGold -= collectedGold;
                }
            }
        }
    }
}
