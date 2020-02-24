using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Possible Traps")]
public class PossibleTraps : ScriptableObject
{
    [SerializeField] private GameObject[] _possibleTraps = null;
    public GameObject[] Traps => _possibleTraps; 
}
