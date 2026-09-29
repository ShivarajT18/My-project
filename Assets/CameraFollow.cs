using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Drag your Player object here
    public float smoothSpeed = 0.125f; // Higher means faster snapping
    public Vector3 offset = new Vector3(0, 0, -10); // Keep the camera back from the 2D plane

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;
            
            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Apply the position
            transform.position = smoothedPosition;
        }
    }
}