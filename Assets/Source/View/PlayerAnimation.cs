using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerAnimation : IControl
{
    private readonly AnimationConfig _config;

    private Image _playerImage;
    private RectTransform _playerTransform;
    private Tween _animation;

    private Tween _disappear => ManualFadeTo(0, _config.LenghDisappearSecond);

    private Tween _appears => ManualFadeTo(1, _config.LenghAppearsSecond);

    [Inject]
    public PlayerAnimation(AnimationConfig config)
    {
        _config = config;
    }

    public void Init(Image playerImage,RectTransform playerTransform)
    {
        _playerImage = playerImage;
        _playerTransform = playerTransform;
    }

    public void OnDisable()
    {
        if (_animation != null)
            _animation.Pause();
    }

    public void OnEnable()
    {
        if (_animation != null)
            _animation.Play();
    }

    public async UniTask JumpAnimaiton(Vector3 endPosition)
    {
        Tween jump = _playerTransform.DOJump(endPosition, _config.JumpPower, _config.CountJump, _config.LenghJumpSecond)
            .SetEase(Ease.Linear)
            .Pause();

        await SetAndPlayAnimation(jump);
        _animation = null;
    }

    public async UniTask StepAnimaiton(Vector3 endPosition)
    {
        Tween step = _playerTransform.DOMove(endPosition, _config.LenghStepSecond)
            .SetEase(Ease.Linear)
            .Pause();

        await SetAndPlayAnimation(step);
        _animation = null;
    }

    public async UniTask JumpAnimationObstacle(Vector3 endPosition)
    {
        Vector3 startPosition = _playerTransform.position;
        await JumpAnimaiton(endPosition);

        _playerTransform.position = startPosition;
        await Blick();
    }

    public async UniTask StepAnimationObstacle(Vector3 endPosition)
    {
        Vector3 startPosition = _playerTransform.position;
        await StepAnimaiton(endPosition);

        _playerTransform.position = startPosition;
        await Blick();
    }

    private async UniTask Blick()
    {
        for (int i = 0; i < _config.CountBlick; i++)
        {
            await SetAndPlayAnimation(_disappear);
            await SetAndPlayAnimation(_appears);
        }
    }

    private async UniTask SetAndPlayAnimation(Tween tween)
    {
        _animation = tween;
        _animation.Play();
        await _animation.AsyncWaitForCompletion();
    }

    private Tween ManualFadeTo(float targetAlphaValue, float duration)
    {
        Color currentColor = _playerImage.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlphaValue);

        return DOTween.To(() => _playerImage.color, color => _playerImage.color = color, targetColor, duration)
                          .SetEase(Ease.Linear)
                          .Pause();
    }
}