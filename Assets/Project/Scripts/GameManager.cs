using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameManager : MonoBehaviour
{
    public static Spawner[] Spawners;
    private bool _gameStarted;
    private NearFarInteractor _leftNearFarInteractor;
    private NearFarInteractor _rightNearFarInteractor;
    [SerializeField] private GameObject UI;
    [SerializeField] private Text UIText;

    private void Start()
    {
        Spawners = gameObject.GetComponentsInChildren<Spawner>();
        _leftNearFarInteractor = XRData.Instance.leftHand.GetComponentInChildren<NearFarInteractor>();
        _rightNearFarInteractor = XRData.Instance.rightHand.GetComponentInChildren<NearFarInteractor>();
        UI.SetActive(false);
        //ActivateMenu();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        UIText.text = "Game Over!";
        ActivateMenu();
    }

    public void OnPlayerVictory()
    {
        UIText.text = "Victory!";
        ActivateMenu();
    }

    private void ActivateMenu()
    {
        _leftNearFarInteractor.enableFarCasting = true;
        _rightNearFarInteractor.enableFarCasting = true;
        UI.SetActive(true);
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
