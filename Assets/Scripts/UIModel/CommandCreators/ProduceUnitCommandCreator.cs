using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
    [Inject] AssetsContext _context;
    protected override void specificCommandCreation(
        Action<IProduceUnitCommand> callback)
    {
        callback?.Invoke(_context.Inject(new ProduceUnitCommand()));
    }
}
