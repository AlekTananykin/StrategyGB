using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandButtonsPresenter : MonoBehaviour
{
    [SerializeField]private SelectableValue _selectable;
    [SerializeField] private AssetsContext _context;

    [SerializeField] private CommandButtonsView _view;
    private ISelectable _currentSelectable;

    void Start()
    {
        _selectable.OnSelected += OnSelected;
        OnSelected(_selectable.CurrentValue);
        _view.OnClick += onButtonClick;
    }

    private void onButtonClick(ICommandExecutor executor)
    {
        var unitProducer = executor as 
            CommandExecutorBase<IProduceUnitCommand>;
        if (null != unitProducer)
        {
            unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
            return;
        }

        var attack = executor as
            CommandExecutorBase<IAttackCommand>;
        if (null != attack)
        {
            attack.ExecuteSpecificCommand(_context.Inject(new AttackCommand()));
            return;
        }

        var stop = executor as
            CommandExecutorBase<IStopCommand>;
        if (null != stop)
        {
            stop.ExecuteSpecificCommand(_context.Inject(new StopCommand()));
            return;
        }

        var move = executor as
            CommandExecutorBase<IMoveCommand>;
        if (null != move)
        {
            move.ExecuteSpecificCommand(_context.Inject(new MoveCommand()));
            return;
        }

        var patrol = executor as
            CommandExecutorBase<IPatrolCommand>;
        if (null != patrol)
        {
            patrol.ExecuteSpecificCommand(_context.Inject(new PatrolCommand()));
            return;
        }

        throw new ApplicationException("unknown command!");
    }

    private void OnSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
            return;

        _currentSelectable = selectable;
        _view.Clear();

        if (null != selectable)
        {
            var commandExecutors = new List<ICommandExecutor>();
            Component selectedComponent = (selectable as Component);

            commandExecutors.AddRange(selectedComponent.gameObject.GetComponentsInParent<ICommandExecutor>());
            _view.MakeLayout(commandExecutors);
        }
    }

}
