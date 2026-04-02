using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements.InputSystem;

public class ResourcesUI : MonoBehaviour
{
   [SerializeField] private Transform resourceTemplate;
   
   ResourceTypeListSO resourceTypeList;
   private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDict;
   private void Awake()
   {
      resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
      resourceTemplate.gameObject.SetActive(false);
      
      resourceTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();

      var index = 0;
      foreach (var resourceType in resourceTypeList.resourceTypeList)
      {
         Transform clonedResourceTransform = Instantiate(resourceTemplate,transform);
         clonedResourceTransform.gameObject.SetActive(true);
         
         var offsetAmount = -160f;
         clonedResourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
         clonedResourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
         resourceTypeTransformDict[resourceType] = clonedResourceTransform;
         
         index++;
      }
   }

   private void Start()
   {
      ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
      UpdateResourceAmount();
   }
   
   private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs args)
   {
      UpdateResourceAmount();
   }

   private void UpdateResourceAmount()
   {
      foreach (var resourceType in resourceTypeList.resourceTypeList)
      {
         Transform resourceTransform = resourceTypeTransformDict[resourceType];
         int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
         resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = resourceAmount.ToString();
      }
   }
}
