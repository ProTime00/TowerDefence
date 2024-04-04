using Unity.Mathematics;
using UnityEngine;

namespace Scirpts
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one buid Manager");
            }
            Instance = this;
        }

        private TurretBlueprint _turretToBuild;
        private Node selectedNode;
        public NodeUI nodeUI;

        public GameObject buildEffect;

        public bool CanBuild => _turretToBuild is not null;

        public bool HasMoney => PlayerStats.Money >= _turretToBuild.cost;

        public TurretBlueprint GetTurretToBuild()
        {
            return _turretToBuild;
        }

        public void SelectNode(Node node)
        {
            if (node == selectedNode)
            {
                DeselectNode();
                return;
            }
            selectedNode = node;
            _turretToBuild = null;
            
            nodeUI.SetTarget(node);
        }

        public void DeselectNode()
        {
            selectedNode = null;
            nodeUI.Hide();
        }

        public void SelectTurretToBuild(TurretBlueprint turret)
        {
            nodeUI.Hide();
            _turretToBuild = turret;
            selectedNode = null;
        }
    }
}
