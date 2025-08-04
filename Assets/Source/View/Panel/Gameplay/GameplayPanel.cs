using UnityEngine.UI;
using Zenject;

public class GameplayPanel : IControl
{
    private readonly GameplayController _gameplayController;
    private readonly InputRouter _inputRouter;

    private PauseGameplayPanel _pauseGameplayPanel;
    private Image _gameplayImage;
    private Button _pauseButton;

    [Inject]
    public GameplayPanel(GameplayController gameplayController, InputRouter inputRouter)
    {
        _gameplayController = gameplayController;
        _inputRouter = inputRouter;
    }

    public void Init(Image gameplayImage,Button pauseButton, PauseGameplayPanel pauseGameplayPanel)
    {
        _pauseGameplayPanel = pauseGameplayPanel;
        _gameplayImage = gameplayImage;
        _pauseButton = pauseButton;
    }

    public void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(EnterPausePanel);
    }

    public void OnEnable()
    {
        _pauseButton.onClick.AddListener(EnterPausePanel);
    }

    public void Show()
    {
        _gameplayController.OnEnable();
        _gameplayImage.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _gameplayController.OnDisable();
        _inputRouter.Dispose();
        _gameplayImage.gameObject.SetActive(false);
    }

    private void EnterPausePanel()
    {
        Hide();
        _pauseGameplayPanel.Show();
    }
}