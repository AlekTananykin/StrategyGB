
using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{
    [InjectAsset("Chomper")]
    private Object _unitPrefab;
    public Object UnitPrefab => _unitPrefab;
}
