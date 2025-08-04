using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

public class HealthView : IControl
{
    private readonly Health _health;

    private Image[] _healthImage;
    private Sprite _enableHealth;
    private Sprite _disableHealth;

    private int _indexHealth;

    private bool _canSwith => (_indexHealth < 0 || _indexHealth >= _healthImage.Length) == false;

    [Inject]
    public HealthView(Health health)
    {
        _health = health;
    }

    public void Init(Image[] imageHealth,Sprite enableHealth,Sprite disableHealth)
    {
        if (imageHealth.Length != _health.MaxHealth)
            throw new InvalidOperationException("The length of the array does not match the maximum health");

        _indexHealth = imageHealth.Length - 1;

        _healthImage = imageHealth;
        _enableHealth = enableHealth;
        _disableHealth = disableHealth;
    }

    public void OnDisable()
    {
        _health.onAddHealth -= AddHealth;
        _health.onTakeDamage -= RemoveHealth;
    }

    public void OnEnable()
    {
        _health.onAddHealth += AddHealth;
        _health.onTakeDamage += RemoveHealth;
    }

    private void RemoveHealth()
    {
        if (_canSwith == false)
            throw new ArgumentOutOfRangeException();

        _healthImage[_indexHealth].sprite = _disableHealth;
        _indexHealth--;
    }

    private void AddHealth()
    {
        _indexHealth++;
        
        if(_canSwith == false)
            throw new ArgumentOutOfRangeException();

        _healthImage[_indexHealth].sprite = _enableHealth;
    }
}