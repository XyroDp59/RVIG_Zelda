using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBomb : MonoBehaviour
{
    [Header("Durations")]
    [SerializeField] private float tickingDuration = 8f;
    [SerializeField] private float fastTickingDuration = 2f;
    [SerializeField] private float cooldownBetweenBombs = 1f;

    private bool isWaiting = true;
    WaitForSeconds second = new WaitForSeconds(1);
    WaitForSeconds halfSecond = new WaitForSeconds(0.5f);
    WaitForSeconds cooldown;

    [Header("Objects references")]
    [SerializeField] private Transform bomb;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem ghostBombParticles;
    
    private MeshRenderer mesh;
    private Rigidbody rb;
    [SerializeField] private float offsetFront = 0.1f;
    [SerializeField] private float offsetHeight = 0.3f;
    private AudioSource source;

    void Start()
    {
        source = bomb.GetComponent<AudioSource>();
        mesh = bomb.GetComponent<MeshRenderer>();
        rb = bomb.GetComponent<Rigidbody>();
        cooldown = new WaitForSeconds(cooldownBetweenBombs);
    }

    #region visibility handler

    public void ToggleBombVisibility()
    {
        SwitchBombVisibility(!mesh.enabled);
    }
    public void ShowBomb()
    {
        SwitchBombVisibility(true);
    }
    public void HideBomb()
    {
        SwitchBombVisibility(true);
    }


    public void SwitchBombVisibility(bool b)
    {
        mesh.enabled = b;
        ghostBombParticles.gameObject.SetActive(!b);
    }
    #endregion

    #region Bomb logic

    private void Update()
    {
        if (isWaiting)
        {
            Transform head = Camera.main.transform;
            Vector3 horizontalDir = (head.forward - head.forward.y * Vector3.up ).normalized;
            bomb.localPosition = head.localPosition + offsetFront * horizontalDir + offsetHeight * Vector3.up;
        }
    }

    public void OnBombGrabbed()
    {
        isWaiting = false;
    }


    public void StartTickingTimer()
    {
        StartCoroutine(TickingTimer());
    }


    IEnumerator TickingTimer()
    {
        SwitchBombVisibility(true);

        rb.useGravity = true;
        bomb.SetParent(null);
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



    public void Explodes()
    {
        //CustomDebugger.log("\nExplodes");
        GameObject go = Instantiate(explosionParticles, bomb.position, bomb.rotation).gameObject;
        go.SetActive(true);
        Destroy(go, 3f);

        StartCoroutine(ResetPosition());
    }

    private IEnumerator ResetPosition()
    {  
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        bomb.gameObject.SetActive(false);
        yield return cooldown;

        bomb.parent = transform;
        //bomb.localPosition = offset;
        //CustomDebugger.log("Bomb Return");
        //CustomDebugger.log("transform.position = " + transform.position.ToString());
        //if (bomb.localPosition != offset) throw new IOException();
        bomb.gameObject.SetActive(true);
        SwitchBombVisibility(false);
        isWaiting = true;
    }
}
#endregion