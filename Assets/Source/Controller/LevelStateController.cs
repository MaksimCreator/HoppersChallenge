using System;
using Zenject;

public class LevelStateController : IControl
{
    private readonly GameplayPanel _gameplayPanel;
    private readonly DeathPanel _deathPanel;
    private readonly GameData _gameData;
    private readonly WonPanel _wonPanel;
    private readonly Health _health;
    private readonly Wallet _wallet;

    [Inject]
    public LevelStateController(Health health,Wallet wallet, DeathPanel deathPanel,GameplayPanel gameplayPanel,WonPanel wonPanel,GameData gameData)
    {
        _gameplayPanel = gameplayPanel;
        _deathPanel = deathPanel;
        _gameData = gameData;
        _wonPanel = wonPanel;
        _health = health;
        _wallet = wallet;
    }

    public void OnEnable()
    {
        _health.onDead += Death;
        _wallet.onWon += Won;
    }

    public void OnDisable()
    {
        _health.onDead -= Death;
        _wallet.onWon -= Won;
    }

    private void Death()
    {
        _gameplayPanel.Hide();
        _deathPanel.Show();
    }

    private void Won()
    {
        _gameplayPanel.Hide();

        if (_gameData.CurentLevel == _gameData.ActivatedLevels)
            _gameData.TryActiveNewLevel();

        _wonPanel.Show();
    }
}