using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform mouseVisualTransform;
    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _buildingType;
    
    private Camera _mainCamera;

    private void Awake()
    {
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buildingType = _buildingTypeList.buildingTypeList[0];
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        mouseVisualTransform.localPosition = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_buildingType.prefab, mouseVisualTransform.position, Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _buildingType = _buildingTypeList.buildingTypeList[0];
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _buildingType = _buildingTypeList.buildingTypeList[1];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseWorldPosition =  _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
