using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;
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
    }
}
