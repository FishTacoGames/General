using System.Collections;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField]
    private Light source;
    [SerializeField]
    private float duration;
    private float startIntensity;
    //private Coroutine toggleRoutine;
    private void Start()
    {
        startIntensity = source.intensity;
       //Toggle();
    }
    public void Toggle()
    {
        ToggleLight();
    }
    private void ToggleLight()
    {
        //if (toggleRoutine != null)
        //    StopCoroutine(toggleRoutine);
        //toggleRoutine = StartCoroutine(ToggleSource());

        StartCoroutine(ToggleSource());
    }
    private IEnumerator ToggleSource()
    {
        bool off = !source.enabled;
        if (off)
            source.enabled = true;
        float t = 0;
        float startTarget = off ? 0 : startIntensity;
        float endTarget = off ? startIntensity : 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.intensity = Mathf.Lerp(startTarget, endTarget, t);
            yield return null;
        }
        if (!off)
            source.enabled = false;
      //  Toggle();
    }
}
