using UnityEngine;

namespace TheCity.EnemyAI
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int count;
        public float rate;
    }
}