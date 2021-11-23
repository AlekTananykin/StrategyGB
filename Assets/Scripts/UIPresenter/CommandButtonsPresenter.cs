using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandButtonsPresenter : MonoBehaviour
{
    [SerializeField]private SelectableValue _selectable;

    [SerializeField] private CommandButtonsView _view;
    private ISelectable _currentSelectable;

    void Start()
    {
        _selectable.OnSelected += onSelected;
        onSelected(_selectable.CurrentValue);
        _view.OnClick += onButtonClick;
    }

    private void onButtonClick(ICommandExecutor executor)
    {
        var unitProducer = executor as 
            CommandExecutorBase<IProduceUnitCommand>;

        if (null != unitProducer)
        {
            unitProducer.ExecuteSpecificCommand(new ProduceUnitCommand());
            return;
        }

        throw new ApplicationException("unknown command!");
    }

    private void onSelected(ISelectable selectable)
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
