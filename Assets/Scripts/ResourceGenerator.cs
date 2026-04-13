using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        var collider2DArray = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionArea);
        int nearByResourceAmount = 0;
        foreach (var collider2D in collider2DArray)
        {
            var resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == _resourceGeneratorData.resourceType)
                {
                    nearByResourceAmount++;
                }
            }
        }
        
        nearByResourceAmount
             = Mathf.Clamp(nearByResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);

        if (nearByResourceAmount == 0)
        {
            enabled=false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax/2f) + 
                        _resourceGeneratorData.timerMax * 
                        (1 - (float)nearByResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }

        Debug.Log(nearByResourceAmount);
        Debug.Log(_timerMax);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timerMax;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType,1);
        }
    }
}
