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
    [SerializeField] private PossibleTraps _possibleTraps = null;

    private Queue<Vector3> _possiblePositions;

    private float _timeOfLastTrapPlaced;

    private KeyCode _playerConfirmKey;

    private KeyCode[] _upDownKeys;

    private bool _finished;

    private int _trapIndex;

    private float ElapsedTime(float timeOfStart) => Time.time - timeOfStart;

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
            if (ElapsedTime(_timeOfLastTrapPlaced) < _secondsPerTrap)
            {
                if (Input.GetKeyDown(_playerConfirmKey))
                {
                    if (_possiblePositions.Count > 0)
                        OnConfirm(false);
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
            {
                if (_possiblePositions.Count > 0)
                    OnConfirm(true);
            }
        }
        else
            OnFinished();
    }

    private void OnSelectionUp()
    {
        _trapIndex++;
        if (_trapIndex >= _possibleTraps.Traps.Length)
            _trapIndex = 0;
    }

    private void OnSelectionDown()
    {
        _trapIndex--;
        if (_trapIndex < 0)
            _trapIndex = _possibleTraps.Traps.Length -1;
    }

    private void OnConfirm(bool randomTrap)
    {
        _timeOfLastTrapPlaced = Time.time;
        int index = randomTrap ? Random.Range(0, _possibleTraps.Traps.Length) : _trapIndex;
        Instantiate(_possibleTraps.Traps[index], _possiblePositions.Dequeue(), Quaternion.identity);
        if (_possiblePositions.Count == 0)
            OnFinished();

    }

    private void OnFinished()
    {
        _finished = true;
        DaddyScript.Instance.CreationFinished();
        Destroy(gameObject);
    }   
}
