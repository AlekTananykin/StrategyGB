using System;
using UnityEngine;

public class ActionValue : ScriptableObject
{
    public Vector3 CurrentValue { get; set; }
    public Action<Vector3> OnNewValue;

    public void SetValue(Vector3 value)
    {
        CurrentValue = value;
        OnNewValue?.Invoke(value);
    }
}
