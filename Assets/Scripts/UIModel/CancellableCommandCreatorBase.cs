
using System;
using System.Threading;
using Zenject;

public abstract class CancellableCommandCreatorBase<TCommand, TArgument>:
    CommandCreatorBase<TCommand> where TCommand: ICommand
{
    [Inject] private AssetsContext _context;
    [Inject] private IAwaitable<TArgument> _awatableArgument;

    private CancellationTokenSource _ctSource;

    protected override async 
        void specificCommandCreation(Action<TCommand> callback)
    {
        _ctSource = new CancellationTokenSource();
        try
        {
            var argument = await _awatableArgument.WithCancellation(_ctSource.Token);
            callback?.Invoke(_context.Inject(createCommand(argument)));
        }
        catch { }
    }

    protected abstract TCommand createCommand(TArgument argument);

    public override void ProcessCancel()
    {
        base.ProcessCancel();
        if (null == _ctSource)
            return;

        _ctSource.Cancel();
        _ctSource.Dispose();
        _ctSource = null;
    }

}
