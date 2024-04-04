using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform enemyPrefab;
        public Transform spawnPoint;
    
        public float timeBtwWaves = 5;
        private float _countdown = 2;

        public Text waveCountdownText;
    
        private int _waveIndex;

        private void Update()
        {
            if (_countdown <= 0)
            {
                StartCoroutine(SpawnWave());
                _countdown = timeBtwWaves;
            }

        
            _countdown -= Time.deltaTime;
            _countdown = Mathf.Clamp(_countdown, 0, Mathf.Infinity);
            waveCountdownText.text = $"{_countdown:00.0}"; 


        }

        private IEnumerator SpawnWave()
        {
            _waveIndex++;
            PlayerStats.Rounds += 1;
            for (int i = 0; i < _waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
