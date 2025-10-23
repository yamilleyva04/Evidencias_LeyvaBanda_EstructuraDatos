using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.125f;

    private Vector3 offset = new Vector3(0f, 0f, -15f);

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null || GameManager.Instance.IsGamePaused())
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        transform.position = smoothedPosition;
    }
}