using System;
using UnityEngine;

public class ActionValueBase<T> : ScriptableObject, IAwaitable<T>
{
    public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        private readonly ActionValueBase<TAwaited> 
            _scriptableObjectValueBase;

        public NewValueNotifier(
            ActionValueBase<TAwaited> scriptableObjectValueBase)
        {
            _scriptableObjectValueBase = scriptableObjectValueBase;
            _scriptableObjectValueBase.OnNewValue += onNewValue;
        }

        private void onNewValue(TAwaited result)
        {
            _scriptableObjectValueBase.OnNewValue -= onNewValue;
            onWaitFinish(result);
        }
    }

    public T CurrentValue { get; private set; }
    public Action<T> OnNewValue;
    public virtual void SetValue(T value)
    {
        CurrentValue = value;
        OnNewValue?.Invoke(value);
    }

    public IAwaiter<T> GetAwaiter()
    {
        return new NewValueNotifier<T>(this);
    }
}
