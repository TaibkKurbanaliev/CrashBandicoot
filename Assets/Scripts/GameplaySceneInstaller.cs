using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputSystem_Actions>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
    }
}