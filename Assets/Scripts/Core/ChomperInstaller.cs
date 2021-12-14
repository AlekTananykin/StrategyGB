using Zenject;

public class ChomperInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var executors = gameObject.GetComponentsInChildren<ICommandExecutor>();
        foreach (var executor in executors)
        {
            var baseType = executor.GetType().BaseType;
            Container.Bind(baseType).FromInstance(executor);
        }

        Container.Bind<IHealthHolder>().FromComponentInChildren();
        Container.Bind<float>().WithId("AttackDistance").FromInstance(5f);
        Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);
    }
}
