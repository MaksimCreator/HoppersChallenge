using UnityEngine;

public abstract class GameLoop : MonoBehaviour
{
    private bool _isEnable;
    private IControl[] _controls;
    private IUpdateble[] _updatebles;
    
    private void Update()
    {
        if (_updatebles == null)
            _updatebles = GetUpdatebles();

        for (int i = 0; i < _updatebles.Length; i++)
            _updatebles[i].OnUpdate(Time.deltaTime);
    }

    protected void OnEnable()
    {
        if (_isEnable)
            return;

        if (_controls == null)
            _controls = GetControls();

        for (int i = 0; i < _controls.Length; i++)
            _controls[i].OnEnable();

        _isEnable = true;
    }

    protected void OnDisable()
    {
        if (_isEnable == false)
            return;

        if (_controls == null)
            _controls = GetControls();

        for (int i = 0; i < _controls.Length; i++)
            _controls[i].OnDisable();

        _isEnable = false;
    }

    protected abstract IControl[] GetControls();

    protected abstract IUpdateble[] GetUpdatebles();
}