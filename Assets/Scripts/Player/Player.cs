using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Inventory Inventory { get; private set; }
    private MovementController movementController;

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        if (Inventory == null)
        {
            Debug.LogError("No se encontró el componente Inventory en el Player.");
        }

        // Seleccionar el controlador según la plataforma
        #if UNITY_ANDROID || UNITY_IOS
            movementController = GetComponent<MobileController>();
            if (movementController == null)
            {
                movementController = gameObject.AddComponent<MobileController>();
            }
        #else
            movementController = GetComponent<DesktopController>();
            if (movementController == null)
            {
                movementController = gameObject.AddComponent<DesktopController>();
            }
        #endif

        // Configurar la velocidad
        if (movementController != null)
        {
            movementController.MoveSpeed = speed;
        }
        else
        {
            Debug.LogError("No se pudo inicializar el controlador de movimiento.");
        }
    }

    private void Update()
    {
        if (movementController == null) return;

        // El movimiento se maneja en el controlador

        // Input para pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Implementar lógica de pausa
        }
    }

    // Método público para ser llamado desde el botón de UI en móvil
    public void Attack()
    {
        if (Inventory != null)
        {
            Inventory.UseWeapon(this);
        }
    }
}
