
using System;
using Zenject;

public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
{
    [Inject] AssetsContext _context;

    protected override void specificCommandCreation(Action<IMoveCommand> callback)
    {
        callback?.Invoke(_context.Inject(new MoveCommand()));
    }
}
