using System;
using UnityEngine;

namespace Scirpts
{
    public class Bullet : MonoBehaviour
    {
        private Transform _target;

        public float speed = 70;
        public float AOE;

        public GameObject BulletDestroyEffect;
        

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
            var hitObjects = Physics.OverlapSphere(transform.position, AOE);
            foreach (var enemy in hitObjects)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Damage(enemy.transform);
                }
            }
        }

        private void Damage(Transform enemy)
        {
            Destroy(enemy.gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AOE);
        }
    }
}