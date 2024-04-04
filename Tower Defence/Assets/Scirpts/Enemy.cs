using System;
using Unity.Mathematics;
using UnityEngine;

namespace Scirpts
{
    public class Enemy : MonoBehaviour
    {
        public float startSpeed = 10;
        
        [HideInInspector]
        public float Speed;

        public float health = 100;

        public int moneyGain = 50;

        public GameObject enemyDieEffetc;

        private void Start()
        {
            Speed = startSpeed;
        }

        public void TakeDamage(float damage)
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

        public void Slow(float slowPercent)
        {
            Speed = startSpeed * slowPercent;
        }
    }
}
