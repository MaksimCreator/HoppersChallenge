using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
using Zenject;

public class LevelsPanel : IControl,IUpdateble
{
    private readonly GameData _gameData;
    private readonly HowPlayPanel _howPlayPanel;

    private StartPanelView _startPanel;
    private Button[] _levels;
    private Button _menu;
    private Image _levelsImage;

    private bool _isEnable;

    [Inject]
    public LevelsPanel(GameData gameData,HowPlayPanel howPlayPanel)
    {
        _gameData = gameData;
        _howPlayPanel = howPlayPanel;
    }

    public void Init(Button[] levels,Button menu,Image levelsImage,StartPanelView startPanelView)
    {
        _levels = levels;
        _menu = menu;
        _levelsImage = levelsImage;
        _startPanel = startPanelView;
    }

    public void OnDisable()
    {
        if (_isEnable == false)
            return;

        _isEnable = false;

        for (int i = 0; i < _levels.Length;i++)
            _levels[i].onClick.RemoveAllListeners();
        _menu.onClick.RemoveAllListeners();
    }

    public void OnEnable()
    {
        if (_isEnable)
            return;

        _isEnable = true;

        for (int i = 0; i < _levels.Length; i++)
        {
            int levelIndex = i + 1;
            _levels[i].onClick.AddListener(() => EnterLevel(levelIndex));
        }
        _menu.onClick.AddListener(EnterMenu);
    }

    public void Show()
    => _levelsImage.gameObject.SetActive(true);

    public void OnUpdate(float delta)
    {
        if (_levels.Length != Config.MAX_LEVEL)
            throw new InvalidOperationException();

        for (int i = 0; i < _gameData.ActivatedLevels; i++)
            _levels[i].enabled = true;

        if (_gameData.ActivatedLevels == Config.MAX_LEVEL)
            return;

        for (int i = _gameData.ActivatedLevels; i < Config.MAX_LEVEL; i++)
            _levels[i].enabled = false;
    }

    private async void EnterLevel(int indexLevel)
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        _gameData.SetCurentLevel(indexLevel);
        Hide();
        _howPlayPanel.Show();
    }

    private async void EnterMenu()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        Hide();
        _startPanel.Show();
    }

    private void Hide()
    => _levelsImage.gameObject.SetActive(false);
}