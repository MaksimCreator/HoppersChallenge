using UnityEngine;
using Zenject;

public class ObstacleComponent : MonoBehaviour
{
    private Health _playerHealth;

    [Inject]
    private void Construct(Health health)
    {
        _playerHealth = health;
    }

    public void HandleDamage()
    {
        _playerHealth.TakeDamage();
    }
}
