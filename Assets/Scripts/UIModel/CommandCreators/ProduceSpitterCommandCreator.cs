using System;
using Zenject;

public class ProduceSpitterCommandCreator : CommandCreatorBase<IProduceSpitterCommand>
{
    [Inject] AssetsContext _context;
    protected override void specificCommandCreation(
        Action<IProduceSpitterCommand> callback)
    {
        callback?.Invoke(_context.Inject(new ProduceSpitterCommand()));
    }
}