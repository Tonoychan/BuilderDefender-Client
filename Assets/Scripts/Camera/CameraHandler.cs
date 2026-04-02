using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float zoomAmountSpeed = 20f;
    [SerializeField] private float zoomSpeed = 20f;
    
    [SerializeField] private float minZoomOrthographicSize = 1f;
    [SerializeField] private float maxZoomOrthographicSize = 100f;
    
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    
    private float orthographicSize;
    private float targetOrthographicSize;

    private void Start()
    {
        orthographicSize = _cinemachineCamera.Lens.OrthographicSize;
        targetOrthographicSize =  orthographicSize;
    }

    private void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        var moveDirection = new Vector3(x, y,0f).normalized;
        
        transform.position +=  moveDirection * (moveSpeed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        targetOrthographicSize +=Input.mouseScrollDelta.y * zoomAmountSpeed ; 
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minZoomOrthographicSize, maxZoomOrthographicSize);
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        _cinemachineCamera.Lens.OrthographicSize = orthographicSize;
    }
}
