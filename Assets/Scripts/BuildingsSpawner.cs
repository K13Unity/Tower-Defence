using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingsSpawner : MonoBehaviour
{
    [SerializeField] private GroundSpawner _groundSpawner;
    [SerializeField] private List<TowerConfig> _towerConfig;
    [SerializeField] private List<UpgradeTowerbtn> _upgradeTowerbtn;
    [SerializeField] private CoinManager _coinManager;
    
    public List<TowerConfig> TowerConfig => _towerConfig;
    private List<Tower> _towers = new List<Tower>();
    private Dictionary<TowerType, int> _clickCount = new Dictionary<TowerType, int>();



    public void BildBuilding()
    {
        int random = Random.Range(0, _towerConfig.Count);
        TowerConfig towerConfig = _towerConfig[random];
        var tile = _groundSpawner.GetFreeTile();
        if (tile != null)
        {
            var building = Instantiate(towerConfig.Prefab[0]);
            building.transform.position = new Vector3 (
                tile.transform.position.x, 
                tile.transform.position.y, 
                tile.transform.position.z);
            tile._isFree = false;
            building.Init(towerConfig.TowerData[0], tile, 0);
            _towers.Add(building);
            if(_clickCount.ContainsKey(building.TowerType))
            {
                for (int i = 0; i < _clickCount[building.TowerType]; i++)
                {
                    building.UpgradeTower();
                }
            }
        }
    }

    public void UpgradeBuilding(GroundTile tile, int level)
    {
        var randomType = Random.Range(0, _towerConfig.Count);
        var towerConfig = _towerConfig[randomType];
        if (towerConfig != null)
        {
            if(level < towerConfig.TowerData.Count -1)
            {
                level++;
                var newBuilding = Instantiate(towerConfig.Prefab[level]);
                newBuilding.transform.position = new Vector3(
                     tile.transform.position.x,
                     tile.transform.position.y,
                     tile.transform.position.z);
                newBuilding.Init(towerConfig.TowerData[level], tile, level);
                _towers.Add(newBuilding);
                if (_clickCount.ContainsKey(newBuilding.TowerType))
                {
                    for (int i = 0; i < _clickCount[newBuilding.TowerType]; i++)
                    {
                        newBuilding.UpgradeTower();
                    }
                }
            }
        }
    }

    public void DestroyTower(Tower tower, Tower hitTower)
    {
        Destroy(tower.gameObject);
        Destroy(hitTower.gameObject);
    }

    private void OnEnable()
    {
        foreach (var btn in _upgradeTowerbtn)
        {
            btn.onButtonDown += OnUpdateClick;
        }
    }

    private void OnUpdateClick(TowerType towerType)
    {
        UpdateClickCount(towerType);
        foreach (var tower in _towers)
        {
            if (tower.TowerType == towerType)
            {
                 tower.UpgradeTower();
            }
        }
    }
    private void UpdateClickCount(TowerType towerType)
    {
        if (_clickCount.ContainsKey(towerType))
        {
             _clickCount[towerType]++;
        }
        else
        {
            _clickCount.Add(towerType, 1);
        }
    }

    private void OnDisable()
    {
        foreach (var btn in _upgradeTowerbtn)
        {
            btn.onButtonDown -= OnUpdateClick;
        }
    }
}
