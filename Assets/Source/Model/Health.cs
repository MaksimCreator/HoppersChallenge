using System;

public class Health
{
    public readonly int MaxHealth;

    private int _health;

    public event Action onDead;
    public event Action onTakeDamage;
    public event Action onAddHealth;

    public Health(MapConfig mapConfig)
    {
        _health = mapConfig.Health;
        MaxHealth = _health;
    }

    public void TakeDamage()
    {
        _health--;
        onTakeDamage?.Invoke();

        if(_health == 0)
            onDead?.Invoke();
    }

    public void AddHealth()
    {
        if (_health > MaxHealth)
            throw new InvalidOperationException();

        if (_health == MaxHealth)
            return;

        _health++;
        onAddHealth?.Invoke();
    }
}