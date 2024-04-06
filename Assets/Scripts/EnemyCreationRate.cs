using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreationRate : MonoBehaviour
{
    [SerializeField] private float _lastCreationTime = 2f;
    [SerializeField] private float _intervalTime = 0;
    


    void Update()
    {
        ReverseTimeFlow();
    }

    void ReverseTimeFlow()
    {
        _intervalTime += Time.deltaTime;
        if (_intervalTime >= _lastCreationTime)
        {
            NextEnemy();
            _intervalTime = 0;
        }
    }

    private void NextEnemy()
    {
        EnemySpawner.Instance.CreatingEnemy();
    }
}
