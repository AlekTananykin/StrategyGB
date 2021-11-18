
using UnityEngine;

public class MainBuilding : MonoBehaviour, IUnitProducer
{
    [SerializeField] GameObject _unitPrefab;
    [SerializeField] Transform _unitsParent;

    public void ProduceUnit()
    {
        Instantiate(_unitPrefab, new Vector3(Random.Range(-10, 10), 0, 
            Random.Range(-10, 10)), 
            Quaternion.identity, _unitsParent);
    }
}
