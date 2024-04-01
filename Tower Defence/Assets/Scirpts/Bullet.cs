using UnityEngine;

namespace Scirpts
{
    public class Bullet : MonoBehaviour
    {
        private Transform _target;

        public float speed = 70;

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

        }

        private void HitTarget()
        {
            var effect = Instantiate(BulletDestroyEffect, transform.position, transform.rotation);
            Destroy(effect, 2);
            Destroy(_target.gameObject);
            Destroy(gameObject);
        }
    }
}