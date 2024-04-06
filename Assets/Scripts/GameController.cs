using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController Instance {  get; private set; }
    [SerializeField] private Life—ounterUI _lifeSlider;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GroundSpawner _groundSpawner;

    private int _number—urrentEnemies = 0;
    private int _maxNumberEnemies = 3;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _groundSpawner.LayOutTiles();
    }
    

    public void EnemyCounter(Enemy enemy)
    {
        if(_number—urrentEnemies != _maxNumberEnemies)
        {
            _number—urrentEnemies++; 
            UpdateScoreImage(_number—urrentEnemies);
        }
        else
        {

        }
    }

    public void UpdateScoreImage(int value)
    {
        _lifeSlider.value = _number—urrentEnemies;
    }
    
}
