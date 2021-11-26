using System;
using Zenject;

public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
{
    [Inject] AssetsContext _context;
    protected override void specificCommandCreation(Action<IPatrolCommand> callback)
    {
        callback?.Invoke(_context.Inject(new PatrolCommand()));
    }
}
