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
            if (_activeBuildingType != null)
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
}
