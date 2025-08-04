using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InitializedPanelSample : MonoBehaviour
{
    [SerializeField] private Image _menuImage;
    [SerializeField] private Image _howPlayImage;
    [SerializeField] private Image _levelsImage;

    [Header("StartPanel")]
    [SerializeField] private Button _playNow;
    [SerializeField] private Button _enterLevels;
    [SerializeField] private Button _exit;

    [Header("LevelsPanel")]
    [SerializeField] private Button[] _levels;
    [SerializeField] private Button _homeLevels;

    [Header("PlayHow")]
    [SerializeField] private Button _homePlayHow;
    [SerializeField] private Button _startGame;

    private StartPanelView _startPanel;
    private LevelsPanel _levelsPanel;
    private HowPlayPanel _howPlayPanel;

    [Inject]
    private void Construct(StartPanelView startPanelView,LevelsPanel levelsPanel,HowPlayPanel howPlayPanel)
    {
        _startPanel = startPanelView;
        _levelsPanel = levelsPanel;
        _howPlayPanel = howPlayPanel;
    }

    public void Init()
    {
        _howPlayPanel.Init(_startGame, _homePlayHow, _howPlayImage, _startPanel);
        _levelsPanel.Init(_levels, _homeLevels, _levelsImage, _startPanel);
        _startPanel.Init(_menuImage,_playNow, _enterLevels, _exit);

        _levelsImage.gameObject.SetActive(false);
        _howPlayImage.gameObject.SetActive(false);
        _startPanel.Show();
    }
}
