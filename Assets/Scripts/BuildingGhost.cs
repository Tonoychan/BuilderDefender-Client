using System;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostBuildingSprite;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    
    private void Awake()
    {
        resourceNearbyOverlay = transform.Find("pf_ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedArgs e)
    {
        if(e.activeBuildingType ==null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
        }
    }

    private void Update()
    {
        transform.position = Utility.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        ghostBuildingSprite.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        ghostBuildingSprite.gameObject.SetActive(true);
    }

    private void Hide()
    {
        ghostBuildingSprite.gameObject.SetActive(false);
    }
}
