using TMPro;
using Zenject;

public class InventaryView : IControl
{
    private const int START_ITEM = 0;

    private readonly Inventary _inventary;

    private TextMeshProUGUI _countApply;
    private TextMeshProUGUI _countSword;
    private TextMeshProUGUI _countStar;

    [Inject]
    public InventaryView(Inventary inventary)
    {
        _inventary = inventary;
    }

    public void Init
        (TextMeshProUGUI countApply, 
        TextMeshProUGUI countSword, 
        TextMeshProUGUI countStar,
        TextMeshProUGUI maxCountApply,
        TextMeshProUGUI maxCountStar,
        TextMeshProUGUI maxCountSword)
    {
        _countApply = countApply;
        _countSword = countSword;
        _countStar = countStar;

        maxCountApply.text = _inventary.MaxApply.ToString();
        maxCountStar.text = _inventary.MaxStar.ToString();
        maxCountSword.text = _inventary.MaxSword.ToString();

        OnUpdateSword(START_ITEM);
        OnUpdateApply(START_ITEM);
        OnUpdateStar(START_ITEM);
    }

    public void OnDisable()
    {
        _inventary.onUpdateApply -= OnUpdateApply;
        _inventary.onUpdateSword -= OnUpdateSword;
        _inventary.onUpdateStar -= OnUpdateStar;
    }

    public void OnEnable()
    {
        _inventary.onUpdateApply += OnUpdateApply;
        _inventary.onUpdateSword += OnUpdateSword;
        _inventary.onUpdateStar += OnUpdateStar;
    }

    private void OnUpdateApply(int apply)
    => _countApply.text = apply.ToString();

    private void OnUpdateSword(int sword)
    => _countSword.text = sword.ToString();

    private void OnUpdateStar(int star)
    => _countStar.text = star.ToString();
}