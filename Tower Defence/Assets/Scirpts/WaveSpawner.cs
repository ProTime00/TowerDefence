using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts
{
    public class WaveSpawner : MonoBehaviour
    {
        public static int enemiesAlives = 0;

        public Wave[] Waves;
        
        
        public Transform enemyPrefab;
        public Transform spawnPoint;
    
        private float timeBtwWaves = 3;
        private float _countdown = 2;

        public Text waveCountdownText;
    
        private int _waveIndex;

        private void Update()
        {
            if (enemiesAlives > 0)
            {
                return;
            }
            if (_countdown <= 0)
            {
                StartCoroutine(SpawnWave());
                _countdown = timeBtwWaves;
                return;
            }

        
            _countdown -= Time.deltaTime;
            _countdown = Mathf.Clamp(_countdown, 0, Mathf.Infinity);
            waveCountdownText.text = $"{_countdown:00.0}"; 


        }

        private IEnumerator SpawnWave()
        {
            
            PlayerStats.Rounds += 1;

            Wave wave = Waves[_waveIndex];
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1 / wave.spawnRate);
            }
            _waveIndex++;
            if (_waveIndex >= Waves.Length)
            {
                Debug.Log("end level 1");
                this.enabled = false;
            }
        }

        private void SpawnEnemy(GameObject enemy)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            enemiesAlives++;
        }
    }
}
