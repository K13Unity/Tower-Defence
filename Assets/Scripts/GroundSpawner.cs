using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GroundTile _groundTilePrefab;
    List<GroundTile> _tiles = new();

    private Vector3 _nextSpawnPoint;

    int _rows = 3;
    int _tilesPerRow = 5;



    // знайти випадкову вільну плитку і повернути її
    public GroundTile GetFreeTile()
    {
        while (_tiles.Any(tile => tile._isFree == true))
        {
            var randomIndex = Random.Range(0, _tiles.Count);
            if (_tiles[randomIndex]._isFree == true)
            {
                return _tiles[randomIndex];
            }
        }
        return null;
    }
    

    public void LayOutTiles()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _tilesPerRow; col++)
            {
                CreateTile();
            }

            _nextSpawnPoint.z += _groundTilePrefab.transform.localScale.z;
            _nextSpawnPoint.x = 0f;
        }
    }

    private void CreateTile()
    {
        var tile = Instantiate(_groundTilePrefab, _nextSpawnPoint, Quaternion.identity);
        _nextSpawnPoint.x += _groundTilePrefab.transform.localScale.x;
        _tiles.Add(tile);
    }
}
