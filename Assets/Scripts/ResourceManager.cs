using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> _resourceAmountDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _resourceAmountDict =  new Dictionary<ResourceTypeSO, int>();
        var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        foreach (var resourceType in resourceTypeList.resourceTypeList)
        {
            _resourceAmountDict[resourceType] = 0;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
            AddResource(resourceTypeList.resourceTypeList[0], 2);
        }
        
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDict[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDict[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (var resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    
    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        foreach (var resourceAmount in resourceAmountArray)
        {
            _resourceAmountDict[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
