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
        Model.Attack();
        
        // Si estamos usando MobileInputProvider, también actualizamos su estado
        if (_controller != null && Application.isMobilePlatform)
        {
            var mobileProvider = _controller.GetInputProvider() as MobileInputProvider;
            mobileProvider?.StartAttack();
        }
    }

    public void StopAttack()
    {
        Model.StopAttack();
        
        // Si estamos usando MobileInputProvider, también actualizamos su estado
        if (_controller != null && Application.isMobilePlatform)
        {
            var mobileProvider = _controller.GetInputProvider() as MobileInputProvider;
            mobileProvider?.StopAttack();
        }
    }

    // Método de conveniencia para UI que maneja todo el ataque
    public void PerformAttack()
    {
        Model.Attack();
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
