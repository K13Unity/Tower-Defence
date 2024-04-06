using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _radiusDamage = 3f;

    private Enemy  _enemyCarrent;

    private int _damage;
    private int _createDamage;
    private float _rotationSpeed = 2f;
    private float _distanceThreshold = 0.2f;
    private float _createChance;
    private bool _isCritical;

    private Vector3 _targetPosition;

    private void Update()
    {
        DirectionBullet();
    }

    public void Initialize(Enemy enemyCarrent, int damag, int createDamage, float createChance)
    {
        _targetPosition = enemyCarrent.transform.position;

        _damage = damag;
        _createDamage = createDamage;
        _createChance = createChance;
        _enemyCarrent = enemyCarrent;
        _enemyCarrent.OnEnemyDie += OnEnemyDeath;
    }

    private int CritChance()
    {
        var random = Random.Range(0f, 100f);
        if (random <= _createChance)
        {
            _damage =  _damage * +_createDamage;
            _isCritical = true;
            return _damage;

        }
        else
        {
            _isCritical = false;
            return _damage;
        }
    }

    private void DirectionBullet()
    {

        Vector3 direction = (_targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _targetPosition);
        if (_enemyCarrent == null && distance < _distanceThreshold)
        {
            DamageInRadius();
            Destroy(gameObject);
        }
        Flight(direction, distance);
    }

    private void Flight(Vector3 direction, float distance)
    {
        transform.position += direction * _speed * Time.deltaTime;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        if(_enemyCarrent != null && distance < _distanceThreshold)
        {
            CritChance();
            _enemyCarrent.TakeDamage(_damage, _isCritical);
            DamageInRadius();
            Destroy(gameObject);
        }
        
    }
    private void DamageInRadius()
    {
        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, _radiusDamage));
        foreach (Collider col in colliders)
        {
            Enemy nearbyEnemy = col.GetComponent<Enemy>();
            if (nearbyEnemy != null)
            {
                nearbyEnemy.TakeDamage(_damage, _isCritical);
            }
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnEnemyDie -= OnEnemyDeath;
        _enemyCarrent = null;
    }

    private void OnDisable()
    {
        if(_enemyCarrent != null)
        {
            _enemyCarrent.OnEnemyDie -= OnEnemyDeath;
        }
    }
}
