using Zenject;
using UnityEngine;

public class SampleSceneLoop : GameLoop
{
    [SerializeField] private InitializedPanelSample _panel;

    private bool _isInit;
    private IControl[] _controls;
    private IUpdateble[] _updatebles;

    [Inject]
    private void Construct(StartPanelView startPanelView, LevelsPanel levelsPanel, HowPlayPanel howPlayPanel)
    {
        _controls = new IControl[]
        {
            startPanelView,
            levelsPanel,
            howPlayPanel
        };
        _updatebles = new IUpdateble[]
        {
            levelsPanel
        };
    }

    private void Awake()
    {
        if (_isInit)
            return;

        _panel.Init();
        _isInit = false;
    }

    protected override IControl[] GetControls()
    =>  _controls;

    protected override IUpdateble[] GetUpdatebles()
    => _updatebles;
}
