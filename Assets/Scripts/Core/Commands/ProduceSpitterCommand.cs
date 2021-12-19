using UnityEngine;

public class ProduceSpitterCommand : IProduceSpitterCommand
{
    [InjectAsset("Spitter")]
    private Object _unitPrefab;
    public Object UnitPrefab => _unitPrefab;
}