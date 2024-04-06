using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{

    public static event Action<Enemy> OnEnemyTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            EnemySpawner.Instance.EnemyQueue.Enqueue(enemyComponent);
            OnEnemyTrigger?.Invoke(enemyComponent);
        }
    }
}
