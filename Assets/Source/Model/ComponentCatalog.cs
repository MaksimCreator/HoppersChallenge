using System.Collections.Generic;
using UnityEngine;
using System;

public class ComponentCatalog
{
    private readonly Dictionary<Vector2, GroundComponent> _groundComponents = new();
    private readonly Dictionary<Vector2, ItemComponent> _itemComponents = new();
    private readonly Dictionary<Vector2, ObstacleComponent> _obstaclComponents = new();
    private readonly Dictionary<Vector2, StepJumpCheckerComponent> _stepJumpCheckerComponents = new();

    public ComponentCatalog(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            GroundComponent groundComponent = gameObjects[i].GetComponent<GroundComponent>();

            if (groundComponent == null)
                throw new InvalidOperationException();

            _groundComponents.Add(groundComponent.IndexMap, groundComponent);

            if (gameObjects[i].TryGetComponent(out ItemComponent itemComponent))
                _itemComponents.Add(groundComponent.IndexMap, itemComponent);

            if (gameObjects[i].TryGetComponent(out ObstacleComponent ObstaclComponent))
                _obstaclComponents.Add(groundComponent.IndexMap, ObstaclComponent);

            if (gameObjects[i].TryGetComponent(out StepJumpCheckerComponent StepJumpCheckerComponent))
                _stepJumpCheckerComponents.Add(groundComponent.IndexMap, StepJumpCheckerComponent);
        }
    }

    public bool TryGetComponent(Vector2 positionOnMap, out GroundComponent Componet)
    {
        Componet = default;

        if(_groundComponents.TryGetValue(positionOnMap, out Componet))
            return true;

        return false;
    }

    public bool TryGetComponent(Vector2 positionOnMap, out ObstacleComponent Componet)
    {
        Componet = default;

        if (_obstaclComponents.TryGetValue(positionOnMap, out Componet))
            return true;

        return false;
    }

    public bool TryGetComponent(Vector2 positionOnMap, out StepJumpCheckerComponent Componet)
    {
        Componet = default;

        if (_stepJumpCheckerComponents.TryGetValue(positionOnMap, out Componet))
            return true;

        return false;
    }

    public bool TryGetComponent(Vector2 positionOnMap, out ItemComponent Componet)
    {
        Componet = default;

        if (_itemComponents.TryGetValue(positionOnMap, out Componet))
            return true;

        return false;
    }
}