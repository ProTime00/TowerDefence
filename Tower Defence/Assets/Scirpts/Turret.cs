using UnityEngine;

namespace Scirpts
{
    public class Turret : MonoBehaviour
    {
        [Header("General")]
        public float range = 15;
        public float turnSpeed = 15;
        
        [Header("Use bullets")]
        public float fireRate = 1;
        public float fireCountdown;
        public GameObject bulletPrefab;

        [Header("Use laser")]
        public bool useLaser;
        public LineRenderer lineRenderer;
        
        
    
        [Header("Unity setup Fields")]
        public Transform rotator;
        public Transform firePoint;
    
        
        private const string EnemyTag = "Enemy";
        private Transform _target;
    
    


        private void Start()
        {
            if (useLaser)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }


        private void Update()
        {
            if (_target == null)
            {
                if (useLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }
                }
                return;
            }

            LockOnTarget();

            if (useLaser)
            {
                UseLaser();
            }
            else
            {
                if (fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1 / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }
        }

        private void UseLaser()
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, _target.position);
        }

        private void LockOnTarget()
        {
            Vector3 dir = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
