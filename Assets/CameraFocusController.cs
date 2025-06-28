using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraFocusController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineCamera npcCam;
    [SerializeField] private float focusTime = 3f;

    private bool isSwitching;

    public void FocusOnNPC()
    {
        if (isSwitching) return;
        StartCoroutine(SwitchToNPC());
    }

    private IEnumerator SwitchToNPC()
    {
        isSwitching = true;

        npcCam.Priority = 20;
        playerCam.Priority = 10;

        yield return new WaitForSeconds(focusTime);

        npcCam.Priority = 10;
        playerCam.Priority = 20;

        isSwitching = false;
    }
}
