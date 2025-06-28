using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private TMP_Text dialogueText;

    private Coroutine hideRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Auto-assign text if not set
        if (dialogueText == null)
        {
            dialogueText = GetComponentInChildren<TMP_Text>();
        }
    }

    public void ShowMessage(string message, float duration)
    {
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
        }

        dialogueText.text = message;
        gameObject.SetActive(true);
        hideRoutine = StartCoroutine(HideAfterDelay(duration));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
