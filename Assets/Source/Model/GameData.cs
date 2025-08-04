using System;

[Serializable]
public class GameData
{
    private const int MinLevel = 1;

    public int ActivatedLevels { get; private set; } = MinLevel;
    public int CurentLevel { get; private set; } = MinLevel;

    private bool IsMax => ActivatedLevels < Config.MAX_LEVEL;

    public void SetCurentLevel(int level)
    {
        if (level > Config.MAX_LEVEL || level < MinLevel || level > ActivatedLevels)
            throw new InvalidOperationException();

        CurentLevel = level;
    }

    public bool TryActiveNewLevel()
    {
        if (CurentLevel < MinLevel || CurentLevel > ActivatedLevels || CurentLevel > Config.MAX_LEVEL)
            throw new InvalidOperationException();

        if(IsMax == false)
            return false;

        if (CurentLevel == ActivatedLevels)
        {
            ActivatedLevels++;
            return true;
        }

        return false;
    }
}
