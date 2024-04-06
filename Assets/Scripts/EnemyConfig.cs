using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyData _enemyData;
        public EnemyData EnemyData => _enemyData;

    }


    [Serializable]
    public struct EnemyData
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private int _coins;

        public float Speed => _speed;
        public int Health => _health;
        public int Coins => _coins;
        public EnemyData(float speed, int health, int coins)
        {
            _speed = speed;
            _health = health;
            _coins = coins;
        }
    }
}

