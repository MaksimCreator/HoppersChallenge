using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PauseGameplayPanel : IControl
{
    private readonly GameplayPanel _gameplayPanel;

    private Button _homeButton;
    private Button _backGameplay;
    private Image _pausePanelImage;

    [Inject]
    public PauseGameplayPanel(GameplayPanel gameplayPanel)
    {
        _gameplayPanel = gameplayPanel;
    }

    public void Init(Button homeButton,Button backGameplay,Image pausePanelImage)
    {
        _homeButton = homeButton;
        _backGameplay = backGameplay;
        _pausePanelImage = pausePanelImage;
    }

    public void OnDisable()
    {
        _homeButton.onClick.RemoveListener(EnterStartMenu);
        _backGameplay.onClick.RemoveListener(BackGameplay);
    }

    public void OnEnable()
    {
        _homeButton.onClick.AddListener(EnterStartMenu);
        _backGameplay.onClick.AddListener(BackGameplay);
    }

    public void Show()
    => _pausePanelImage.gameObject.SetActive(true);

    public void Hide()
    => _pausePanelImage.gameObject.SetActive(false);

    private void EnterStartMenu()
    {
        Hide();
        SceneManager.LoadScene(Config.START_MENU_INDEDX);
    }

    private void BackGameplay()
    {
        Hide();
        _gameplayPanel.Show();
    }
}
