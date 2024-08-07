using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public float attractionForce = 10f;
    public float attractionRadius = 5f;

    private void OnTriggerStay(Collider other)
    {      
        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            // Calculate the direction from this object to the other object
            Vector3 direction = transform.position - other.transform.position;
            float distance = direction.magnitude;
            direction.Normalize();
            if (distance <= attractionRadius)
                rb.AddForce(attractionForce * Time.deltaTime * direction, ForceMode.Force);
        }
    }
}
