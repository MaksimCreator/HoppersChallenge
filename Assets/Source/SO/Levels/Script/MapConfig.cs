using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "Level/MapConfig")]
public class MapConfig : ScriptableObject
{
    [SerializeField] private List<Vector2> _positionGround;

    [SerializeField] private int _countApply;
    [SerializeField] private int _countStar;
    [SerializeField] private int _countSword;
    [SerializeField] private int _targetScore;
    [SerializeField] private int _health = 3;
    [SerializeField] private float _delayFirstWonPanel = 1.2f;
    [SerializeField] private int _lenght;
    [SerializeField] private int _row;
    [SerializeField] private bool _moveRowsDown = false;
    [SerializeField] private bool _moveRowsUp = false;

    private int[,] _map;

    private int __void = (int)TypeMap.Void;
    private int __ground = (int)TypeMap.Ground;

    public int Health => _health;

    public int CountApply => _countApply;

    public int CountStar => _countStar;

    public int CountSword => _countSword;

    public int TargetScore => _targetScore;

    public int DelayFirstWonPanelMilisecond => (int)(_delayFirstWonPanel * 1000);

    public int[,] Map
    {
        get
        {
            if (_map == null)
                _map = GetMap();

            return _map;
        }
    }

    protected int[,] GetMap()
    {
        int[,] map = new int[_row, _lenght];

        for (int i = 0; i < _row; i++)
        {
            for (int y = 0; y < _lenght; y++)
            {
                for (int z = 0; z < _positionGround.Count; z++)
                {
                    if (i == _positionGround[z].x && y == _positionGround[z].y)
                        map[i, y] = __ground;
                }
            }
        }

        return map;
    }

    private void OnValidate()
    {
        if (_moveRowsDown)
        {
            for (int i = 0; i < _positionGround.Count; i++)
                _positionGround[i] = (new Vector2(_positionGround[i].x + 1, _positionGround[i].y));

            _moveRowsDown = false;
        }

        if (_moveRowsUp)
        {
            for (int i = 0; i < _positionGround.Count; i++)
                _positionGround[i] = (new Vector2(_positionGround[i].x - 1, _positionGround[i].y));

            _moveRowsUp = false;
        }

        int maxRow = 0;
        int maxLenght = 0;

        for (int i = 0; i < _positionGround.Count; i++)
        {
            if (_positionGround[i].x > maxRow)
                maxRow = (int)_positionGround[i].x;
            if (_positionGround[i].y > maxLenght)
                maxLenght = (int)_positionGround[i].y;
        }

        _row = maxRow + 1;
        _lenght = maxLenght + 1;
    }
}