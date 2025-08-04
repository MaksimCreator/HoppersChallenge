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
    [SerializeField] private Image _gameplayImage;
    [SerializeField] private Button _pauseButton;
    
    [Header("PauseGameplayPanel")]
    [SerializeField] private Image _pauseGameplayImage;
    [SerializeField] private Button _pauseHomeButton;
    [SerializeField] private Button _pauseBackGameplay;
    
    [Header("DeathPanel")]
    [SerializeField] private Image _deathImage;
    [SerializeField] private Button _deathReplay;
    [SerializeField] private TextMeshProUGUI _scoreDeath;

    [Header("WonPanel")]
    [SerializeField] private Button _replayWon;
    [SerializeField] private Image _firstPaneImageWon;
    [SerializeField] private Image _secondPaneImageWon;
    [SerializeField] private TextMeshProUGUI _firstPanelScoreWon;
    [SerializeField] private TextMeshProUGUI _secondPanelScoreWon;

    private GameplayController _levelController;
    private PlayerAnimation _playerAnimation;
    
    private InventaryView _inventaryView;
    private WalletView _walletView;
    private HealthView _healthView;

    private PauseGameplayPanel _pauseGameplayPanel;
    private GameplayPanel _gameplayPanel;
    private DeathPanel _deathPanel;
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
        _pauseGameplayPanel = pauseGameplayPanel;
        _playerAnimation = playerAnimation;
        _levelController = levelController;
        _inventaryView = inventaryView;
        _gameplayPanel = gameplayPanel;
        _walletView = walletView;
        _healthView = healthView;
        _deathPanel = deathPanel;
        _wonPanel = wonPanel;

    }

    public void Init()
    {
        _levelController.Init(_player);

        _playerAnimation.Init(_playerImage, _player);
        
        _inventaryView.Init(_countApply, _countSword, _countStar,_maxCoundApplay,_maxCountStar,_maxCountSword);
        _healthView.Init(_healthImage, _enableHealth, _disableHealth);
        _walletView.Init(_scoreGameplay,_targetGameplay);

        _wonPanel.Init(_replayWon, _firstPaneImageWon, _secondPaneImageWon, _firstPanelScoreWon, _secondPanelScoreWon, _mapConfig);
        _pauseGameplayPanel.Init(_pauseHomeButton, _pauseBackGameplay, _pauseGameplayImage);
        _gameplayPanel.Init(_gameplayImage, _pauseButton,_pauseGameplayPanel);
        _deathPanel.Init(_deathImage, _deathReplay,_scoreDeath);
    }
}