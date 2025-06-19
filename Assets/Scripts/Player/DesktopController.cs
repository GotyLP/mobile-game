using UnityEngine;
using System.Collections;

public class DesktopController : MovementController
{
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    private Player player;
    private Vector3 currentInput;

    private void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        currentInput = new Vector3(horizontal, 0, vertical).normalized;

        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAttack();
        }
    }

    public override Vector3 GetMovementInput()
    {
        return currentInput;
    }

    public void Attack()
    {
        if (player != null && player.Inventory != null)
        {
            player.Inventory.UseWeapon(player);
            StartCoroutine(AttackCooldown());
        }
    }

    public void StopAttack()
    {
        if (player != null && player.Inventory != null)
        {
            player.Inventory.StopWeapon();
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
} 