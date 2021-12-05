using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class CommandButtonsPresenter : MonoBehaviour
{
    [Inject]
    private IObservable<ISelectable> _selectedValues;

    [SerializeField] private CommandButtonsView _view;
    [Inject] 
    private CommandButtonsModel _model;

    private ISelectable _currentSelectable;

    void Start()
    {
        _selectedValues.Subscribe(onSelected);
        
        _view.OnClick += _model.OnCommandButtonClicked;

        _model.OnCommandSent += _view.UnblockAllInteractions;
        _model.OnCommandCancel += _view.UnblockAllInteractions;
        _model.OnCommandAccepted += _view.BlockInteractions;
    }


    private void onSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
            return;

        if (null != _currentSelectable)
            _model.OnSelectionChanged();

        _currentSelectable = selectable;
        _view.Clear();

        if (null != selectable)
        {
            var commandExecutors = new List<ICommandExecutor>();
            Component selectedComponent = (selectable as Component);

            if (null == selectedComponent)
                throw new NullReferenceException("selectable is not Component");

            commandExecutors.AddRange(
                selectedComponent.gameObject.GetComponentsInParent<ICommandExecutor>());
            _view.MakeLayout(commandExecutors);
        }
    }

}
