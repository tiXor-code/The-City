using TheCity.GameMaster;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.EnemyAI
{
    public class EnemyAbstract<T> where T : EnemyAbstract
    {
        public GameObject gameObject;
        public T ScriptComponent;

        public EnemyAbstract(string name)
        {
            gameObject = new GameObject(name);
            ScriptComponent = gameObject.AddComponent<T>();
        }
    }

    public enum EnemyDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    public abstract class EnemyAbstract : MonoBehaviour
    {
        //[HideInInspector, SerializeField, Range(0.1f, 10f)]
        public float speed = 2f;

        public float startHealth;
        protected float health;

        public float damage = 1;

        public int worth = 50;

        public GameObject deathEffect;

        [HideInInspector]
        public Vector3 position;
        [HideInInspector]
        public int nextPathCellIndex;

        private bool isDead = false;
        
        [HideInInspector]
        public float startSpeed = 2f;

        private EnemyDirection enemyDirection;

        [Header("Unity Stuff")]
        public Image healthBar;      

        public EnemyDirection Direction { get => enemyDirection; set => enemyDirection = value; }

        void Start()
        {
            health = startHealth;
        }

        private void Awake()
        {
            if (gameObject != GameObject.Find("EnemyGenerator")) gameObject.tag = "Enemy";
        }
        
        public void Initialize(Vector3 position, float speed, float health, float damage, int worth, int nextPathCellIndex)
        {
            this.position = position;
            this.speed = speed;
            this.health = health;
            this.damage = damage;
            this.worth = worth;
            this.nextPathCellIndex = nextPathCellIndex;
        }

        public void TakeDamage(int amount)
        {
            health -= amount;

            healthBar.fillAmount = health / startHealth;

            Debug.Log("This enemy took " + amount + " of damage. HP left: " + health);  

            //healthBar.fillAmount = health / startHealth;

            if (health <= 0 && !isDead)
            {
                Kill();
            }
        }

        public void Slow(float pct)
        {
            speed = startSpeed * (1f - pct);
        }

        private void Kill()
        {
            isDead = true;

            PlayerStats.Money += worth;
            
            WaveManager.EnemiesAlive--;

            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Destroy(gameObject);

            //WaveManager.enemyList.Remove(this);
        }
        
        private void Die()
        {
            // Reach the end of the path

            isDead = true;
            
            WaveManager.EnemiesAlive--;

            Destroy(gameObject);

            //WaveManager.enemyList.Remove(this);
        }

        public void EndPath()
        {
            PlayerStats.Lives--;
            Die();
        }

        public bool IsEnemyValid()
        {
            if (this == null) return false;
            return true;
        }

        public EnemyAbstract GetEnemy()
        {
            if (IsEnemyValid() == false) return null;
            return this;
        }
    }

    public class Enemy : EnemyAbstract
    {
        private void Start()
        {
            //speed = 2;
            //startHealth = 100;
            //damage = 1;
            //worth = 50;
            health = startHealth;

            //deathEffect = 
        }
    }

    public class Primitive : EnemyAbstract
    {
        private void Start()
        {
            speed = 4;
            startHealth = 50f;
            damage = 2;
            worth = 20;
        }
    }
}