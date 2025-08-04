using System;

public class Wallet
{
    public readonly int TargetScore;

    public int Value { get; private set; }

    public event Action<int> onUpdateScore;
    public event Action onWon;

    public Wallet(MapConfig mapConfig)
    {
        TargetScore = mapConfig.TargetScore;
    }

    public void AddScore(int value)
    {
        Value += value;
        onUpdateScore.Invoke(Value);

        if (Value >= TargetScore)
            onWon?.Invoke();
    }
}