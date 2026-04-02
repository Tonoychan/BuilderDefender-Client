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

    private void TestLog()
    {
        foreach (var resourceType in _resourceAmountDict.Keys)
        {
            Debug.Log(resourceType +": "+ _resourceAmountDict[resourceType]);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
            AddResource(resourceTypeList.resourceTypeList[0], 2);
            TestLog();
        }
        
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDict[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        TestLog();
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDict[resourceType];
    }
}
