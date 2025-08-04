using System.Collections.Generic;
using UnityEngine;

public class StepJumpCheckerComponent : MonoBehaviour
{
    [SerializeField] private List<Vector2> _positionStep = new();

    public MovementAnimationType GetTypeAnimation(Vector2 startPosition)
    {
        for (int i = 0; i < _positionStep.Count; i++)
        {
            if (startPosition == _positionStep[i])
                return MovementAnimationType.Step;
        }

        return MovementAnimationType.Jump;
    }
}
