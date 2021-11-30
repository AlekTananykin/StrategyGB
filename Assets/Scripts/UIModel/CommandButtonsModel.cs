using System;
using UnityEngine;
using Zenject;

public class CommandButtonsModel
{
    public event Action<ICommandExecutor> OnCommandAccepted;
    public event Action OnCommandSent;
    public event Action OnCommandCancel;

    [Inject]
    private CommandCreatorBase<IProduceUnitCommand> _unitProduce;
    [Inject] 
    private CommandCreatorBase<IAttackCommand> _attack;
    [Inject] 
    private CommandCreatorBase<IStopCommand> _stop;
    [Inject] 
    private CommandCreatorBase<IMoveCommand> _move;
    [Inject] 
    private CommandCreatorBase<IPatrolCommand> _patrol;


    private bool _commandIsPending;

    public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
    {
        if (_commandIsPending)
            processOnCancel();

        _commandIsPending = true;

        OnCommandAccepted?.Invoke(commandExecutor);

        _unitProduce.ProcessCommandExecutor(commandExecutor, 
            command=> executeCommandWrapper(commandExecutor, command));

        _attack.ProcessCommandExecutor(commandExecutor,
            command => executeCommandWrapper(commandExecutor, command));

        _stop.ProcessCommandExecutor(commandExecutor,
            command => executeCommandWrapper(commandExecutor, command));
        _move.ProcessCommandExecutor(commandExecutor,
            command => executeCommandWrapper(commandExecutor, command));
        _patrol.ProcessCommandExecutor(commandExecutor,
            command => executeCommandWrapper(commandExecutor, command));
    }

    private void executeCommandWrapper(ICommandExecutor commandExecutor, ICommand command)
    {
        commandExecutor.ExecuteCommand(command);
        _commandIsPending = false;
        OnCommandSent?.Invoke();
    }

    public void OnSelectionChanged()
    {
        _commandIsPending = false;
        processOnCancel();
    }

    private void processOnCancel()
    {
        _unitProduce.ProcessCancel();
        _attack.ProcessCancel();
        _stop.ProcessCancel();
        _move.ProcessCancel();
        _patrol.ProcessCancel();

        OnCommandCancel?.Invoke();

    }
}
