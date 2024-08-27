using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private GameObject SpikeTrap;
    [SerializeField]
    private float trapDelay = 0.3f;
    [SerializeField]
    private float duration = 0.2f;
    [SerializeField]
    private float returnDuration = 1f;
    [SerializeField]
    private float distance = 5f;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        startPos = SpikeTrap.transform.position;
        endPos = startPos + Vector3.forward * distance;
       // Interact();
    }
    public void Interact()
    {
        TriggerTrap();
    }
    private void TriggerTrap()
    {
        StartCoroutine(TrapAction());
    }

    private IEnumerator TrapAction()
    {
        yield return new WaitForSeconds(trapDelay);
        // Move the spike forward
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / duration;
            SpikeTrap.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime);
            yield return null;
        }
        SpikeTrap.transform.position = endPos;

        yield return new WaitForSeconds(trapDelay / 2);

        // Move the spike back
        t = 0;
        while (t < returnDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / returnDuration);
            SpikeTrap.transform.position = Vector3.Lerp(endPos, startPos, normalizedTime);
            yield return null;
        }
        SpikeTrap.transform.position = startPos;
    }
}
