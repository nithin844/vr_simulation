using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class WeldingPathTracker : MonoBehaviour
{
    public Transform rayOrigin; 
    public float rayDistance = 0.1f;
    public LayerMask weldableLayer;
    public LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem particles;

    private List<Vector3> weldPoints = new List<Vector3>();

    public InputActionProperty triggerinput;
    public InputActionProperty grabvalue;

    private bool isWelding = false;

    private void Start()
    {
        lineRenderer.positionCount = 0;
        if (particles != null)
        {
            particles.Stop();
        }
    }

    private void Update()
    {
        float value = triggerinput.action.ReadValue<float>();
        float grabValue = grabvalue.action.ReadValue<float>();
        Debug.Log(grabValue);

        bool isTriggerHeld = (value > 0.1f && grabValue > 0.1f);

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, 2f, weldableLayer))
        {
            if (isTriggerHeld)
            {
                if (particles != null)
                {
                    particles.transform.position = hit.point;
                    if (!particles.isPlaying)
                        particles.Play();
                }

                Vector3 weldPoint = hit.point;

                if (!isWelding)
                {
                    isWelding = true;
                    weldPoints.Clear();
                    lineRenderer.positionCount = 0;
                }

                if (weldPoints.Count == 0 || Vector3.Distance(weldPoints[weldPoints.Count - 1], weldPoint) > 0.005f)
                {
                    weldPoints.Add(weldPoint);
                    lineRenderer.positionCount = weldPoints.Count;
                    lineRenderer.SetPositions(weldPoints.ToArray());
                }
            }
            else
            {
                StopWelding();
            }
        }
        else
        {
            StopWelding();
        }
    }

    private void StopWelding()
    {
        if (particles != null && particles.isPlaying)
            particles.Stop();

        isWelding = false;
    }
}
