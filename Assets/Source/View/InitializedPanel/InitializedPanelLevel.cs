using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InitializedPanelLevel : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private MapConfig _mapConfig;

    [Header("Item")]
    [SerializeField] private TextMeshProUGUI _countApply;
    [SerializeField] private TextMeshProUGUI _countSword;
    [SerializeField] private TextMeshProUGUI _countStar;
    [SerializeField] private TextMeshProUGUI _maxCoundApplay;
    [SerializeField] private TextMeshProUGUI _maxCountSword;
    [SerializeField] private TextMeshProUGUI _maxCountStar;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreGameplay;
    [SerializeField] private TextMeshProUGUI _targetGameplay;

    [Header("Player")]
    [SerializeField] private RectTransform _player;
    [SerializeField] private Image _playerImage;

    [Header("Health")]
    [SerializeField] private Image[] _healthImage;
    [SerializeField] private Sprite _enableHealth;
    [SerializeField] private Sprite _disableHealth;

    [Header("GameplayPanel")]
    [SerializeField] private Image _gameplayPanel;
    [SerializeField] private Button _pauseButton;
    
    [Header("PauseGameplayPanel")]
    [SerializeField] private Image _pauseGameplayPanel;
    [SerializeField] private Button _pauseHomeButton;
    [SerializeField] private Button _pauseBackGameplay;
    
    [Header("DeathPanel")]
    [SerializeField] private Image _deathPanel;
    [SerializeField] private Button _deathReplay;
    [SerializeField] private TextMeshProUGUI _scoreDeath;

    [Header("WonPanel")]
    [SerializeField] private Button _replayWon;
    [SerializeField] private Image _firstPanePanelWon;
    [SerializeField] private Image _secondPanePanelWon;
    [SerializeField] private TextMeshProUGUI _firstPanelScoreWon;
    [SerializeField] private TextMeshProUGUI _secondPanelScoreWon;

    private GameplayController _levelController;
    private PlayerAnimation _playerAnimation;
    
    private InventaryView _inventaryView;
    private WalletView _walletView;
    private HealthView _healthView;

    private PauseGameplayPanel _pauseGameplay;
    private GameplayPanel _gameplay;
    private DeathPanel _death;
    private WonPanel _wonPanel;

    [Inject]
    private void Construct(InventaryView inventaryView,
        WalletView walletView,
        GameplayController levelController,
        PlayerAnimation playerAnimation,
        HealthView healthView,
        GameplayPanel gameplayPanel,
        PauseGameplayPanel pauseGameplayPanel,
        DeathPanel deathPanel,
        WonPanel wonPanel)
    {
        _pauseGameplay = pauseGameplayPanel;
        _playerAnimation = playerAnimation;
        _levelController = levelController;
        _inventaryView = inventaryView;
        _gameplay = gameplayPanel;
        _walletView = walletView;
        _healthView = healthView;
        _death = deathPanel;
        _wonPanel = wonPanel;
    }

    public void Init()
    {
        _levelController.Init(_player);

        _playerAnimation.Init(_playerImage, _player);
        
        _inventaryView.Init(_countApply, _countSword, _countStar,_maxCoundApplay,_maxCountStar,_maxCountSword);
        _healthView.Init(_healthImage, _enableHealth, _disableHealth);
        _walletView.Init(_scoreGameplay,_targetGameplay);

        _wonPanel.Init(_replayWon, _firstPanePanelWon, _secondPanePanelWon, _firstPanelScoreWon, _secondPanelScoreWon, _mapConfig);
        _pauseGameplay.Init(_pauseHomeButton, _pauseBackGameplay, _pauseGameplayPanel);
        _gameplay.Init(_gameplayPanel, _pauseButton,_pauseGameplay);
        _death.Init(_deathPanel, _deathReplay,_scoreDeath);
    }
}