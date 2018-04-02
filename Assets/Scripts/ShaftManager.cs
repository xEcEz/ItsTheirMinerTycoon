using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaftManager : MineArea {

    void Start() {
        capacity = 1;
    }

    public override void CollectGold() {
        if(!active || managed) {
            startTime = Time.time;
            active = true;
            toggleButton.interactable = false;

            bufferGold = capacity;
        }
    }
}
