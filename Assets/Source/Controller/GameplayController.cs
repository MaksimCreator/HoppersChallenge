using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayController : IControl
{
    private const int START_WAY = 1;

    private RectTransform _playerPositionRaycast;
    private PathFinding _pathFinding;
    private InputRouter _inputRouter;
    private RaycastSystem _raycastSystem;
    private ComponentCatalog _componentCatalog;
    private PlayerMovement _playerMovement;

    private Vector2 _playerPositionMap;
    private Vector2 _pointStep = Vector2.zero;
    private MovementAnimationType _typeAnimationStep;
    private ObstacleContext _obstacleContextStep;
    private ItemContext _itemContextStep;

    private GroundComponent _groundComponentStep;
    private ItemComponent _itemComponentStep;
    private StepJumpCheckerComponent _stepJumpCheckerStep;
    private ObstacleComponent _obstaclComponentStep;

    [Inject]
    public GameplayController(
        PathFinding pathFinding,
        InputRouter inputRouter,
        RaycastSystem raycastSystem,
        ComponentCatalog componentCatalog,
        PlayerMovement playerMovement)
    {
        _pathFinding = pathFinding;
        _inputRouter = inputRouter;
        _raycastSystem = raycastSystem;
        _componentCatalog = componentCatalog;
        _playerMovement = playerMovement;
    }

    public void Init(RectTransform playerPositionRaycast)
    {
        _playerPositionRaycast = playerPositionRaycast;
    }

    public void OnDisable()
    {
        _inputRouter.OnDisable();
        _playerMovement.OnDisable();
        _inputRouter.onClickAsync -= OnClik;
    }

    public void OnEnable()
    {
        _inputRouter.OnEnable();
        _playerMovement.OnEnable();
        _inputRouter.onClickAsync += OnClik;
    }

    private async UniTask OnClik(Vector2 position)
    => await OnClikAsync(position);

    private void GetComponents(Vector2 positionMap)
    {
        if (_componentCatalog.TryGetComponent(positionMap, out _groundComponentStep) == false)
            throw new InvalidOperationException();

        if (_componentCatalog.TryGetComponent(positionMap, out _stepJumpCheckerStep))
            _typeAnimationStep = _stepJumpCheckerStep.GetTypeAnimation(_playerPositionMap);

        if (_componentCatalog.TryGetComponent(positionMap, out _obstaclComponentStep))
            _obstacleContextStep = ObstacleContext.Obstacle;
        else
            _obstacleContextStep = ObstacleContext.NonObstacle;

        if (_componentCatalog.TryGetComponent(positionMap, out _itemComponentStep))
        {
            if (_obstaclComponentStep != null)
                throw new InvalidOperationException();

            _itemContextStep = ItemContext.Item;
        }
        else
        {
            _itemContextStep = ItemContext.NonItem;
        }
            
    }

    private async UniTask OnClikAsync(Vector2 position)
    {
        if (_raycastSystem.TryGetGroundedComponent(position, out GroundComponent endComponent) == false)
            return;

        if (_raycastSystem.TryGetGroundedComponent(_playerPositionRaycast.position, out GroundComponent startComponent) == false)
            throw new InvalidOperationException();

        _playerPositionMap = startComponent.IndexMap;
        CellMapBFS start = new CellMapBFS((int)startComponent.IndexMap.x, (int)startComponent.IndexMap.y, 0);
        CellMapBFS end = new CellMapBFS((int)endComponent.IndexMap.x,(int)endComponent.IndexMap.y,0);

        List<CellMapBFS> way = _pathFinding.FindShortestPath(start, end);

        for (int i = START_WAY; i < way.Count; i++)
        {
            _pointStep.x = way[i].Row;
            _pointStep.y = way[i].Col;

            _groundComponentStep = null;
            _stepJumpCheckerStep = null;
            _obstaclComponentStep = null;
            _itemComponentStep = null;
            GetComponents(_pointStep);

            await _playerMovement.Move(_typeAnimationStep,_obstacleContextStep,_groundComponentStep.Position);
            ItemAction(_itemContextStep,_obstacleContextStep);

            if (_playerMovement.CanMove == false)
                break;

            _playerPositionMap = _pointStep;
        }
    }

    private void ItemAction(ItemContext itemContext,ObstacleContext obstacleContext)
    {
        if (itemContext == ItemContext.Item)
            _itemComponentStep.AddItem();
        else if (obstacleContext == ObstacleContext.Obstacle)
            _obstaclComponentStep.HandleDamage();
    }
}
