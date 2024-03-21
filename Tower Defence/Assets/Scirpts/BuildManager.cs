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

        private void Start()
        {
            _turretToBuild = standartTurretPrefab;
        }

        private GameObject _turretToBuild;

        public GameObject GetTurretToBuild()
        {
            return _turretToBuild;
        }
    }
}
