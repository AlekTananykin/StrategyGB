using System;
using UnityEngine;

public abstract class CommandCreatorBase<T> where T: ICommand
{
    public ICommandExecutor ProcessCommandExecutor(
        ICommandExecutor commandExecutor, Action<T> callback)
    {
        var specificExecutor = commandExecutor as CommandExecutorBase<T>;
        if (null == specificExecutor)
            specificCommandCreation(callback);

        return commandExecutor;
    }

    abstract protected void specificCommandCreation(Action<T> callback);

    public virtual void ProcessCancel() { }
}
