using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaftsManager : MonoBehaviour {

    // Managers
    public GameManager GM;
    public List<ShaftManager> shaftManagers;

    // UI
    public GameObject shaftsPanel;
    public GameObject shaftPanelPrefab;
    public Button newShaftButton;

    // Management
    private int newShaftCost = 1;
    private int shaftIndex = 0;
    
    void Awake() {
        newShaftButton.GetComponentInChildren<Text>().text = "New Shaft (" + newShaftCost + "G)"; ;
    }

	void Update () {
        if(GM.gold >= newShaftCost) {
            newShaftButton.interactable = true;
        } else {
            newShaftButton.interactable = false;
        }
    }

    public void CreateNewShaft() {
        if(GM.gold >= newShaftCost) {
            GM.gold -= newShaftCost;
            newShaftCost *= 10;

            // Instantiate the new shaftPanel
            GameObject instantiatedShaftPanel = Instantiate<GameObject>(shaftPanelPrefab, transform.position, transform.rotation);
            newShaftButton.GetComponentInChildren<Text>().text = "New Shaft: " + newShaftCost + "G";
            instantiatedShaftPanel.transform.SetParent(shaftsPanel.transform);
            instantiatedShaftPanel.transform.SetSiblingIndex(shaftIndex);

            // Push the new shaftManager in the shafts list
            shaftManagers.Add(instantiatedShaftPanel.GetComponent<ShaftManager>());
            shaftIndex++;
        }
    }
}
