using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRouter : IControl
{
    private readonly PlayerInput _input = new();

    public event Func<Vector2, UniTask> onClickAsync;

    private bool _isEnable = false;

    public void OnDisable()
    {
        if (_isEnable == false)
            return;

        _input.Disable();
        _input.Player.Click.performed -= Click;
        _isEnable = false;
    }

    public void OnEnable()
    {
        if (_isEnable)
            return;

        _input.Enable();
        _input.Player.Click.performed += Click;
        _isEnable = true;
    }

    public void Dispose()
    => _input.Dispose();

    private async void Click(InputAction.CallbackContext obj)
    {
        Vector2 position = Touchscreen.current.primaryTouch.position.value;
        _input.Disable();
        await onClickAsync.Invoke(position);
        _input.Enable();
    }
}