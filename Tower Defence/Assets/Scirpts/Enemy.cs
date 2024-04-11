using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts
{
    public class Enemy : MonoBehaviour
    {
        public float startSpeed = 10;
        
        [HideInInspector]
        public float Speed;

        public float health = 100;

        private float maxHp;

        public int moneyGain = 50;

        public GameObject enemyDieEffetc;

        private bool _isDead;

        public Image hp;

        private void Awake()
        {
            maxHp = health;
        }

        private void Start()
        {
            Speed = startSpeed;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            hp.fillAmount = health / maxHp;
            if (health <= 0)
            {
                KillEnemy();
            }
        }

        private void KillEnemy()
        {
            // fix the issue when several sources kill an enemy in the same frame (usually with the laser)
            if (_isDead)
            {
                return;
            }

            _isDead = true;
            PlayerStats.Money += moneyGain;
            var temp = Instantiate(enemyDieEffetc, transform.position, quaternion.identity);
            Destroy(temp, 2);
            WaveSpawner.enemiesAlives--;
            Destroy(gameObject);
        }

        public void Slow(float slowPercent)
        {
            Speed = startSpeed * slowPercent;
        }
    }
}
