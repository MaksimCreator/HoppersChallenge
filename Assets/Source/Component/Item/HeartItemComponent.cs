using Zenject;

public class HeartItemComponent : ItemComponent
{
    private Health _health;

    [Inject]
    private void Construct(Health health)
    {
        _health = health;
    }

    protected override void OnAddItem()
    =>  _health.AddHealth();
}