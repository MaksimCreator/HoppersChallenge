using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class DeathPanel : IControl
{
    private readonly Wallet _wallet;

    private Image _deathPanel;
    private Button _replay;
    private TextMeshProUGUI _score;

    [Inject]
    public DeathPanel(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Init(Image deathPanel,Button replay,TextMeshProUGUI score)
    {
        _deathPanel = deathPanel;
        _replay = replay;
        _score = score;
    }

    public void OnEnable()
    {
        _replay.onClick.AddListener(Replay);
    }

    public void OnDisable()
    {
        _replay.onClick.RemoveListener(Replay);
    }

    public void Show()
    { 
        _deathPanel.gameObject.SetActive(true);
        _score.text = _wallet.Value.ToString();
    }

    public void Hide()
    => _deathPanel.gameObject.SetActive(false);

    private void Replay()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}