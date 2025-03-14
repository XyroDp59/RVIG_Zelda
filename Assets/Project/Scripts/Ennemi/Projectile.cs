using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 0.3f;
    [SerializeField] int projectileDamage = 1;
    
    private Vector3 _direction;
    
    public void Initialise(Vector3 direction)
    {
        _direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * _direction);
    }
}
