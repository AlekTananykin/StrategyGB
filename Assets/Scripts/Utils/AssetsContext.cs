
using System;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(AssetsContext), 
    menuName ="Game/" + nameof(AssetsContext))]

public class AssetsContext : ScriptableObject
{
    public GameObject GetObjectOfType(Type targetType, string targetName = null)
    {
        for (int i = 0; i < _objects.Length; ++i)
        {
            GameObject obj = _objects[i];
            if (targetType.IsAssignableFrom(obj.GetType()))
            {
                if (null == targetName || obj.name == targetName)
                    return obj;
            }
        }
        return null;
    }
    [SerializeField] private GameObject[] _objects;
}
