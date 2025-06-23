using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;              
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;
    public LayerMask objectToHide;      

    private List<Renderer> hiddenObjects = new List<Renderer>();

    [Header("Configuración de zoom")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    private float currentZoom;
    private void Start()
    {
        currentZoom = offset.magnitude;
    }

    void LateUpdate()
    {
        Zoom();
        if (target == null)
            return;

        // Suavizado de la cámara al moverse
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

         
        RestoreHiddenObjects();

        // Raycast desde la cámara hacia el jugador
        Vector3 directionToPlayer = target.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToPlayer, distanceToPlayer, objectToHide);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null && rend.enabled)
            {
                rend.enabled = false;
                hiddenObjects.Add(rend);
            }
        }
    }
    void Zoom()
    {
        // Zoom con el scroll del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Calcula nueva posición con zoom
        Vector3 direction = offset.normalized;
        Vector3 desiredPosition = target.position + direction * currentZoom;
    }

    void RestoreHiddenObjects() //recupera objetos ocultos en el frame anterior
    {
        foreach (Renderer rend in hiddenObjects)
        {
            if (rend != null)
                rend.enabled = true;
        }
        hiddenObjects.Clear();
    }
}
