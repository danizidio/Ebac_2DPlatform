using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static System.Action OnSpawnEnemy;

    [SerializeField] GameObject enemy;

    private void Start()
    {
        OnSpawnEnemy?.Invoke();
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, this.transform.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        OnSpawnEnemy = SpawnEnemy;
    }
    private void OnDisable()
    {
        OnSpawnEnemy = null;
    }
}
