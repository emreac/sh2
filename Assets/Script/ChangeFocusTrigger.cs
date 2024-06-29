using UnityEngine;
using Cinemachine;

public class ChangeFocusTrigger : MonoBehaviour
{
    public Transform newFocusPoint;
    public CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building")) // Assuming the player has the tag "Player"
        {
            virtualCamera.LookAt = newFocusPoint;
        }
    }
}
