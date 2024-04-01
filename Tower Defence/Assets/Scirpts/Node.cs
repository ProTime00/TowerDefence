using UnityEngine;

namespace Scirpts
{
    public class Node : MonoBehaviour
    {
        private GameObject _turret;
        
        
        private Material _material ;
        private Color _defaultColor;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _defaultColor = _material.color;

        }

        private void OnMouseDown()
        {
            if (_turret != null)
            {
                Debug.Log("cant build here");
                return;
            }

            var turretToBuild = BuildManager.Instance.GetTurretToBuild();
            Vector3 offset = Vector3.zero;
            offset.y += 0.5f;
            _turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation);
        }

        private void OnMouseEnter()
        {
            _material.color = Color.cyan;
        }

        private void OnMouseExit()
        {
            _material.color = _defaultColor;
        }
    }
}
