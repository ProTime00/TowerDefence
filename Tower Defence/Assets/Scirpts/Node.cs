using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Scirpts
{
    public class Node : MonoBehaviour
    {
        [HideInInspector]
        public GameObject turret;

        [HideInInspector] public TurretBlueprint blueprint;
        [HideInInspector] public bool isUpgraded;
        
        
        private Material _material ;
        private Color _defaultColor;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _defaultColor = _material.color;

        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (turret != null)
            {
                BuildManager.Instance.SelectNode(this);
                
                return;
            }
            
            if (!BuildManager.Instance.CanBuild)
            {
                return;
            }
            BuildTurret(BuildManager.Instance.GetTurretToBuild());
        }

        private void BuildTurret(TurretBlueprint turretBlueprint)
        {
            
            if (PlayerStats.Money < turretBlueprint.cost)
            {
                return;
            }

            PlayerStats.Money -= turretBlueprint.cost;
            Vector3 offset = Vector3.zero;
            offset.y += 0.5f;
            var temp = Instantiate(turretBlueprint.prefab, transform.position + offset, quaternion.identity);
            blueprint = turretBlueprint;
            var buildEffectTemp = Instantiate(BuildManager.Instance.buildEffect, transform.position + offset, quaternion.identity);
            turret = temp;
            
            Destroy(buildEffectTemp, 2);
        }

        public void UpgradeTurret()
        {

            if (isUpgraded)
            {
                return;
            }
            if (PlayerStats.Money < blueprint.upgradeCost)
            {
                return;
            }

            
            PlayerStats.Money -= blueprint.upgradeCost;
            
            Destroy(turret);
            
            Vector3 offset = Vector3.zero;
            offset.y += 0.5f;
            var temp = Instantiate(blueprint.upgradedPrefab, transform.position + offset, Quaternion.identity);
            var buildEffectTemp = Instantiate(BuildManager.Instance.buildEffect, transform.position + offset, Quaternion.identity);
            turret = temp;
            isUpgraded = true;
            Destroy(buildEffectTemp, 2);
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!BuildManager.Instance.CanBuild)
            {
                return;
            }

            _material.color = BuildManager.Instance.HasMoney ? Color.cyan : Color.red;
            
        }

        private void OnMouseExit()
        {
            _material.color = _defaultColor;
        }

        public Vector3 GetBuildPosition()
        {
            Vector3 offset = Vector3.zero;
            offset.y += 2;
            return transform.position + offset;
        }
    }
}
