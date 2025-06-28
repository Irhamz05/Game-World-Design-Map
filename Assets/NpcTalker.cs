using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using TMPro;
using System.Collections;

public class NpcTalker : MonoBehaviour
{
    [SerializeField] private string npcID = "npc_guard_001";

    [TextArea(2, 4)]
    [SerializeField] private string[] dialogueLines;

    [SerializeField] private AudioClip[] voiceLines;

    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueUI;
    [SerializeField] private float talkDuration = 3f;

    [SerializeField] private CinemachineCamera npcCam;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private Transform focusPoint;

    private AudioSource audioSource;
    private bool hasTalked = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (CheckpointManager.Instance != null && CheckpointManager.Instance.WasNpcCleared(npcID))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTalked) return;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TalkToPlayer(other.gameObject));
        }
    }

    private IEnumerator TalkToPlayer(GameObject player)
    {
        hasTalked = true;

        var input = player.GetComponent<StarterAssets.StarterAssetsInputs>();
        var controller = player.GetComponent<StarterAssets.ThirdPersonController>();
        var animator = player.GetComponent<Animator>();

        if (input != null) input.enabled = false;
        if (controller != null) controller.enabled = false;
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("MotionSpeed", 0f);
        }

        npcCam.Follow = null;
        npcCam.LookAt = focusPoint;
        npcCam.Activate();
        playerCam.Deactivate();

        dialogueCanvas.enabled = true;

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogueUI.text = dialogueLines[i];

            if (voiceLines != null && i < voiceLines.Length && voiceLines[i] != null)
            {
                audioSource.Stop();
                audioSource.clip = voiceLines[i];
                audioSource.Play();
            }

            yield return new WaitForSeconds(talkDuration);
        }

        dialogueCanvas.enabled = false;

        npcCam.Deactivate();
        playerCam.Activate();

        if (input != null) input.enabled = true;
        if (controller != null) controller.enabled = true;

        CheckpointManager.Instance.MarkNpcCleared(npcID);
    }
}
