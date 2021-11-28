using System;
using Zenject;

public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
{
    [Inject] AssetsContext _context;
    private Action<IAttackCommand> _creationCallback;

    [Inject]
    private void Init(AttackableValue attackClicks)
    {
        attackClicks.OnNewValue += onSelected;
    }

    private void onSelected(IAttackable attackable)
    {
        _creationCallback?.Invoke(_context.Inject(new AttackCommand(attackable)));
        _creationCallback = null;
    }

    protected override void specificCommandCreation(Action<IAttackCommand> callback)
    {
        _creationCallback = callback;
    }
}
