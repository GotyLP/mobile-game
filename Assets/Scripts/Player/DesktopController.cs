using UnityEngine;
using System.Collections;

public class DesktopController : MovementController
{
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Update()
    {
        // Obtener input del teclado (WASD y flechas)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("W o UpArrow");
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("S o DownArrow");
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("A o LeftArrow");
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("D o RightArrow");
            MoveRight();
        }
        else
        {
            Debug.Log("Stop");
            Stop();
        }

        // Aplicar el movimiento directamente
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Control de ataque con mouse
        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAttack();
        }
    }

    private void Attack()
    {
        if (player != null && player.Inventory != null)
        {
            player.Inventory.UseWeapon(player);
            StartCoroutine(AttackCooldown());
        }
    }

    private void StopAttack()
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

    public override Vector3 GetMovementInput()
    {
        return moveDirection;
    }

    public override void MoveUp()
    {
        moveDirection = Vector3.forward;
    }

    public override void MoveDown()
    {
        moveDirection = Vector3.back;
    }

    public override void MoveLeft()
    {
        moveDirection = Vector3.left;
    }

    public override void MoveRight()
    {
        moveDirection = Vector3.right;
    }

    public override void Stop()
    {
        moveDirection = Vector3.zero;
    }
} 