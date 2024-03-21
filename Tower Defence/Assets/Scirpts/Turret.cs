using UnityEngine;

namespace Scirpts
{
    public class Turret : MonoBehaviour
    {
        [Header("Attributes")]
        
        public float range = 15;
        public float fireRate = 1;
        public float fireCountdown = 0f;
        public float turnSpeed = 15;
    
        [Header("Unity setup Fields")]
        
        public Transform rotator;
        public GameObject bulletPrefab;
        public Transform firePoint;
    
        private const string EnemyTag = "Enemy";
    
    
        private Transform _target;
    
    


        private void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }


        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            Vector3 dir = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        private void Shoot()
        {
            var BulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = BulletGameObject.GetComponent<Bullet>();
            if (bullet is not null)
            {
                bullet.SeekTarget(_target);
            }
        }

        private void UpdateTarget()
        {
     
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (var variable in enemies)
            {
                float dist = Vector3.Distance(transform.position, variable.transform.position);
                if (shortestDistance >= dist)
                {
                    shortestDistance = dist;
                    nearestEnemy = variable;
                }
            }

            if (nearestEnemy is not null && shortestDistance <= range)
            {
                _target = nearestEnemy.transform;
            }
            else
            {
                _target = null;
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
