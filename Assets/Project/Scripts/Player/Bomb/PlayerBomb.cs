using System.Collections;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Player.Bomb
{
    public class PlayerBomb : MonoBehaviour
    {
        [Header("Durations")]
        [SerializeField] private float tickingDuration = 8f;
        [SerializeField] private float fastTickingDuration = 2f;
        [SerializeField] private float cooldownBetweenBombs = 1f;

        private bool isTicking = false;
        WaitForSeconds second = new WaitForSeconds(1);
        WaitForSeconds halfSecond = new WaitForSeconds(0.5f);
        WaitForSeconds cooldown;

        [Header("FlickerParams")]
        [SerializeField] private Gradient flickerGradient;
        [SerializeField] private float flickerDuration;


        [Header("Objects references")]
        [SerializeField] private Transform bombSpawner;
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private ParticleSystem ghostBombParticles;

        AudioSource source;
        MeshRenderer mesh;
        Rigidbody rb;
        Material material;

        /*
         
        garder en mémoire une liste des positions toutes les 1/f secondes des throw_Duration dernières secondes
        rendre la force du lancer proportionnel à la distance (en local position) entre la 1ere et la dernière position de la liste
         
         */

        void Start()
        {
            source = GetComponent<AudioSource>();
            mesh = GetComponent<MeshRenderer>();
            rb = GetComponent<Rigidbody>();
            cooldown = new WaitForSeconds(cooldownBetweenBombs);
            material = GetComponent<Renderer>().material;

            HideBomb();
            rb.useGravity = false;
            material.SetColor("_BaseColor", flickerGradient.Evaluate(0));
            isTicking = false;
        }

        #region visibility handler

        public void ShowBomb()
        {
            SwitchBombVisibility(true);
        }
        public void HideBomb()
        {
            SwitchBombVisibility(false);
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
            if (transform.parent == bombSpawner)
            {
                transform.localPosition = Vector3.zero;
                rb.useGravity = false;
            }
            else
            {
                ShowBomb();
                rb.useGravity = true;
            }
        }

        public void StartTickingTimer()
        {
            if (!isTicking) StartCoroutine(TickingTimer());
            StartCoroutine(SpawnNewBombDelay());
        }

        IEnumerator Flicker()
        {
            source.Play();
            float t = 0.0f;
            while (t < flickerDuration)
            {
                material.SetColor("_BaseColor", flickerGradient.Evaluate(t / flickerDuration));
                t += Time.deltaTime;
                yield return null;
            }
            material.SetColor("_BaseColor", flickerGradient.Evaluate(0));
        }

        IEnumerator TickingTimer()
        {
            rb.useGravity = true;
            isTicking = true;
            transform.SetParent(null);

            for (int i = 0; i < tickingDuration; i++)
            {
                StartCoroutine(Flicker());
                yield return second;
            }
            for (int i = 0; i < fastTickingDuration; i++)
            {
                StartCoroutine(Flicker());
                yield return halfSecond;
            }
            Explodes();
        }

        IEnumerator SpawnNewBombDelay()
        {
            yield return cooldown;
            SpawnNewBomb();
        }

        private void SpawnNewBomb()
        {
            if (bombSpawner.childCount == 0)
            {
                GameObject go = Instantiate(bombPrefab, bombSpawner);
                go.transform.localPosition = Vector3.zero;
            }
        }

        public void Explodes()
        {
            GameObject go = Instantiate(explosionParticles, transform.position, transform.rotation).gameObject;
            go.SetActive(true);
            Destroy(gameObject);
            Destroy(go, 3f);
            SpawnNewBomb();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Armor a;
            if(collision.gameObject.TryGetComponent<Armor>(out a))
            {
                Explodes();
            }

            Health h;
            if (collision.gameObject.TryGetComponent<Health>(out h))
            {
                Explodes();
            }
        }
    }
    #endregion
}