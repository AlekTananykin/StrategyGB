
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
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
        //instantiate(command.unitprefab, new vector3(random.range(-10, 10), 0,
        //    random.range(-10, 10)),
        //    quaternion.identity, _unitsparent);

        //var instance = _diContainer.InstantiatePrefab(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,
        //    Random.Range(-10, 10)), Quaternion.identity, _unitsParent);

        var instance = _diContainer.InstantiatePrefab(command.UnitPrefab, transform.position + new Vector3(2,0,2),
        Quaternion.identity, _unitsParent);
        int factionId = GetComponent<FactionMember>().FactionId;
        instance.GetComponent<FactionMember>().SetFaction(factionId);

        var queue = instance.GetComponent<ICommandQueue>();
        var mainBuilding = GetComponent<MainBuilding>();
        queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));


    }

    public void ReceiveDamage(int amount)
    {
        if (_health <= 0.0)
            return;

        _health -= amount;

        if (_health <= 0)
            Destroy(gameObject);
    }

    public Vector3 RallyPoint;

}
