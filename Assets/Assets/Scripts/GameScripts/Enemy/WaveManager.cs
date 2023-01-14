using System.Collections;
using System.Collections.Generic;
using TheCity.ChessMaze;
using UnityEngine;
using Color = UnityEngine.Color;

namespace TheCity.EnemyAI
{
    public class WaveManager : MonoBehaviour
    {
        //private GameObject enemy;
        private Transform parent;

        private Vector3 startPosition;
        private Vector3 exitPosition;

        private Vector3 startRotation;

        public GameObject[] enemies;

        private MapBrain currentMap;

        public MapVisualizer mapVisualizer;

        private MapData data;
        private MapGrid grid;
        private EnemyAbstract enemy;

        private EnemyFactory enemyFactory;

        public WaveSpawner waveSpawner;

        private Coroutine enemySpawnCoroutine;

        private Coroutine SpawnUsingPrefabsCoroutine;

        public static int EnemiesAlive = 0;

        public Wave[] waves;

        //public Transform spawnPoint;

        public float timeBetweenWaves = 5f;
        private float countdown = 2f;

        //public Text waveCountdownText;

        //public GameManager gameManager;

        private int waveIndex = 0;

        public int numberOfEnemies = 5;

        private GameObject enemyObject;

        //public Enemy enemy;

        public static List<EnemyAbstract> enemyList = new List<EnemyAbstract>();

        private void Start()
        {
            parent = this.transform;
            //enemyFactory = GetComponent<EnemyFactory>();
            //enemyFactory = new EnemyFactory();
            //mapVisualizer = GetComponent<MapVisualizer>();
        }

        public void BeginEnemyLogic(bool spawnUsingPrefabs, GameObject _enemyObject)
        {
            //for (int i = 0; i < numberOfEnemies; i++)
            //{
            enemyObject = _enemyObject;
            SpawnEnemies(spawnUsingPrefabs);
            //}
        }

        public void SpawnEnemies(bool spawnUsingPrefabs)
        {
            GetStartAndExitLocation(startPosition, exitPosition);
            GetStartRotation(startRotation);

            if (spawnUsingPrefabs)
            {
                //SpawnUsingPrefabs();
                SpawnUsingPrefabsCoroutine = StartCoroutine(SpawnUsingPrefabs());
            }
            else
            {
                SpawnUsingPrimitives();
            }
        }

        private IEnumerator SpawnUsingPrefabs()
        {
            //if (enemyList != null)
            //{
            var rotation = new Vector3();

            rotation = RotationCalculator();

            CreateIndicator<Enemy>(startPosition, enemyObject, Quaternion.Euler(rotation));

            //if (numberOfEnemies <= 1)
            //{
            //    CreateIndicator<Enemy>(startPosition, enemies[0]);
            //    Debug.Log("Enemy Spawned!");
            //}
            //else if (numberOfEnemies > 1)
            //{
            //for (int i = 0; i < numberOfEnemies; i++)
            //    {
            //        CreateIndicator<Enemy>(startPosition, enemies[0], Quaternion.Euler(rotation));
            //        yield return new WaitForSeconds(1);
            //        CreateIndicator<Enemy>(startPosition, enemies[1], Quaternion.Euler(rotation));
            //        yield return new WaitForSeconds(1);
            //    }
            //    //foreach (var enemy in enemyList)
            //    //{
            //    //    CreateIndicator<enemy>(startPosition, enemies[0], Quaternion.Euler(rotation));
            //    //}
            //}
            yield break;
            //}
            //else yield break;
        }

        /* public void SpawnEnemiesUsingPrefabs()
             *
             * if there are enemies to spawn, but reset is called, clear the list
             * else if there are no enemies, spawn the number of enemies required by the wave
             *
             */

        private IEnumerator SpawnEnemiesUsingPrefabs()
        {
            //if()

            yield return null;
        }

