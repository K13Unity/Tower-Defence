using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [SerializeField] private List<Tower> prefab;
        [SerializeField] private List<TowerData> _towerData;
        [SerializeField] private TowerType _towerType;
        public TowerType TowerType => _towerType;


        public List<TowerData> TowerData => _towerData;
        public List<Tower> Prefab => prefab;
    }

    [Serializable]
    public class TowerData
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _createDamage;
        [SerializeField] private int _createChance;
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private float _attackSpeedMultiplier;
        [SerializeField] private float _createChanceMultiplier;
        public int Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public int CreateDamage => _createDamage;
        public int CreateChance => _createChance;
        public float DamageMultiplier => _damageMultiplier;
        public float AttackSpeedMultiplier => _attackSpeedMultiplier;
        public float CreateChanceMultiplier => _createChanceMultiplier;
    }
}

