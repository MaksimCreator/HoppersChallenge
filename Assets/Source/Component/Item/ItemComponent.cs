using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    [SerializeField] private GameObject _imageItem;

    private bool _isDisable;

    public void AddItem()
    {
        if (_isDisable)
            return;

        DisableItem();
        OnAddItem();
    }

    private void DisableItem()
    {
        _isDisable = true;
        _imageItem.SetActive(false);
    }

    protected abstract void OnAddItem();
}
