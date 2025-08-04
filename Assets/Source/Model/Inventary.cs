using System;

public class Inventary
{
    public readonly int MaxApply;
    public readonly int MaxStar;
    public readonly int MaxSword;

    private int _apply;
    private int _star;
    private int _sword;

    public event Action<int> onUpdateApply;
    public event Action<int> onUpdateStar;
    public event Action<int> onUpdateSword;

    public Inventary(MapConfig mapConfig)
    {
        MaxSword = mapConfig.CountSword;
        MaxApply = mapConfig.CountApply;
        MaxStar = mapConfig.CountStar;
    }

    public bool TryAddApply()
    {
        if (_apply > MaxApply || _apply < 0)
            throw new InvalidOperationException();

        if (_apply == MaxApply)
            return false;

        _apply++;
        onUpdateApply.Invoke(_apply);
        return true;
    }

    public bool TryAddSword()
    {
        if (_sword > MaxSword || _sword < 0)
            throw new InvalidOperationException();

        if (_sword == MaxSword)
            return false;

        _sword++;
        onUpdateSword.Invoke(_sword);
        return true;
    }

    public bool TryAddStar()
    {
        if (_star > MaxStar || _star < 0)
            throw new InvalidOperationException();

        if (_star == MaxStar)
            return false;

        _star++;
        onUpdateStar.Invoke(_star);
        return true;
    }
}
