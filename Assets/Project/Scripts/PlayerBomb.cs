using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private float tickingDuration = 8f;
    [SerializeField] private float fastTickingDuration = 2f;

    WaitForSeconds second = new WaitForSeconds(1);
    WaitForSeconds halfSecond = new WaitForSeconds(0.5f);

    [SerializeField] private AudioClip tickingSFX;
    [SerializeField] private AudioClip kaboomSFX;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem ghostBombParticles;
    [SerializeField] private Transform player;
    private AudioSource source;
    private MeshRenderer mesh;
    private Rigidbody rb;
    private Vector3 offset;

    public void ToggleBombVisibility()
    {
        mesh.enabled = !mesh.enabled;
        ghostBombParticles.gameObject.SetActive(!ghostBombParticles.gameObject.activeSelf);
    }

    public void ShowBomb()
    {
        if(!mesh.enabled) ToggleBombVisibility();
    }


    public void StartTickingTimer()
    {
        StartCoroutine(TickingTimer());
    }

    IEnumerator TickingTimer()
    {
        rb.useGravity = true;
        source.clip = tickingSFX;
        transform.SetParent(null);
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
        Explodes();
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        source.clip = tickingSFX;
        offset = transform.position.y * Vector3.up;
    }

    public void Explodes()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        source.clip = kaboomSFX;
        source.Play();

        GameObject go = Instantiate(explosionParticles, transform.position, transform.rotation).gameObject;
        go.SetActive(true);
        Destroy(go, 3f);

        transform.parent = player;
        transform.position = player.position + offset;
        ToggleBombVisibility();
    }
}
