using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform mouseVisualTransform;
    [SerializeField] private Transform pfWoodHarvester;
    
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        mouseVisualTransform.localPosition = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(pfWoodHarvester, mouseVisualTransform.position, Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseWorldPosition =  _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
