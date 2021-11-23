
using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{
    [SerializeField]
    private GameObject _unitPrefab;

    public GameObject UnitPrefab => _unitPrefab;
}
