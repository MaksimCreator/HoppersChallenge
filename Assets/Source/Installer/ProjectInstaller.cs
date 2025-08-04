using Zenject;
using UnityEngine;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AnimationConfig _animationConfig;

    public override void InstallBindings()
    {
        RegistaryGameData();
        RegistarySO();
    }

    private void RegistaryGameData()
    {
        Container.Bind<GameData>()
            .FromNew()
            .AsSingle();
    }

    private void RegistarySO()
    {
        Container.Bind<AnimationConfig>()
            .FromInstance(_animationConfig)
            .AsSingle();
    }
}