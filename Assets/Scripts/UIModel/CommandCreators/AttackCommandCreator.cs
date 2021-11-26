using System;
using Zenject;

public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
{
    [Inject] AssetsContext _context;

    protected override void specificCommandCreation(Action<IAttackCommand> callback)
    {
        callback?.Invoke(new AttackCommand());
    }
}
