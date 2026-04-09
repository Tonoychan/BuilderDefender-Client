using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    
    [SerializeField] private Transform buildingButtonTemplate;
    BuildingTypeListSO _buildingTypeListSo;
    private Dictionary<BuildingTypeSO, Transform> _buildingTypeTransformDict;
    private Transform _arrowButton;

    private void Awake()
    {
        buildingButtonTemplate.gameObject.SetActive(false);
        _buildingTypeListSo = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buildingTypeTransformDict = new Dictionary<BuildingTypeSO, Transform>();
        
        var index = 0;
        
        _arrowButton = Instantiate(buildingButtonTemplate, transform);
        _arrowButton.gameObject.SetActive(true);
        
        float offsetAmount = 180f;
        _arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index , 0);
        
        _arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        _arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(120f,80f);
        _arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        
        index++;
        
        foreach (var buildingType in _buildingTypeListSo.buildingTypeList)
        {
            var btnTransform = Instantiate(buildingButtonTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            offsetAmount = 180f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index , 0);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            _buildingTypeTransformDict[buildingType] = btnTransform;
            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuilding();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedArgs e)
    {
        UpdateActiveBuilding();
    }

    private void UpdateActiveBuilding()
    {
        _arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (var buildingType in _buildingTypeTransformDict.Keys)
        {
            var buildingTransform = _buildingTypeTransformDict[buildingType];
            buildingTransform.Find("Selected").gameObject.SetActive(false);
        }

        var activeBuilding = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuilding != null)
        {
            var activeBuildingTransform =_buildingTypeTransformDict[activeBuilding];
            activeBuildingTransform.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            _arrowButton.Find("Selected").gameObject.SetActive(true);
        }
    }
}
