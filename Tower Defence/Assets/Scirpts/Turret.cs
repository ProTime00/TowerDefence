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
        public int DamageOverTime = 20;
        public LineRenderer lineRenderer;
        public ParticleSystem laserEffect;
        public GameObject lightOnLaserEffect;
        
        
        
    
        [Header("Unity setup Fields")]
        public Transform rotator;
        public Transform firePoint;
    
        
        private const string EnemyTag = "Enemy";
        private Transform _target;
        private Enemy _enemy;
        private float slowPercent = 0.5f;


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
                        
                        laserEffect.Stop();
                        lightOnLaserEffect.SetActive(false);
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
            _enemy.TakeDamage(DamageOverTime * Time.deltaTime);
            _enemy.Slow(slowPercent);
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                lightOnLaserEffect.SetActive(true);
                laserEffect.Play();
            }
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, _target.position);

            Vector3 enemyToTurret = firePoint.position - _target.position;
            laserEffect.transform.rotation = Quaternion.LookRotation(enemyToTurret);
            
            laserEffect.transform.position = _target.position + enemyToTurret.normalized;
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
                _enemy = _target.GetComponent<Enemy>();
                
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
