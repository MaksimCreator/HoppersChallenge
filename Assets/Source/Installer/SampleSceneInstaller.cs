using UnityEngine;
using Zenject;

public class SampalSceneInstaller : MonoInstaller
{
    [SerializeField] private InitializedPanelSample _initializedPanel;
    [SerializeField] private SampleSceneLoop _loop;

    public override void InstallBindings()
    {
        RegistaryPanel();
        RegistaryGameLoop();
        RegistaryCompositRoot();
    }

    private void RegistaryCompositRoot()
    {
        Container.Bind<InitializedPanelSample>()
            .FromInstance(_initializedPanel)
            .AsSingle();
    }

    private void RegistaryPanel() 
    {
        Container.Bind<StartPanelView>()
            .FromNew()
            .AsSingle();

        Container.Bind<LevelsPanel>().
            FromNew()
            .AsSingle();

        Container.Bind<HowPlayPanel>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryGameLoop()
    {
        Container.Bind<SampleSceneLoop>()
            .FromInstance(_loop)
            .AsSingle();
    }
}