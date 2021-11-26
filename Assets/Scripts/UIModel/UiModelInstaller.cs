using UnityEngine;
using Zenject;

public class UiModelInstaller: MonoInstaller
{
    [SerializeField] private AssetsContext _legacyContext;

    public override void InstallBindings()
    {
        base.InstallBindings();
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);

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

    }
}
