using UnityEngine;

public class GroundComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _indexMap;

    public Vector2 IndexMap => _indexMap;
    public Vector3 Position => transform.position;
}