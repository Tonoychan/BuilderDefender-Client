using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    
    public event EventHandler<OnActiveBuildingTypeChangedArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Transform mouseVisualTransform;
    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _activeBuildingType;
    
    private Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null && CanSpawnBuilding(_activeBuildingType, Utility.GetMouseWorldPosition()))
            {
                Instantiate(_activeBuildingType.prefab, Utility.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

   

    public void SetActiveBuildingType(BuildingTypeSO newType)
    {
        _activeBuildingType = newType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedArgs
        {
            activeBuildingType = _activeBuildingType
        });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 spawnPosition)
    {
        var boxCollider2D= buildingType.prefab.GetComponent<BoxCollider2D>();
        var collider2DArray = Physics2D.OverlapBoxAll(spawnPosition + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0f);
        var isAreaClear = collider2DArray.Length == 0;
        if(!isAreaClear)
            return false;

        collider2DArray = Physics2D.OverlapCircleAll(spawnPosition, buildingType.minConstructionRadius);
        foreach (var collider2D in collider2DArray)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    return false;
                }
            }
        }
        
        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(spawnPosition, maxConstructionRadius);
        foreach (var collider2D in collider2DArray)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;
    }
}
