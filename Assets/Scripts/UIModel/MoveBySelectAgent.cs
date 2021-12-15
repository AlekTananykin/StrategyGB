using System;
using UniRx;
using UnityEngine;
using Zenject;

public class MoveBySelectAgent : MonoBehaviour
{
    [Inject]
    private IObservable<ISelectable> _selectedValues;
    [Inject]
    private IObservable<Vector3> _groundPointsValues;

    private ISelectable _currentSelected;
    void Start()
    {
        _selectedValues.Subscribe(onChomperSelected);
        _groundPointsValues.Subscribe(onChomperMove);
    }

    private void onChomperSelected(ISelectable selected)
    {
        if (selected == _currentSelected)
            return;

        _currentSelected = selected;

        Debug.Log("new selected");
    }

    private void onChomperMove(Vector3 targetPoint)
    {
        var commandsQueue =
            (_currentSelected as Component).GetComponentInParent<ICommandQueue>();

        if (null == commandsQueue)
            return;

        commandsQueue.Clear();
        commandsQueue.EnqueueCommand(new MoveCommand(targetPoint));
    }
}