using Zenject;
using UnityEngine;

public class CoreInstaller : MonoInstaller
{
    [SerializeField] GameStatus _gameStatus;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
        Container.Bind<IGameStatus>().FromInstance(_gameStatus);
    }
}
