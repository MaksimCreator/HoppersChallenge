using TMPro;
using Zenject;

public class WalletView : IControl
{
    private const int _startScore = 0;

    private TextMeshProUGUI _score;
    private Wallet _wallet;

    [Inject]
    public WalletView(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Init(TextMeshProUGUI score,TextMeshProUGUI target)
    {
        _score = score;
        OnUpdateScore(_startScore);
        target.text = _wallet.TargetScore.ToString();
    }

    public void OnDisable()
    {
        _wallet.onUpdateScore -= OnUpdateScore;
    }

    public void OnEnable()
    {
        _wallet.onUpdateScore += OnUpdateScore;
    }

    private void OnUpdateScore(int score)
    => _score.text = score.ToString();
}