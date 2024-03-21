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
            waveCountdownText.text = Math.Round(_countdown).ToString(); 


        }

        private IEnumerator SpawnWave()
        {
            _waveIndex++;
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
