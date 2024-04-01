using System;
using Scirpts;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseStandartTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.standartTurretPrefab);
    }

    public void SecondTurretPurchase()
    {
        _buildManager.SetTurretToBuild(_buildManager.secondTurretPrefab);
    }
}
