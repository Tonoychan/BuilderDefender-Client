using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

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
        mouseVisualTransform.localPosition = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null)
            {
                Instantiate(_activeBuildingType.prefab, mouseVisualTransform.position, Quaternion.identity);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseWorldPosition =  _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO newType)
    {
        _activeBuildingType = newType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }
}
