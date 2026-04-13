using System;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    public void Show(ResourceGeneratorData data)
    {
        resourceGeneratorData = data;
        gameObject.SetActive(true);
        
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }
}
