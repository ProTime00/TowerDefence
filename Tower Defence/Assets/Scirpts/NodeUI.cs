using System.Collections;
using System.Collections.Generic;
using Scirpts;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;
    public Text cost;
    public Button upgradeButton;

    public void SetTarget(Node node)
    {
        target = node;

        if (!target.isUpgraded)
        {
            cost.text = $"upgrade\n${target.blueprint.upgradeCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            cost.text = "upgrade\ndone";
        }
        
        ui.SetActive(true);
        transform.position = target.GetBuildPosition();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }
    
}
