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

        public GameObject buildEffect;

        public bool CanBuild => _turretToBuild is not null;

        public bool HasMoney => PlayerStats.Money >= _turretToBuild.cost;

        public void SelectTurretToBuild(TurretBlueprint turret)
        {
            _turretToBuild = turret;
        }

        public void BuildTurretOn(Node node)
        {
            if (PlayerStats.Money < _turretToBuild.cost)
            {
                Debug.Log("ta pas la thune mdrrr");
                return;
            }

            PlayerStats.Money -= _turretToBuild.cost;
            Vector3 offset = Vector3.zero;
            Debug.Log($"money left {PlayerStats.Money}");
            offset.y += 0.5f;
            var temp = Instantiate(_turretToBuild.prefab, node.transform.position + offset, quaternion.identity);
            var buildEffectTemp = Instantiate(buildEffect, node.transform.position + offset, quaternion.identity);
            node._turret = temp;
            Destroy(buildEffectTemp, 2);
        }
    }
}
