using System;
using Scirpts;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {
        _buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissileTower()
    {
        _buildManager.SelectTurretToBuild(missileLauncher);
    }
}
