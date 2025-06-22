using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerModel Model { get; private set; }
    private PlayerView _view;
    private PlayerController _controller;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public Inventory Inventory { get; private set; }
    public AttackSystem AttackSystem { get; private set; }

    [field: SerializeField] public float StartLife { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public Joystick Joystick { get; private set; }
    [field: SerializeField] public GameObject DamageCollider { get; private set; }

    private void Awake()
    {
        // Obtener componentes requeridos
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Inventory = GetComponent<Inventory>();
        
        // Verificar DamageCollider antes de inicializar AttackSystem
        Debug.Log($"Player: DamageCollider asignado: {DamageCollider?.name ?? "NULL"}");
        if (DamageCollider == null)
        {
            Debug.LogError("Player: ¡DamageCollider no está asignado en el Inspector! El sistema de ataque no funcionará correctamente.");
        }
        
        // Inicializar sistema de ataque (no es componente)
        AttackSystem = new AttackSystem(DamageCollider, transform);
        
        // Inicializar MVC
        Model = new PlayerModel(this);
        _view = new PlayerView(this);
        _controller = new PlayerController(this);

        EventManager.Subscribe(SimpleEventType.PlayerDeathEvent, DisableComponent);
    }

    void Update()
    {
        _controller.UpdateInputs();
    }

    private void FixedUpdate()
    {
        _controller.FixUpdateInputs();
    }

    // Métodos públicos para ser llamados desde UI
    public void StartAttack()
    {
        Debug.Log("Player: StartAttack() llamado desde UI");
        _controller.EnableUIAttackMode(); // Activar modo UI
        Model.Attack();
        
        if (_controller != null && Application.isMobilePlatform)
        {
            var mobileProvider = _controller.GetInputProvider() as MobileInputProvider;
            mobileProvider?.StartAttack();
        }
    }

    public void StopAttack()
    {
        Debug.Log("Player: StopAttack() llamado desde UI");
        Model.StopAttack();
        _controller.DisableUIAttackMode(); // Desactivar modo UI
        
        if (_controller != null && Application.isMobilePlatform)
        {
            var mobileProvider = _controller.GetInputProvider() as MobileInputProvider;
            mobileProvider?.StopAttack();
        }
    }

    public void PerformAttack()
    {
        Debug.Log("Player: PerformAttack() llamado desde UI");
        _controller.EnableUIAttackMode(); // Activar modo UI temporalmente
        Model.Attack();
        
        // Para un ataque simple, desactivamos el modo UI después de un breve delay
        StartCoroutine(DisableUIAttackModeAfterDelay(0.1f));
    }

    private IEnumerator DisableUIAttackModeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _controller.DisableUIAttackMode();
    }

    private void DisableComponent()
    {
        enabled = false;   
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(SimpleEventType.PlayerDeathEvent, DisableComponent);
    }
}
