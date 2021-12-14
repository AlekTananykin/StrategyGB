
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, 
    ISelectable, IAttackable
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

    [SerializeField] private Vector3 _rallyPoint;
    public Vector3 RallyPoint 
    { 
        get { return _rallyPoint; } 
        set { _rallyPoint = value; } 
    }

    public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        var instance = _diContainer.InstantiatePrefab(command.UnitPrefab, transform.position,
        Quaternion.identity, _unitsParent);
        
        var factionMember = instance.GetComponent<FactionMember>();
        factionMember.SetFaction(GetComponent<FactionMember>().FactionId);


        var queue = instance.GetComponent<ICommandQueue>();
        var mainBuilding = GetComponent<MainBuilding>();
        queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));


    }

    public void RecieveDamage(int amount)
    {
        if( _health <= 0)
        {
            return;
        }
        _health += amount;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
