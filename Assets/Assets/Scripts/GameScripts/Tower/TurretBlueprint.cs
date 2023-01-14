using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCity.EnemyAI
{
    [System.Serializable]
    public class TurretBlueprint
    {
        public GameObject prefab;
        public int cost;

        public GameObject upgradedPrefab;
        public int upgradeCost;

        public int GetSellAmount()
        {
            return (int)(cost * 0.5);
        }
    }
}