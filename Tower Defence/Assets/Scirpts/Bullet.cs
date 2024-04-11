using System;
using UnityEngine;

namespace Scirpts
{
    public class Bullet : MonoBehaviour
    {
        private Transform _target;

        public delegate void Explosion();

        public static event Explosion OnMissileExplosion;

        public float speed = 70;
        public float AOE;

        public GameObject BulletDestroyEffect;
        public int bulletDamage = 25;
        
        private Collider[] _results = new Collider[256];


        public void SeekTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = _target.position - transform.position;
            float distThisFrame = speed * Time.deltaTime;
            if (dir.magnitude <= distThisFrame)
            {
                HitTarget();
                return;
            }
            
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            transform.LookAt(_target);
        }

        private void HitTarget()
        {
            var effect = Instantiate(BulletDestroyEffect, transform.position, transform.rotation);
            Destroy(effect, 2);

            if (AOE > 0)
            {
                Explode();
            }
            else
            {
                Damage(_target);
            }
            
            Destroy(gameObject);
        }

        private void Explode()
        {
            OnMissileExplosion?.Invoke();
            var size = Physics.OverlapSphereNonAlloc(transform.position, AOE, _results);
            for (var index = 0; index < size; index++)
            {
                if (index > 255)
                {
                    Debug.LogError("The array _results is too small for some reasons");
                }
                var enemy = _results[index];
                if (enemy.CompareTag("Enemy"))
                {
                    Damage(enemy.transform);
                }
            }
        }

        private void Damage(Transform enemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AOE);
        }
    }
}