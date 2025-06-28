using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NPCDialogueSimple : MonoBehaviour
{
    [SerializeField] private string message = "Hello, traveler!";
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private Transform npcLookTarget;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        DialogueUI.Instance?.ShowMessage(message, displayDuration);
        FindObjectOfType<CameraFocusController>()?.FocusOnNPC();
    }

}
