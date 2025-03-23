using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessControl : MonoBehaviour
{
    [SerializeField] private AnimationCurve vignetteFlashCurve;
    private Volume volume;
    private Vignette vignette;
    public static PostProcessControl Instance;

    void Start()
    {
        Instance = this;
        volume = GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
    }

    public IEnumerator DamageVignetteFlicker()
    {
        var length = vignetteFlashCurve.keys[^1].time;
        var timer = 0f;
        while (timer < length)
        {
            timer += Time.deltaTime;
            vignette.intensity.value = vignetteFlashCurve.Evaluate(timer);
            yield return null;
        }
    }

    // private IEnumerator PostProcessTest()
    // {
    //     while(true)
    //     {
    //         vignette.intensity.value = 0.5f;
    //         yield return new WaitForSeconds(1f);
    //         vignette.intensity.value = 0.8f;
    //         Debug.Log(vignette.intensity.value);
    //         yield return new WaitForSeconds(1f);
    //     }
    // }
}
