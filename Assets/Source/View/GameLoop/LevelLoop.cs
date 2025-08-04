using Zenject;
using UnityEngine;

public class LevelLoop : GameLoop
{
    [SerializeField] private InitializedPanelLevel _panel;

    private IControl[] _controls;
    private IUpdateble[] _updatebles;

    private bool _isInit;

    [Inject]
    private void Construct(
        GameplayController levelController,
        LevelStateController levelStateController,
        InventaryView inventaryView,
        WalletView walletView,
        HealthView healthView,
        DeathPanel deathPanel,
        PauseGameplayPanel pauseGameplayPanel,
        GameplayPanel gameplayPanel,
        WonPanel wonPanel)
    {
        _controls = new IControl[]
        {
            levelController,
            levelStateController,
            inventaryView,
            walletView,
            healthView,
            deathPanel,
            pauseGameplayPanel,
            gameplayPanel,
            wonPanel
        };

        _updatebles = new IUpdateble[0];
    }

    private void Awake()
    {
        if (_isInit)
            return;

        _panel.Init();
        _isInit = false;
    }

    protected override IControl[] GetControls()
    => _controls;

    protected override IUpdateble[] GetUpdatebles()
    => _updatebles;
}