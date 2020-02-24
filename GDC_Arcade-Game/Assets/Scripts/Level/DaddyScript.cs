using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaddyScript : MonoBehaviour
{
    public static DaddyScript Instance { get; private set; }

    [SerializeField] private GameObject P1Prefab = null;
    [SerializeField] private GameObject P2Prefab = null;

    [SerializeField] private Transform p1SpawnPoint = null;
    [SerializeField] private Transform p2SpawnPoint = null;

    private int finishedPlayers;

    private void Awake()
    {
        Instance = this;
        finishedPlayers = 0;
    }

    public void CreationFinished()
    {
        finishedPlayers++;
        if (finishedPlayers == 2)
            SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        Instantiate(P1Prefab, p1SpawnPoint.position, Quaternion.identity);
        Instantiate(P2Prefab, p2SpawnPoint.position, Quaternion.identity);
    }
}
