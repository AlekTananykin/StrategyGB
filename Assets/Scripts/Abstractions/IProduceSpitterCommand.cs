using UnityEngine;

public interface IProduceSpitterCommand : ICommand
{
    Object UnitPrefab { get; }
}
