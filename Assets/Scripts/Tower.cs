using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _timeSinceLastShot = 0f;
    [SerializeField] private TowerType _towerType;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _shotDelay;
    public GroundTile groundTile { get; private set; }
    private TowerData _towerData;


    public TowerType TowerType => _towerType;
    public int damage;
    public float creatChance;
    public int createDamage;
    public float attackSpeed;
    internal bool raycastTarget;
    public int level { get; private set; }


    private void Update()
    {
        LookTowardsEnemy();
        _timeSinceLastShot += Time.deltaTime;
        if (EnemySpawner.Instance.EnemyQueue.Count > 0)
        {
            ShotTimeInterval();
        }
    }

    public void ShotTimeInterval()
    {

        if (_timeSinceLastShot >= 1/ attackSpeed)
        {
            ShotAnimation();
            _timeSinceLastShot = 0f;
        }
    }
    private async void ShotAnimation()
    {
        _animator.SetTrigger("isShot");
        _animator.SetFloat("SpeedMultiplier",  attackSpeed);
        await UniTask.Delay(_shotDelay);
        if (EnemySpawner.Instance.EnemyQueue.Count > 0)
        {
            Shot();
        }
    }

    private void Shot()
    {
        if (EnemySpawner.Instance.EnemyQueue.Count > 0)
        {
            Enemy targetEnemy = EnemySpawner.Instance.EnemyQueue.Peek();
            if (targetEnemy != null)
            {

                var bullet = Instantiate(_bulletPrefab);
            if(_firePoint != null)
            {
                bullet.transform.position = _firePoint.transform.position;
                bullet.transform.rotation = _firePoint.transform.rotation;
                bullet.Initialize(EnemySpawner.Instance.EnemyQueue.Peek(), damage, createDamage, creatChance);
            }
            else
            {
                Destroy(bullet.gameObject);
            }
            }
           
        }
    }

    private void LookTowardsEnemy()
    {

        if (EnemySpawner.Instance.EnemyQueue.Count > 0 && EnemySpawner.Instance.EnemyQueue.Peek() != null)
        {
            transform.LookAt(EnemySpawner.Instance.EnemyQueue.Peek().transform.position);
        }
    }

    public void Init(TowerData towerData, GroundTile tile, int lvl)
    {
        damage = towerData.Damage;
        createDamage = towerData.CreateDamage;
        creatChance = towerData.CreateChance;
        attackSpeed = towerData.AttackSpeed;
        _towerData = towerData;
        groundTile = tile;
        level = lvl;
    }

    public void UpgradeTower()
    {
        damage =  ((int)(damage * _towerData.DamageMultiplier));
        attackSpeed = attackSpeed * _towerData.AttackSpeedMultiplier;
        creatChance = creatChance * _towerData.CreateChanceMultiplier;
        if (_animator != null)
        {
            _animator.SetFloat("SpeedMultiplier", attackSpeed);
        }
        _shotDelay = Mathf.RoundToInt(_shotDelay / _towerData.AttackSpeedMultiplier);
    }
    private void OnEnable()
    {
        TriggerEnemy.OnEnemyTrigger += UpdateEnemyQueue;
    }

    private void UpdateEnemyQueue(Enemy enemy)
    {
        if (!EnemySpawner.Instance.EnemyQueue.Contains(enemy))
        {
            EnemySpawner.Instance.EnemyQueue.Enqueue(enemy);
        }
    }

    private void OnDisable()
    {
        TriggerEnemy.OnEnemyTrigger -= UpdateEnemyQueue;
    }
}
