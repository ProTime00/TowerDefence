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

        public GameObject standartTurretPrefab;
        public GameObject secondTurretPrefab;

        private GameObject _turretToBuild;
        public GameObject missileTurretPrefab;

        public GameObject GetTurretToBuild()
        {
            return _turretToBuild;
        }

        public void SetTurretToBuild(GameObject turret)
        {
            _turretToBuild = turret;
        }
    }
}
