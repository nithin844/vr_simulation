using UnityEngine;

public class WeldingTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera weldingCamera;

    private void Start()
    {
        // Ensure only main camera is active at first
        mainCamera.enabled = true;
        weldingCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached welding setup");

            // Switch to welding camera
            mainCamera.enabled = false;
            weldingCamera.enabled = true;

            // Optionally, start welding logic here
            // e.g., enable welding script
        }
    }
}
