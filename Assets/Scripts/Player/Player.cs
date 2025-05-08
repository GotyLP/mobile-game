using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MobileController controller;
    [SerializeField] float speed;
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        if (Inventory == null)
        {
            Debug.LogError("No se encontró el componente Inventory en el Player. Asegúrate de que el componente Inventory esté adjunto al GameObject del Player.");
        }
    }

    private void Update()
    {
        if (controller == null) return;
        transform.position += controller.GetMovementInput() * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Boton de retorno del celular
        }
        
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            //Boton central del celular
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Inventory.UseWeapon(this);
        }
    }
}
