using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private TimeManagerWave timeManagerWave;
    [SerializeField] private TriggerEnemy _triggerEnemy;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private DamageDisplay _damageDisplay;
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private EnemyConfig _miniBossConfig;
    [SerializeField] private EnemyConfig _bossConfig;
    [SerializeField] private Enemy _enemyPref;
    [SerializeField] private Enemy _miniBossPref;
    [SerializeField] private Enemy _bossPref;

    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _lastCreationTime = 2f;
    [SerializeField] private float _intervalTime = 0;

    private Enemy _miniBoss;
    private Enemy _boss;
    private EnemyData _enemyData;
    private EnemyData _miniBossData;
    private EnemyData _bossData;

    private bool _isSpawning = false;
    private int _enemyCounter = 0;

    public Queue<Enemy> EnemyQueue = new Queue<Enemy>();



    private void Awake()
    {
        Instance = this;
        _enemyData = _enemyConfig.EnemyData;
        _miniBossData = _miniBossConfig.EnemyData;
        _bossData = _bossConfig.EnemyData;
    }


    void Update()
    {
        if (_isSpawning)
        {
            _intervalTime += Time.deltaTime;
            if (_intervalTime >= _lastCreationTime)
            {
                CreatingEnemy();
                _intervalTime = 0;
            }
        }
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        
        enemy.OnEnemyDie -= OnEnemyDeath;
        if(_miniBoss != null && enemy == _miniBoss)
        {
            _miniBoss = null;
            _miniBossData = new EnemyData(_miniBossData.Speed, (int)(_miniBossData.Health * 1.5f), _miniBossData.Coins);
        }
        if(_boss != null && enemy == _boss)
        {
            timeManagerWave.StartWave();
            _boss = null;
            _bossData = new EnemyData(_bossData.Speed, (int)(_bossData.Health * 2.5f), _bossData.Coins);

        }
        enemy.Die();
        if (EnemyQueue.Count > 0)
        {
            EnemyQueue.Dequeue();
        }
    }

    private void OnEnable()
    {
        timeManagerWave.onWaveStart += OnWaveStart;
        timeManagerWave.onWaveEnd += OnWaveEnd;
    }

    private void OnDisable()
    {
        while (EnemyQueue.Count > 0)
        {
            EnemyQueue.Dequeue().OnEnemyDie -= OnEnemyDeath;
        }
        timeManagerWave.onWaveStart -= OnWaveStart;
        timeManagerWave.onWaveEnd -= OnWaveEnd;
    }

    public void CreatingEnemy() 
    {
        if (_enemyCounter >= 30 && _enemyCounter % 30 == 0)
        {
            CreateMiniBoss();
        }
        else
        {
            CreateNormalEnemy();
        }
    }

    private void CreateNormalEnemy()
    {
        Enemy prefab = _enemyPref;
        EnemyConfig enemyConfig = _enemyConfig;
        _enemyCounter++;

        var _enemy = Instantiate(prefab);
        _enemy.Init(_enemyData, _damageDisplay);
        _enemy.transform.position = transform.position;
        _enemy.SetWay(_wayPoints);
       // EnemyQueue.Add(_enemy);
        _enemy.OnEnemyDie += OnEnemyDeath;
        _enemy.OnEnemyDie += _coinManager.OnEnemyDeath;
    }

    private void CreateMiniBoss()
    {
        Enemy prefab = _miniBossPref;
        EnemyConfig enemyConfig = _miniBossConfig;
        _enemyCounter++;

        var _enemy = Instantiate(prefab);
        _enemy.Init(_miniBossData, _damageDisplay);
        _enemy.transform.position = transform.position;
        _enemy.SetWay(_wayPoints);
       // EnemyQueue.Add(_enemy);
        _enemy.OnEnemyDie += OnEnemyDeath;
        _enemy.OnEnemyDie += _coinManager.OnEnemyDeath;
        _miniBoss = _enemy;
        _enemyData = new EnemyData(_enemyData.Speed, (int)(_enemyData.Health * 1.5f), _enemyData.Coins);
    }

    public void CreateBoss()
    {
        var _enemyBoss = Instantiate(_bossPref);
        _enemyBoss.Init(_bossData, _damageDisplay);
        _enemyBoss.transform.position = transform.position;
        _enemyBoss.SetWay(_wayPoints);
        //EnemyQueue.Add(_enemyBoss);
        _enemyBoss.OnEnemyDie += OnEnemyDeath;
        _enemyBoss.OnEnemyDie += _coinManager.OnEnemyDeath;
        _boss = _enemyBoss;
    }

    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }


    private void OnWaveStart()
    {
        StartSpawning();
    }

    private void OnWaveEnd()
    {
        StopSpawning();
        foreach (Enemy enemy in EnemyQueue)
        {
            enemy.Die(); 
        }
        EnemyQueue.Clear();
        CreateBoss();
    }
    
}
