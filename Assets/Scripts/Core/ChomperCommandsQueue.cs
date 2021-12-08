using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ChomperCommandsQueue : MonoBehaviour, ICommandQueue
{
    [Inject] CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
    [Inject] CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;
    [Inject] CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
    [Inject] CommandExecutorBase<IStopCommand> _stopCommandExecutor;

    private ReactiveCollection<ICommand> _innerCollection 
        = new ReactiveCollection<ICommand>();

    [Inject]
    private void OnServerInitialized()
    {
        _innerCollection.ObserveAdd().Subscribe(onNewCommand).AddTo(this);
    }

    private void onNewCommand(ICommand command, int index)
    {
        if (0 == index)
            executeCommand(command);
    }

    private async void executeCommand(ICommand command)
    {
        await _moveCommandExecutor.TryExecuteCommand(command);
        await _patrolCommandExecutor.TryExecuteCommand(command);
        await _attackCommandExecutor.TryExecuteCommand(command);
        await _stopCommandExecutor.TryExecuteCommand(command);

        if (_innerCollection.Count > 0)
        {
            _innerCollection.RemoveAt(0);
        }
        checkTheQueue();
    }

    private void checkTheQueue()
    {
        if (_innerCollection.Count > 0)
            executeCommand(_innerCollection[0]);
    }

    public void EnqueueCommand(object wrapperCommand)
    {
        var command = wrapperCommand as ICommand;
        _innerCollection.Add(command);
    }

    public void Clear()
    {
        _innerCollection.Clear();
        _stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
    }
}
