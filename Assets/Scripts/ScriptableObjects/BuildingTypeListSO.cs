using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingTypeListSO", menuName = "ScriptableObjects/BuildingTypeList")]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO>  buildingTypeList;
}
