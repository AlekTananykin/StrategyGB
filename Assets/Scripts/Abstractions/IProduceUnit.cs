using UnityEngine;

public interface IProduceUnitCommand: ICommand
{
    Object UnitPrefab { get; }
}
