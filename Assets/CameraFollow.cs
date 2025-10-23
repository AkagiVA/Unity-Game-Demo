// 10/22/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private void Update()
    {
        if (mainCamera != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            mainCamera.orthographicSize -= scroll * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
    }
}