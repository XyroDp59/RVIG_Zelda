using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabList = new List<GameObject>();
    [SerializeField] private float _cooldown;
    [SerializeField] private int maxSpawnableEnemies;

    private WaitForSeconds _delay;
    private List<GameObject> _spawnedEnemies = new();
    
    public bool activated;
    public bool allDied;

    // Start is called before the first frame update
    void Start()
    {
        _delay = new WaitForSeconds(_cooldown);
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        yield return activated ? _delay : null;
        
        if (activated)
        {
            int r = Random.Range(0, _prefabList.Count);
            _spawnedEnemies.Add(Instantiate(_prefabList[r], new Vector3(Random.Range(-5,5), 0, Random.Range(-5, 5)), Quaternion.identity));
            maxSpawnableEnemies--;
            
            if(maxSpawnableEnemies == 0) activated = false;
        }
        StartCoroutine(SpawningCoroutine());
    }

    private void Update()
    {
        //CustomDebugger.log(_spawnedEnemies.Count.ToString());
        foreach (var enemy in _spawnedEnemies.ToList())
        {
            if (!enemy) _spawnedEnemies.Remove(enemy);
        }
        if (!activated && maxSpawnableEnemies == 0 && _spawnedEnemies.Count == 0) allDied = true;
    }
}
