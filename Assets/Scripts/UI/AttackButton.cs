using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Player player;
    
    // Si no asignas el player en el inspector, se buscará automáticamente
    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (attackButton == null)
        {
            attackButton = GetComponent<Button>();
        }

        if (player == null)
        {
            Debug.LogError("No se encontró el Player para el AttackButton");
            enabled = false;
        }
    }

    // Método para ser llamado desde el botón (OnClick event)
    public void OnAttackButtonClick()
    {
        if (player != null)
        {
            player.PerformAttack();
        }
    }

    // Para ataque continuo - presionar y mantener
    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
        {
            player.StartAttack();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
        {
            player.StopAttack();
        }
    }

    // Método alternativo para llamar desde eventos de UI
    public void StartAttacking()
    {
        if (player != null)
        {
            player.StartAttack();
        }
    }

    public void StopAttacking()
    {
        if (player != null)
        {
            player.StopAttack();
        }
    }
} 