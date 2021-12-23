using UnityEngine;
using Zenject;

public class SpitterBuildingCommandQueue : MonoBehaviour, ICommandQueue
{
    [Inject] CommandExecutorBase<IProduceSpitterCommand> _produceUnitCommandExecutor;

    public void Clear()
    {
    }

    public async void EnqueueCommand(object command)
    {
        await _produceUnitCommandExecutor.TryExecuteCommand(command);
    }

    public Vector3 RallyPoint { get; set; }
}