        private Vector3 RotationCalculator()
        {
            var rotation = new Vector3();

            if (mapVisualizer.directionStart == Direction.Left)
            {
                //Debug.Log("Start goes to left");
                rotation = new(0, 270, 0);
            }
            else if (mapVisualizer.directionStart == Direction.Right)
            {
                //Debug.Log("Start goes to right");
                rotation = new(0, 90, 0);
            }
            else if (mapVisualizer.directionStart == Direction.Up)
            {
                //Debug.Log("Start goes to up");
                rotation = new(0, 0, 0);
            }
            else if (mapVisualizer.directionStart == Direction.Down)
            {
                //Debug.Log("Start goes to down");
                rotation = new(0, 180, 0);
            }

            return rotation;
        }

        private void CreateIndicator<Tparam>(Vector3 position, GameObject prefab, Quaternion rotation = new Quaternion()) where Tparam : EnemyAbstract
        {
            var placementPosition = position + new Vector3(0, 1f, 0f);
            // Call the SetDirection method, get the rotation:

            GameObject gameObject = Instantiate(prefab, placementPosition, rotation);
            gameObject.transform.parent = parent;
            //gameObject.transform.localScale = new(0.5f, 0.5f, 0.5f);
            enemy = null;
            enemy = gameObject.GetComponent<Tparam>();

            if (enemyList == null) enemyList = new List<EnemyAbstract>();
            enemyList.Add(enemy);
            //waveSpawner.Waves.enemy;
        }

        private void SpawnUsingPrimitives()
        {
            CreateIndicator<Primitive>(startPosition, Color.red, PrimitiveType.Cylinder);
            Debug.Log("Enemy Spawned!");
        }

        //private void CreateIndicator(Vector3 position, Color color, PrimitiveType type)
        //{
        //    var element = GameObject.CreatePrimitive(type);
        //    listOfEnemies.Add(element);
        //    element.transform.position = position + new Vector3(.5f, .5f, .5f);
        //    element.transform.parent = parent;
        //    var renderer = element.GetComponent<Renderer>();
        //    renderer.material.SetColor("_Color", color);
        //}
        private void CreateIndicator<Tparam>(Vector3 position, Color color, PrimitiveType type) where Tparam : EnemyAbstract
        {
            GameObject g = GameObject.CreatePrimitive(type);
            g.transform.position = position + new Vector3(.5f, .5f, .5f);
            g.transform.parent = parent;
            g.transform.localScale = new(0.5f, 0.3f, 0.5f);
            var renderer = g.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);
            EnemyAbstract enemy = null;
            enemy = g.AddComponent<Tparam>();

            if (enemyList == null) enemyList = new List<EnemyAbstract>();
            enemyList.Add(enemy);
        }

        public void GetStartAndExitLocation(Vector3 startPosition, Vector3 exitPosition)
        {
            this.startPosition = startPosition;
            this.exitPosition = exitPosition;
            //return startPosition, exitPosition;
        }

        public void GetStartRotation(Vector3 startRotation)
        {
            this.startRotation = startRotation;
        }

        public void ResetEnemies()
        {
            if (enemyList != null && enemyList.Count > 1)
            {
                //foreach (GameObject enemy in transform)
                //{
                //    Destroy(enemy);//enemy);//.GetComponent<Enemy>());
                //}
                foreach (Transform child in this.transform)
                {
                    Destroy(child.gameObject);
                }
                enemyList.Clear();
            }
        }

        //private void ActivateMovingBehaviour()
        //{
        //    if (!enemyRunCompleted)
        //    {
        //        if (enemyList.Count > 0)
        //        {
        //            foreach (var enemy in enemyList.Keys)
        //            {
        //                enemy.SetActive(true);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (var enemy in enemyList.Keys)
        //        {
        //            enemy.SetActive(false);
        //        }
        //    }
        //}

        //void Update()
        //{
        //    ActivateMovingBehaviour();
        //    if (enemyList.Count > 0) MoveOnTheGrid(data);
        //}

        private void Update()
        {
            if (EnemiesAlive > 0)
            {
                return;
            }

            if (waveIndex == waves.Length)
            {
                //gameManager.WinLevel();
                this.enabled = false;
            }

            if (countdown <= 0f)
            {
                //StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            //waveCountdownText.text = string.Format("{0:00.00}", countdown);
        }

        private IEnumerator SpawnWave()
        {
            //PlayerStats.Rounds++;

            Wave wave = waves[waveIndex];

            EnemiesAlive = wave.count;

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemies(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }

            waveIndex++;
        }
    }
}