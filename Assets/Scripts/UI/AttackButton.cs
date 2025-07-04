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
            player.StartAttack();
        }
    }

    public void EventTriggerStopAttack()
    {
        if (player != null)
        {
            player.StopAttack();
        }
    }

    public void EventTriggerPerformAttack()
    {
        if (player != null)
        {
            player.PerformAttack();
        }
    }
} 