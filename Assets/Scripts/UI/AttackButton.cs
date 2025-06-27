using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("No se encontró el Player para el AttackButton");
            enabled = false;
        }
    }

    public void EventTriggerStartAttack()
    {
        if (player != null)
        {
            Debug.Log("EventTrigger: Iniciando ataque");
            player.StartAttack();
        }
    }

    public void EventTriggerStopAttack()
    {
        if (player != null)
        {
            Debug.Log("EventTrigger: Deteniendo ataque");
            player.StopAttack();
        }
    }

    public void EventTriggerPerformAttack()
    {
        if (player != null)
        {
            Debug.Log("EventTrigger: Ataque simple");
            player.PerformAttack();
        }
    }
} 