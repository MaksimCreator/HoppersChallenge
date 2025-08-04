using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartPanelView : IControl
{
    private readonly HowPlayPanel _howPlayPanel;
    private readonly LevelsPanel _levelsPanel;

    private Button _playNow;
    private Button _levels;
    private Button _exit;
    private Image _startPanel;

    private bool _isEnable;

    [Inject]
    public StartPanelView(HowPlayPanel howPlayPanel,LevelsPanel levelsPanel)
    {
        _howPlayPanel = howPlayPanel;
        _levelsPanel = levelsPanel;
    }

    public void Init(Image startPanel,Button playNow, Button levels, Button exit)
    {
        _startPanel = startPanel;
        _playNow = playNow;
        _levels = levels;
        _exit = exit;
    }

    public void OnDisable()
    {
        if (_isEnable == false)
            return;

        _isEnable = false;

        _playNow.onClick.RemoveAllListeners();
        _levels.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
    }

    public void OnEnable()
    {
        if (_isEnable)
            return;

        _isEnable = true;

        _playNow.onClick.AddListener(EnterPlayHow);
        _levels.onClick.AddListener(EnterLevels);
        _exit.onClick.AddListener(Exit);
    }

    public void Show()
    => _startPanel.gameObject.SetActive(true);

    private async void EnterPlayHow()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        Hide();
        _howPlayPanel.Show();
    }

    private async void EnterLevels()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        Hide();
        _levelsPanel.Show();
    }

    private async void Exit()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        _startPanel.gameObject.SetActive(false);
        Application.Quit();
    }

    private void Hide()
    =>  _startPanel.gameObject.SetActive(false);
}