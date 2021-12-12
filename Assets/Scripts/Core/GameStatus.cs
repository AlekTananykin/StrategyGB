using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class GameStatus : MonoBehaviour, IGameStatus
{
    public IObservable<int> Status => _status;
    private Subject<int> _status = new Subject<int>();

    private void checkStatus(object status)
    {
        if (0 == FactionMember.FactionCount)
        {
            _status.OnNext(0);
        }
        else if (1 == FactionMember.FactionCount)
        {
            _status.OnNext(FactionMember.GetWinner());
        }
    }

    private void Update()
    {
        ThreadPool.QueueUserWorkItem(checkStatus);
    }
}
