using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private const KeyCode P1_UP_KEY = KeyCode.W;
    private const KeyCode P1_DOWN_KEY = KeyCode.S;
    private const KeyCode P1_CONFIRM_KEY = KeyCode.C;

    private const KeyCode P2_UP_KEY = KeyCode.UpArrow;
    private const KeyCode P2_DOWN_KEY = KeyCode.DownArrow;
    private const KeyCode P2_CONFIRM_KEY = KeyCode.K;

    [Range(1, 2)]
    [SerializeField] private byte _targetPlayer = 1;
    [SerializeField] private float _secondsPerTrap = 6.0f;
    [SerializeField] private Transform _spotsHolder = null;

    private Queue<Vector3> _possiblePositions;

    private float _timeOfLastTrapPlaced;

    private KeyCode _playerConfirmKey;

    private KeyCode[] _upDownKeys;

    private bool _finished;

    void Awake()
    {
        _possiblePositions = new Queue<Vector3>();

        foreach (Transform t in _spotsHolder) _possiblePositions.Enqueue(t.position);

        _playerConfirmKey = _targetPlayer == 1 ? P1_CONFIRM_KEY : P2_CONFIRM_KEY;
        _upDownKeys = new KeyCode[2] { _targetPlayer == 1 ? P1_UP_KEY : P2_UP_KEY, _targetPlayer == 1 ? P1_DOWN_KEY : P2_DOWN_KEY };

        _timeOfLastTrapPlaced = Time.time;
    }

    private void Update()
    {

        if (!_finished)
        {
            if (Input.GetKeyDown(_playerConfirmKey))
            {
                OnConfirm();
            }
            // Up Key
            else if (Input.GetKeyDown(_upDownKeys[0]))
            {
                OnSelectionUp();
            }
            // Down Key
            else if (Input.GetKeyDown(_upDownKeys[1]))
            {
                OnSelectionDown();
            }
        }
        else 
            OnFinished();
    }

    private void OnSelectionUp()
    {
        
    }

    private void OnSelectionDown()
    {

    }

    private void OnConfirm()
    {
        _possiblePositions.Peek();
    }

    private void OnFinished()
    {

    }
}
