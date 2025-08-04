using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private List<GameObject> _components;
    [SerializeField] private InitializedPanelLevel _level;

    public override void InstallBindings()
    {
        RegistaryPathFinding();
        RegistaryInitialized();
        RegistaryAnimaition();
        RegistaryController();
        RegistaryInventary();
        RegistaryMovement();
        RegistaryCatalog();
        RegistaryWallet();
        RegistarySystem();
        RegistaryHealth();
        RegistaryInput();
        RegistaryPanel();
    }

    private void RegistaryMovement()
    {
        Container.Bind<PlayerMovement>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryInitialized()
    {
        Container.Bind<InitializedPanelLevel>()
            .FromInstance(_level)
            .AsSingle();
    }

    private void RegistaryAnimaition()
    {
        Container.Bind<PlayerAnimation>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryController()
    {
        Container.Bind<GameplayController>()
            .FromNew()
            .AsSingle();

        Container.Bind<LevelStateController>()
            .FromNew()
            .AsSingle();
    }

    private void RegistarySystem()
    {
        Container.Bind<RaycastSystem>()
            .FromInstance(new RaycastSystem(_raycaster, _eventSystem))
            .AsSingle();
    }

    private void RegistaryInput()
    {
        Container.Bind<InputRouter>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryWallet()
    {
        Container.Bind<Wallet>()
            .FromInstance(new Wallet(_mapConfig))
            .AsSingle();

        Container.Bind<WalletView>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryInventary()
    {
        Container.Bind<Inventary>()
            .FromInstance(new Inventary(_mapConfig))
            .AsSingle();

        Container.Bind<InventaryView>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryHealth()
    {
        Container.Bind<Health>()
            .FromInstance(new Health(_mapConfig))
            .AsSingle();

        Container.Bind<HealthView>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryPathFinding()
    {
        Container.Bind<PathFinding>()
            .FromInstance(new PathFinding(_mapConfig.Map))
            .AsSingle();
    }

    private void RegistaryCatalog()
    {
        Container.Bind<ComponentCatalog>()
            .FromInstance(new ComponentCatalog(_components))
            .AsSingle();
    }

    private void RegistaryPanel()
    {
        Container.Bind<DeathPanel>()
            .FromNew()
            .AsSingle();

        Container.Bind<GameplayPanel>()
            .FromNew()
            .AsSingle();

        Container.Bind<PauseGameplayPanel>()
            .FromNew()
            .AsSingle();

        Container.Bind<WonPanel>()
            .FromNew()
            .AsSingle();
    }
}