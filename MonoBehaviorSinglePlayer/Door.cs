using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{// rotate this objects Y 90 degrees based on the door state
    [SerializeField] // Can be left empty
    private AudioClip DoorSound;
    [SerializeField]
    private float RotationDuration = 1f;

   // [SerializeField]
    //private Transform DummyPlayer;
    private bool doorOpen = false;
    private bool front;
    private Vector3 originalRotation;
    private Coroutine rotationCoroutine;
    private static readonly WaitForEndOfFrame waitForEndOfFrame = new();
    private void Start()
    {
        originalRotation = transform.localEulerAngles;
       // Rotate(DummyPlayer);
    }
    public void Interact(Transform player)
    {
        Rotate(player);
    }
    private void Rotate(Transform player)
    {
        front = Vector3.Dot(transform.forward, player.position - transform.position) > 0;
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        if (DoorSound != null)
            AudioSource.PlayClipAtPoint(DoorSound, transform.position, 0.5f);
        rotationCoroutine = StartCoroutine(RotateDoor(doorOpen ? 0f : (front ? 90f : -90f)));
        doorOpen = !doorOpen;
    }
    private IEnumerator RotateDoor(float angle)
    {
        Vector3 startRotation = transform.localEulerAngles;
        Vector3 endRotation = originalRotation + new Vector3(0, angle, 0);
        float timeElapsed = 0f;
        Vector3 shortestEndRotation = startRotation + new Vector3(0, Mathf.DeltaAngle(startRotation.y, endRotation.y), 0);
        while (timeElapsed < RotationDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(startRotation, shortestEndRotation, timeElapsed / RotationDuration);
            timeElapsed += Time.deltaTime;
            yield return waitForEndOfFrame;
        }
        transform.localEulerAngles = endRotation;
       // Rotate(DummyPlayer);
    }
}
