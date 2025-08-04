using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class WonPanel : IControl
{
    private readonly Wallet _wallet;
    
    private Button _replay;
    private Image _firstPaneImage;
    private Image _secondPaneImage;
    private TextMeshProUGUI _firstPanelScore;
    private TextMeshProUGUI _secondPanelScore;
    private int _delayTransition;

    [Inject]
    public WonPanel(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Init(Button replay,Image firstPanelImage, Image secondPanelImage,TextMeshProUGUI firstPanelScore, TextMeshProUGUI secondPanelScore,MapConfig mapConfig)
    {
        _replay = replay;
        _firstPaneImage = firstPanelImage;
        _secondPaneImage = secondPanelImage;
        _firstPanelScore = firstPanelScore;
        _secondPanelScore = secondPanelScore;
        _delayTransition = mapConfig.DelayFirstWonPanelMilisecond;
    }

    public void OnDisable()
    {
        _replay.onClick.RemoveListener(Replay);
    }

    public void OnEnable()
    {
        _replay.onClick.AddListener(Replay);
    }

    private void Replay()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public async UniTaskVoid Show()
    {
        _firstPaneImage.gameObject.SetActive(true);
        _firstPanelScore.text = _wallet.Value.ToString();

        await UniTask.Delay(_delayTransition);

        _firstPaneImage.gameObject.SetActive(false);
        _secondPanelScore.text = _wallet.Value.ToString();
        _secondPaneImage.gameObject.SetActive(true);
    }

    public void Hide()
    => _secondPaneImage.gameObject.SetActive(false);
}
