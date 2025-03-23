using UnityEngine;
using UnityEngine.Events;

public class TutoEnd : MonoBehaviour
{
    public UnityEvent tutoExitedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) tutoExitedEvent.Invoke();
    }
}
