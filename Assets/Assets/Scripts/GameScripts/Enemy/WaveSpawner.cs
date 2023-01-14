using System.Collections;
using TheCity.ChessMaze;
using TheCity.GameMaster;
using TMPro;
using UnityEngine;

namespace TheCity.EnemyAI
{
    public class WaveSpawner : MonoBehaviour
    {
        public enum SpawnState
        { SPAWNING, WAITING, COUNTING, MAPCREATION };

        [System.Serializable]
        public class Wave
        {
            public string name;
            public GameObject enemy;
            public int count;
            public float rate;
        }

        [SerializeField]
        private Wave[] waves;

        [SerializeField] private TextMeshProUGUI _waveText;

        public WaveManager waveManager;

        public bool spawnUsingPrefabs;

        private int nextWave = 0;

        private int waveIndex = 0;

        public static int waveIndexPublic;

        public int NextWave
        {
            get { return nextWave + 1; }
        }

        //public Transform[] spawnPoints;

        public MapBrain mapBrain;

        public float timeBetweenWaves = 5f;
        public float waveCountdown;

        public float WaveCountdown
        {
            get { return waveCountdown; }
        }

        private float searchCountdown = 1f;

        private SpawnState state = SpawnState.MAPCREATION;

        public SpawnState State
        {
            get { return state; }
        }

        public Wave[] Waves { get => waves; set => waves = value; }

        private void Start()
        {
            //if (spawnPoints.Length == 0)
            //{
            //	Debug.LogError("No spawn points referenced.");
            //}

            waveIndexPublic = waveIndex;

            waveCountdown = timeBetweenWaves;
        }

        private void Update()
        {
            if (mapBrain.IsAlgorithmRunning || !mapBrain.DidAlgorithmEverRan)
            {
                MapCreation();
            }
            else
            {
                if (state == SpawnState.WAITING)
                {
                    if (!EnemyIsAlive())
                    {
                        WaveCompleted();
                    }
                    else
                    {
                        return;
                    }
                }

                if (waveCountdown <= 0)
                {
                    if (state != SpawnState.SPAWNING)
                    {
                        StartCoroutine(SpawnWave(waves[nextWave]));
                    }
                }
                else
                {
                    waveCountdown -= Time.deltaTime;
                }

                if (waveIndex == waves.Length)
                {
                    Debug.Log("LEVEL WON!");
                    this.enabled = false;
                }
            }

            waveIndexPublic = waveIndex;
        }

        private void MapCreation()
        {
            waveCountdown = timeBetweenWaves;

            state = SpawnState.MAPCREATION;

            nextWave = 0;

            // IF mapBrain.mapGenerationCompleted = true
            if (!mapBrain.IsAlgorithmRunning || mapBrain.DidAlgorithmEverRan)
            {
                state = SpawnState.COUNTING;
            }
        }

        private bool MapCreationIsRunning()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (mapBrain.IsAlgorithmRunning)
                {
                    return true;
                }
            }
            return false;
        }

        private void WaveCompleted()
        {
            Debug.Log("Wave Completed!");

            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;

            if (nextWave + 1 > waves.Length - 1)
            {
                nextWave = 0;
                Debug.Log("ALL WAVES COMPLETE! Looping...");
            }
            else
            {
                nextWave++;
            }
        }

        private bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerator SpawnWave(Wave _wave)
        {
            Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.SPAWNING;

            waveIndex++;
            _waveText.text = "Wave " + waveIndex;
            PlayerStats.Rounds++;

            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds(1f / _wave.rate);

                Debug.Log(_wave.count);
            }

            state = SpawnState.WAITING;

            yield break;
        }

        private void SpawnEnemy(GameObject _enemy)
        {
            Debug.Log("Spawning Enemy: " + _enemy.name);

            //Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //Instantiate(_enemy, _sp.position, _sp.rotation);
            waveManager.BeginEnemyLogic(spawnUsingPrefabs, _enemy);
        }
    }
}