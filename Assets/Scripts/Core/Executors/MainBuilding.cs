
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IAttackable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;

    public Sprite Icon => _icon;

    public Transform PivotPoint => transform;

    [SerializeField] 
    Transform _unitsParent;
    [SerializeField]
    private float _health = 1000;
    [SerializeField]
    private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;

    [Inject]
    DiContainer _diContainer;

    public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,
            Random.Range(-10, 10)),
            Quaternion.identity, _unitsParent);

        //_diContainer.InstantiatePrefab(innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,
        //    Random.Range(-10, 10)), Quaternion.identity, _unitsParent);

    }

}
