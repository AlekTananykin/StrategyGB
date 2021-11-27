using System;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(Vector3Value), menuName ="Game/" + nameof(Vector3Value))]
public class Vector3Value : ScriptableObject
{
    public Vector3 CurrentValue { get; set; }
    public Action<Vector3> OnNewValue;

    public void SetValue(Vector3 value)
    {
        CurrentValue = value;
        OnNewValue?.Invoke(value);
    }

}
