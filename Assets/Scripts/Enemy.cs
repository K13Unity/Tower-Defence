using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform _currentTargetPosition {  get; private set; }
    private List<Transform> _wayPoints = new List<Transform>();
    private DamageDisplay _damageDisplay;
    private float _arrived = 0.1f;
    private float _turnSpeed = 5f;
    public float _speed;
    public int _health;
    public int _coins;

    public event Action<Enemy> OnEnemyDie;



    public void Init(EnemyData enemyData, DamageDisplay damageDisplay)
    {
        _speed = enemyData.Speed;
        _health = enemyData.Health;
        _coins = enemyData.Coins;
        _damageDisplay = damageDisplay;
    }


    public void SetWay(List<Transform> way)
    {
        _wayPoints = way;
        _currentTargetPosition = _wayPoints[0];
    } 

    void Update()
    {
        var direction = (_currentTargetPosition.position - transform.position).normalized;
        var distance = Vector3.Distance(transform.position, _currentTargetPosition.position);
        
        if (distance > _arrived)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _turnSpeed * Time.deltaTime);
            transform.position += direction * _speed  * Time.deltaTime;
        }
        else
        {
            transform.position = _currentTargetPosition.position;
            int _nextIndex = _wayPoints.IndexOf(_currentTargetPosition);
            _nextIndex = (_nextIndex + 1);
            _currentTargetPosition = _wayPoints[_nextIndex];
        }
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        _health -= damage;
        _damageDisplay.ShowDamage(damage, transform.position, isCritical);
        if (_health <= 0)
        {
            OnEnemyDie?.Invoke(this);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void OnFinished()
    {
        OnEnemyDie?.Invoke(this);
    }
}
