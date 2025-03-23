using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Spawner[] Spawners;

    private void Start()
    {
        Spawners = gameObject.GetComponentsInChildren<Spawner>();
    }

    public void OnTutoExited()
    {
        CustomDebugger.log("tuto exited");
        foreach (var spawner in Spawners)
        {
            spawner.activated = true;
        }
    }
}
