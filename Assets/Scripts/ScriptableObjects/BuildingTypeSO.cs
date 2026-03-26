using UnityEngine;

[CreateAssetMenu(fileName = "BuildingTypeSO", menuName = "ScriptableObjects/Building/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
}
