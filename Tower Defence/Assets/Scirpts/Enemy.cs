using UnityEngine;

namespace Scirpts
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10;

        private Transform _target;

        private int _waypointindex;

        private void Start()
        {
            _target = Waypoints.WaypointsTransforms[0];
        }

        private void Update()
        {
            Vector3 dir = _target.position - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                GetNextWaypoint();
            }
        }

        private void GetNextWaypoint()
        {
            if (_waypointindex >= Waypoints.WaypointsTransforms.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            _waypointindex++;
            _target = Waypoints.WaypointsTransforms[_waypointindex];
        }
    }
}
