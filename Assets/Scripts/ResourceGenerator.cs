using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData,Vector3 position)
    {
        var collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionArea);
        int nearByResourceAmount = 0;
        foreach (var collider2D in collider2DArray)
        {
            var resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearByResourceAmount++;
                }
            }
        }
        
        nearByResourceAmount
            = Mathf.Clamp(nearByResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        
        return nearByResourceAmount;
    }

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
        int nearByResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData, transform.position);
        
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

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }
    
    public float GetTimerNormalized()
    {
        return _timer / _timerMax;
    }

    public float GetAmountGeneratedPerSec()
    {
        return 1 / _timerMax;
    }
}
