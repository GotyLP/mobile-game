using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DEPRECATED: Rotation is now handled in PlayerModel for better MVC architecture
/// This component should be removed from the Player GameObject
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    private Vector3 lastPosition;

    void Start()
    {
        Debug.LogWarning("PlayerRotation: Este componente está deprecated. La rotación ahora se maneja en PlayerModel. Remover este componente del GameObject.");
        enabled = false; // Disable this component
    }

    void Update()
    {
        // Functionality moved to PlayerModel.Move()
        // This method is now empty to avoid conflicts
        /*
        Vector3 direction = transform.position - lastPosition;// Rotar hacia la direccin de movimiento
        if (direction.magnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        lastPosition = transform.position;
        */
    }
}
