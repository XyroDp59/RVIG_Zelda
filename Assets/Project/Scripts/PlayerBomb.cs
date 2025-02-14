using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private float tickingDuration = 8f;
    [SerializeField] private float fastTickingDuration = 2f;

    WaitForSeconds second = new WaitForSeconds(1);
    WaitForSeconds halfSecond = new WaitForSeconds(0.5f);

    [SerializeField] private AudioClip tickingSFX;
    [SerializeField] private AudioClip kaboomSFX;
    [SerializeField] private ParticleSystem bombParticles;
    [SerializeField] private Transform player;
    private AudioSource source;

    public void OnBombTaken()
    {
        transform.parent = transform.parent.parent;
    }


    public void StartTickingTimer()
    {
        StartCoroutine(TickingTimer());
    }

    IEnumerator TickingTimer()
    {
        for (int i = 0; i < tickingDuration; i++)
        {
            source.Play();
            yield return second;
        }
        for (int i = 0; i < tickingDuration; i++)
        {
            source.Play();
            yield return halfSecond;
        }
        source.clip = kaboomSFX;
        source.Play();
        GameObject go = Instantiate(bombParticles, transform.position, transform.rotation).gameObject;
        Destroy(go, 2f);

    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = tickingSFX;
    }

    public void Explodes()
    {
        source.clip = kaboomSFX;
        source.Play();
        GameObject go = Instantiate(bombParticles, transform.position, transform.rotation).gameObject;
        transform.position = player.position;
    }
}
