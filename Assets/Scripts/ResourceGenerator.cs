using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingType;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = _buildingType.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax;
            ResourceManager.Instance.AddResource(_buildingType.resourceGeneratorData.resourceType,1);
        }
    }
}
