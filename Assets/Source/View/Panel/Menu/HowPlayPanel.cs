using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class HowPlayPanel : IControl
{
    private readonly GameData _gameData;

    private StartPanelView _menuPanel;
    private Button _play;
    private Button _home;
    private Image _howPlayImage;

    private bool _isEnable;

    [Inject]
    public HowPlayPanel(GameData gameData)
    {
        _gameData = gameData;
    }

    public void Init(Button play, Button home,Image howPlayPanel,StartPanelView startPanelView)
    {
        _play = play;
        _home = home;
        _howPlayImage = howPlayPanel;
        _menuPanel = startPanelView;
    }

    public void Show()
    => _howPlayImage.gameObject.SetActive(true);

    public void OnDisable()
    {
        if (_isEnable == false)
            return;

        _isEnable = false;
        _play.onClick.RemoveAllListeners();
        _home.onClick.RemoveAllListeners();
    }

    public void OnEnable()
    {
        if (_isEnable)
            return;

        _isEnable = true;
        _play.onClick.AddListener(EnterLevel);
        _home.onClick.AddListener(EnterMenu);
    }

    private async void EnterMenu()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        Hide();
        _menuPanel.Show();
    }

    private async void EnterLevel()
    {
        OnDisable();
        await UniTask.Delay(Config.DELAY_TRANSITION_MILISECOND);
        OnEnable();
        SceneManager.LoadScene(_gameData.CurentLevel);
    }

    private void Hide()
    => _howPlayImage.gameObject.SetActive(false);
}