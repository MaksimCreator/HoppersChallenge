using UnityEngine;
using Zenject;

public abstract class ItemInventaryComponent : ItemComponent
{
    [SerializeField] private int _addScoreValue;

    private Inventary _inventary;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet,Inventary inventary)
    {
        _wallet = wallet;
        _inventary = inventary;
    }

    protected override void OnAddItem()
    {
        if (TryAddItemInventary(_inventary) == false)
            return;

        _wallet.AddScore(_addScoreValue);
    }

    protected abstract bool TryAddItemInventary(Inventary inventary);
}
