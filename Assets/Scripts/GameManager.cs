using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
        
    // UI
    public Text goldDisp;
    public Text workersDisp;
    public Text managersDisp;

    // Ressources
    public int gold = 0;
    public int rateGold = 0;
    public int activeWorkersCount = 0;
    public int totalWorkersCount = 0;
    public int managersCount = 0;

    // Areas
    public CartManager CM;
    public ElevatorManager EM;
    public ShaftsManager SM;
	
	void Update () {
        if((!CM.active || CM.managed) && CM.areaGold > 0) {
            gold += CM.areaGold;
            CM.areaGold = 0;
        }

        int activeShafts = SM.shaftManagers.FindAll(s => s.active).Count;
        int managedShafts = SM.shaftManagers.FindAll(s => s.managed).Count;
        activeWorkersCount = Utils.boolToInt(CM.active) + Utils.boolToInt(EM.active) + activeShafts;
        totalWorkersCount = 2 + SM.shaftManagers.Count;
        managersCount = Utils.boolToInt(CM.managed) + Utils.boolToInt(EM.managed) + managedShafts;

        goldDisp.text = "Gold: " + gold;
        workersDisp.text = "Workers: " + activeWorkersCount + "/" + totalWorkersCount + " active";
        managersDisp.text = "Managers: " + managersCount;
    }
}
