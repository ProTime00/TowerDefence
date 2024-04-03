using Unity.Mathematics;
using UnityEngine;

namespace Scirpts
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10;

        public int health = 100;

        public int moneyGain = 50;

        public GameObject enemyDieEffetc;

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
                EndPath();
                return;
            }
            _waypointindex++;
            _target = Waypoints.WaypointsTransforms[_waypointindex];
        }

        private void EndPath()
        {
            PlayerStats.Lives--;
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                KillEnemy();
            }
        }

        private void KillEnemy()
        {
            PlayerStats.Money += moneyGain;
            var temp = Instantiate(enemyDieEffetc, transform.position, quaternion.identity);
            Destroy(temp, 2);
            Destroy(gameObject);
        }
    }
}
