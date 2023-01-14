using UnityEngine;

namespace TheCity.EnemyAI
{
    public class EnemyFactory : MonoBehaviour
    {
        internal GameObject SpawnEnemies(GameObject prefab, Vector3 placementPosition, Quaternion rotation)
        {
            //for (int i = 0; i < numberOfEnemies; i++)
            //{
            EnemyAbstract<Enemy> enemy = new EnemyAbstract<Enemy>("Enemy");
            enemy.ScriptComponent.Initialize(
                position: new(0, 0, 0),
                speed: 2,
                health: 100,
                damage: 1,
                worth: 10,
                nextPathCellIndex: 0
                ); ;
            var element = Instantiate(prefab, placementPosition, rotation);
            return element;
            //}
        }
    }
}