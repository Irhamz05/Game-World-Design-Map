using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget; // Set this in the Inspector to the destination

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportTarget != null)
            {
                other.transform.position = teleportTarget.position;
            }
        }
    }
}