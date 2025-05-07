using UnityEngine;

public class CameraFollowZoom : MonoBehaviour
{
    [Header("Target a seguir")]
    public Transform target;

    [Header("Configuración de seguimiento")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;

    [Header("Configuración de zoom")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    private float currentZoom;

    void Start()
    {
        currentZoom = offset.magnitude;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Zoom con el scroll del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Calcula nueva posición con zoom
        Vector3 direction = offset.normalized;
        Vector3 desiredPosition = target.position + direction * currentZoom;

        // Movimiento suave hacia el objetivo
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // Mira siempre hacia el objetivo
        transform.LookAt(target);
    }
}
