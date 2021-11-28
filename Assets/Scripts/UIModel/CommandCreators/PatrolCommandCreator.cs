using System;
using UnityEngine;
using Zenject;

public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
{
    [Inject] AssetsContext _context;
    [Inject] SelectableValue _selectable;

    private Action<IPatrolCommand> _creationCollback;

    [Inject]
    private void Init(Vector3Value groundClicks)
    {
        groundClicks.OnNewValue += onNewValue;
    }

    private void onNewValue(Vector3 groundClick)
    {
        _creationCollback?.Invoke(
            _context.Inject(
                new PatrolCommand(_selectable.CurrentValue.PivotPoint.position,
                groundClick)));

        _creationCollback = null;
    }

    protected override void specificCommandCreation(Action<IPatrolCommand> callback)
    {
        _creationCollback = callback;
    }

    public override void ProcessCancel()
    {
        base.ProcessCancel();
        _creationCollback = null;
    }
}
