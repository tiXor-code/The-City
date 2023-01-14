using System.Collections;
using System.Collections.Generic;
using TheCity.EnemyAI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public EnemyAbstract enemy;

    public float XLock = 45;
    public float lockPos = 0;

    public Vector3 offset = new Vector3(0, 1f, 0.3f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(XLock, lockPos, lockPos);
        transform.position = enemy.transform.position + offset;
        transform.localScale = new(0.044f, 0.05f, 0f);
    }
}
