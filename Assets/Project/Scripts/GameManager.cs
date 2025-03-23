using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Spawner[] Spawners;
    private bool _gameStarted;

    private void Start()
    {
        Spawners = gameObject.GetComponentsInChildren<Spawner>();
    }

    public void OnTutoExited()
    {
        //CustomDebugger.log("tuto exited");
        foreach (var spawner in Spawners)
        {
            spawner.activated = true;
        }
        _gameStarted = true;
    }

    public void OnPlayerDeath()
    {
        
    }

    public void OnPlayerVictory()
    {
        
    }

    private void Update()
    {
        if(!_gameStarted) return;
        foreach (var spawner in Spawners)
        {
            if (spawner.activated) return;
            if (!spawner.allDied) return;
        }
        
        OnPlayerVictory();
    }
}
