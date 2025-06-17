using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioClip pickupSfx;

    private AudioSource audioSource;
    private bool isCollected = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Rotate the coin around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true; // Prevent double pickup

            if (pickupSfx != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSfx);
                Destroy(gameObject, pickupSfx.length); // Delay destruction
            }
            else
            {
                Destroy(gameObject); // No sound, destroy immediately
            }
        }
    }
}
