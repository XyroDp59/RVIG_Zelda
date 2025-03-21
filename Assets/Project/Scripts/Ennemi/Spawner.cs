using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabList = new List<GameObject>();
    [SerializeField] private float _cooldown;

    private WaitForSeconds _delay;

    // Start is called before the first frame update
    void Start()
    {
        _delay = new WaitForSeconds(_cooldown);
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        yield return _delay;
        int r = Random.Range(0, _prefabList.Count);
    }
}
