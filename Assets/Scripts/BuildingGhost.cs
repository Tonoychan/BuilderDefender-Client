using System;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostBuildingSprite;
    
    private void Awake()
    {
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
        }
        else
        {
            Show(e.activeBuildingType.sprite);
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
