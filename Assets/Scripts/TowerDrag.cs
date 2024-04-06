using Assets.Scripts;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    private Tower _tower;
    private Camera _mainCamera;
    private BuildingsSpawner _buildingsSpawner;
    private Vector3 _towerPosition;
    private bool _isDragging = false;
    private float _dragHeight = 0.2f;
    private void Awake()
    {
        _mainCamera = Camera.main;
        _buildingsSpawner = FindObjectOfType<BuildingsSpawner>();
    }

    private void Update()
    {
        if (_isDragging)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out var position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                float newY = Mathf.Max(worldPosition.y, -0.3f);
                _tower.transform.position = new Vector3(worldPosition.x, newY - _dragHeight, worldPosition.z);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                Ray rayToGround = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
               
                if (Physics.Raycast(rayToGround, out hit, Mathf.Infinity, _layerMask))
                {
                    Tower hitTower = hit.collider.GetComponent<Tower>();
                    if (hitTower != _tower && hitTower.TowerType == _tower.TowerType && hitTower.level == _tower.level)
                    {
                        var tile = hitTower.groundTile;
                        _tower.groundTile._isFree = true;
                        _buildingsSpawner.DestroyTower(_tower, hitTower);
                        _buildingsSpawner.UpgradeBuilding( tile, hitTower.level);
                    }
                    else
                    {
                        _tower.transform.position = _towerPosition;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, _layerMask))
                {
                    _tower = hit.collider.GetComponent<Tower>();
                    if (_tower != null)
                    {
                        _towerPosition = _tower.transform.position;
                        _isDragging = true;
                    }
                }
            }
        }
    }
}

