using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerModel Model { get; private set; }
    private PlayerView _view;
    private PlayerController _controller;
    

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public Inventory Inventory { get; private set; }
    public AttackSystem AttackSystem { get; private set; }
    public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public float StartLife { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public Joystick Joystick { get; private set; }
    [field: SerializeField] public GameObject DamageCollider { get; private set; }

    private void Awake()
    {
        StartLife = SaveData.life !=0 ? SaveData.life : StartLife ; // Asignar vida inicial desde SaveData
        CharacterController = GetComponent<CharacterController>();      
        Rigidbody = GetComponent<Rigidbody>();    
        Animator = GetComponent<Animator>();
        Inventory = GetComponent<Inventory>();
        
        if (DamageCollider == null)
        {
            Debug.LogError("Player: ¡DamageCollider no está asignado en el Inspector! El sistema de ataque no funcionará correctamente.");
        }
        
        AttackSystem = new AttackSystem(DamageCollider, transform);
        
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

    #region UIPublicMethods
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
    #endregion

    private IEnumerator DisableUIAttackModeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _controller.DisableUIAttackMode();
    }

    private void DisableComponent()
    {
        enabled = false;   
    }
    public void UpdateLife()
    {
        
        Model.AddLife();
    }

    public void UpdateSpeed()
    {
        
        Model.AddSpeed();
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(SimpleEventType.PlayerDeathEvent, DisableComponent);
    }
}
