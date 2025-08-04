using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationConfig", menuName = "AnimationConfig/PlayerAnimation")]
public class AnimationConfig : ScriptableObject
{
    [SerializeField] private int _jumpPower = 5;
    [SerializeField] private int _countJump = 1;
    [SerializeField] private int _countBlick = 4;
    [SerializeField] private float _lenghAnimationJump = 1.5f;
    [SerializeField] private float _lenghAnimationStep = 1.5f;
    [SerializeField] private float _lenghAnimationAppears = 0.75f;
    [SerializeField] private float _lenghAnimationDisappear = 0.75f;

    public int JumpPower => _jumpPower;

    public int CountJump => _countJump;

    public int CountBlick => _countBlick;

    public float LenghJumpSecond => _lenghAnimationJump;

    public float LenghStepSecond => _lenghAnimationStep;

    public float LenghAppearsSecond => _lenghAnimationAppears;

    public float LenghDisappearSecond => _lenghAnimationDisappear;
}