using System;
using UnityEngine;
using Zenject;
using UniRx;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Game/Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackableClicksRMB;
    [SerializeField] private SelectableValue _selectables;

    public override void InstallBindings()
    {
        Container.BindInstances(_legacyContext, _groundClicksRMB,
            _attackableClicksRMB, _selectables);

        Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
            .To<ProduceUnitCommandCreator>().AsTransient();
        
        Container.Bind<CommandCreatorBase<IAttackCommand>>()
            .To<AttackCommandCreator>().AsTransient();

        Container.Bind<CommandCreatorBase<IMoveCommand>>()
            .To<MoveCommandCreator>().AsTransient();

        Container.Bind<CommandCreatorBase<IPatrolCommand>>()
            .To<PatrolCommandCreator>().AsTransient();

        Container.Bind<CommandCreatorBase<IStopCommand>>()
            .To<StopCommandCreator>().AsTransient();

        Container.Bind<CommandButtonsModel>().AsTransient();


        Container.Bind<IAwaitable<IAttackable>>()
            .FromInstance(_attackableClicksRMB);
        Container.Bind<IAwaitable<Vector3>>()
            .FromInstance(_groundClicksRMB);

        Container.Bind<IObservable<ISelectable>>()
            .FromInstance(_selectables);
    }
}