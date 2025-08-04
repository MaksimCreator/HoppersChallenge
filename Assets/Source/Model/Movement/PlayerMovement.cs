using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerMovement : IControl
{
    private readonly PlayerAnimation _playerAnimation;

    public bool CanMove { get; private set; }

    [Inject]
    public PlayerMovement(PlayerAnimation playerAnimation)
    {
        _playerAnimation = playerAnimation;
    }

    public async UniTask Move(MovementAnimationType typeAnimation, ObstacleContext itemContext, Vector2 positionMove)
    {
        if (itemContext == ObstacleContext.NonObstacle)
            CanMove = true;
        else
            CanMove = false;

        if (typeAnimation == MovementAnimationType.Step)
        {
            if (CanMove)
                await _playerAnimation.StepAnimaiton(positionMove);
            else
                await _playerAnimation.StepAnimationObstacle(positionMove);
        }
        else
        {
            if (CanMove)
                await _playerAnimation.JumpAnimaiton(positionMove);
            else
                await _playerAnimation.JumpAnimationObstacle(positionMove);
        }
    }

    public void OnDisable()
    => _playerAnimation.OnDisable();

    public void OnEnable()
    => _playerAnimation.OnEnable();
}