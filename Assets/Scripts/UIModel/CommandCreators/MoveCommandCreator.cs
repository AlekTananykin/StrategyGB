
using System;
using UnityEngine;
using Zenject;

public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
{
    [Inject] AssetsContext _context;
    private Action<IMoveCommand> _creationCallback;

    [Inject]
    private void Init(Vector3Value groundClicks)
    {
        groundClicks.OnNewValue += onNewValue;
        
    }

    private void onNewValue(Vector3 groundClick)
    {
        _creationCallback?.Invoke(
            _context.Inject(new MoveCommand(groundClick)));

        _creationCallback = null;
    }

    protected override void specificCommandCreation(Action<IMoveCommand> callback)
    {
        _creationCallback = callback;
    }

    public override void ProcessCancel()
    {
        base.ProcessCancel();
        _creationCallback = null;
    }
}
