using UnityEngine;
using UnityEngine.EventSystems;

namespace Scirpts
{
    public class Node : MonoBehaviour
    {
        [Header("Optional")]
        public GameObject _turret;
        
        
        private Material _material ;
        private Color _defaultColor;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _defaultColor = _material.color;

        }

        private void OnMouseDown()
        {
            if (!BuildManager.Instance.CanBuild)
            {
                return;
            }
            if (_turret != null)
            {
                Debug.Log("cant build here");
                return;
            }

            BuildManager.Instance.BuildTurretOn(this);
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
    }
}
