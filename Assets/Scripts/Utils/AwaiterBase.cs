using System;

public abstract class AwaiterBase<TAwaited>: IAwaiter<TAwaited>
{
    private Action _continuation;
    public bool IsCompleted { get; private set; }


    private TAwaited _result;

    protected void onWaitFinish(TAwaited result)
    {
        _result = result;
        IsCompleted = true;
        _continuation?.Invoke();
    }

    public void OnCompleted(Action continuation)
    {
        if (IsCompleted)
        {
            continuation?.Invoke();
        }
        else
        {
            _continuation = continuation;
        }
    }

    public TAwaited GetResult() => _result;

}
