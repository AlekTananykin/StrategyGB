using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonsView : MonoBehaviour
{
    public Action<ICommandExecutor, ICommandQueue> OnClick;

    [SerializeField] private GameObject _attackButton;
    [SerializeField] private GameObject _moveButton;
    [SerializeField] private GameObject _patrolButton;
    [SerializeField] private GameObject _stopButton;
    [SerializeField] private GameObject _produceUnitButton;
    [SerializeField] private GameObject _setRellyButton;

    private Dictionary<Type, GameObject> _buttonsByExecutorType;


    public void BlockInteractions(ICommandExecutor executor)
    {
        UnblockAllInteractions();
        getButtonGameObjectByType(executor.GetType()).
            GetComponent<Selectable>().interactable = false;
    }

    private GameObject getButtonGameObjectByType(Type interactorType)
    {
        return _buttonsByExecutorType
            .Where(type => type.Key.IsAssignableFrom(interactorType))
            .First()
            .Value;
    }

    public void UnblockAllInteractions() => setInteractible(true);

    private void setInteractible(bool value)
    {
        _attackButton.GetComponent<Selectable>().interactable = value;
        _moveButton.GetComponent<Selectable>().interactable = value;
        _patrolButton.GetComponent<Selectable>().interactable = value;
        _stopButton.GetComponent<Selectable>().interactable = value;
        _produceUnitButton.GetComponent<Selectable>().interactable = value;
    }

    private void Start()
    {
        _buttonsByExecutorType = new Dictionary<Type, GameObject>();
        _buttonsByExecutorType
        .Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
        _buttonsByExecutorType
        .Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
        _buttonsByExecutorType
        .Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
        _buttonsByExecutorType
        .Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
        _buttonsByExecutorType
        .Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);

        Clear();
    }

    public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors, ICommandQueue queue)
    {
        foreach (var currentExecutor in commandExecutors)
        { 
            var buttonGameObject = getButtonGameObjectByType(
                currentExecutor.GetType());

            buttonGameObject.SetActive(true);
            var button = buttonGameObject.GetComponent<Button>();
            button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
        }
    }

    public void Clear()
    {
        foreach (var kvp in _buttonsByExecutorType)
        {
            kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
            kvp.Value.SetActive(false);
        }
    }

}
