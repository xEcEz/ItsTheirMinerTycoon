using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineArea : MonoBehaviour {

    // Managers
    public GameManager GM;

    // UI
    public Text areaGoldDisp;
    public Text boostedDisp;
    public Text levelDisp;
    public Text managedDisp;
    public Button toggleButton;
    public Button hireManagerButton;
    public Button boostButton;
    public Button upgradeButton;

    // Ressources
    public int areaGold = 0;

    // Specs
    public int level = 1;
    public int capacity;
    public int upgradeRatio = 2;
    public int upgradeCost = 10;
    public int boostCooldown = 10;
    public int boostDuration = 5;
    public int boostRatio = 2;
    public int managerCost = 10;
    public float baseRequiredTime = 3.0F;

    // Management
    public bool active = false;
    public bool managed = false;
    protected bool boosted = false;
    protected int bufferGold = 0;
    protected float requiredTime;
    protected float startTime = 0;
    protected float boostStartTime, boostEndTime = 0;

    private void Awake() {
        requiredTime = baseRequiredTime;
        boostedDisp.gameObject.SetActive(false);
        managedDisp.gameObject.SetActive(false);

        upgradeButton.GetComponentInChildren<Text>().text = "Upgrade (" + upgradeCost + "G)";
        hireManagerButton.GetComponentInChildren<Text>().text = "Hire Manager (" + managerCost + "G)";


        // Prefab assignment handling
        if(!GM) {
            GameObject GMObject = GameObject.Find("GameManager");
            GM = GMObject.GetComponent<GameManager>();
        }
    }

    void Update() {
        // Handle toggling
        if(active && !managed && (Time.time - startTime) >= requiredTime) {
            toggleButton.interactable = true;
            active = false;
        }

        // Handle boosting
        if(!boostButton.interactable && (Time.time - boostEndTime) >= boostCooldown) {
            boostButton.interactable = true;
        }
        if(boosted && (Time.time - boostStartTime) >= boostDuration) {
            boosted = false;
            requiredTime = baseRequiredTime;
            boostEndTime = Time.time;

            boostedDisp.gameObject.SetActive(false);
        }

        // Handle buffer gold
        if((!active || managed) && bufferGold > 0) {
            areaGold += bufferGold;
            bufferGold = 0;
        }

        // Handle management
        if(managed && (Time.time - startTime) >= requiredTime) {
            CollectGold();
        }
        
        // Handle UI
        if(areaGoldDisp != null) {
            areaGoldDisp.text = "Stored Gold: " + areaGold;
        }
        hireManagerButton.interactable = (GM.gold >= managerCost);
        upgradeButton.interactable = (GM.gold >= upgradeCost);
    }

    public virtual void CollectGold() { }

    public void Upgrade() {
        if(GM.gold >= upgradeCost) {
            GM.gold -= upgradeCost;
            capacity *= upgradeRatio;
            upgradeCost *= upgradeRatio;
            level += 1;

            levelDisp.text = "Level: " + level;
            upgradeButton.GetComponentInChildren<Text>().text = "Upgrade (" + upgradeCost + "G)";
        }
    }

    public void Boost() {
        boosted = true;
        boostStartTime = Time.time;
        boostEndTime = Time.time;
        requiredTime /= boostRatio;

        boostButton.interactable = false;
        boostedDisp.gameObject.SetActive(true);
    }

    public void HireManager() {
        if(GM.gold >= managerCost) {
            active = true;
            managed = true;
            GM.gold -= managerCost;

            managedDisp.gameObject.SetActive(true);
            hireManagerButton.interactable = false;
            hireManagerButton.gameObject.SetActive(false);
            toggleButton.interactable = false;
            toggleButton.gameObject.SetActive(false);
        }
    }
}

