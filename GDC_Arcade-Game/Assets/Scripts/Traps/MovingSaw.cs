using System.Collections;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDistant = default;
    [SerializeField] private float _moveTime = default;
    private float _turnTimer = default;
    private bool _moveLeft = default;
    private bool _moveRight = default;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _moveLeft = true;
        _moveRight = false;
        _turnTimer = _moveTime;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _turnTimer -= Time.deltaTime;
        MoveSaw();
        ChangeDirection();

    }
    
    private void MoveSaw()
    {
        if (_moveLeft)
        {
            transform.position -= _moveDistant * Time.deltaTime;
        }

        if (_moveRight)
        {
            transform.position += _moveDistant * Time.deltaTime;
        }
    }

    private void ChangeDirection()
    {
        if (_turnTimer < 0)
        {
            _turnTimer = _moveTime;
            _moveLeft = !_moveLeft;
            _moveRight = !_moveRight;
        }
    }

}
