using System;
using Zenject;

public class StopCommandCreator : CommandCreatorBase<IStopCommand>
{
    [Inject] AssetsContext _context;

    protected override void specificCommandCreation(Action<IStopCommand> callback)
    {
        callback?.Invoke(_context.Inject(new StopCommand()));
    }
}
