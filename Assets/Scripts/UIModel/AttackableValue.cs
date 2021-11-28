using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Game/" +
nameof(AttackableValue), order = 0)]

public class AttackableValue : ScriptableObject
{
    public IAttackable CurrentValue { get; private set; }
    public Action<IAttackable> OnSelected;
    public void SetValue(IAttackable value)
    {
        CurrentValue = value;
        OnSelected?.Invoke(value);
    }
}
