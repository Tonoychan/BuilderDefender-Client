using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceTypeListSO", menuName = "ScriptableObjects/Resource/ResourceTypeListSO")]
public class ResourceTypeListSO : ScriptableObject
{
    public List<ResourceTypeSO> resourceTypeList;
}
