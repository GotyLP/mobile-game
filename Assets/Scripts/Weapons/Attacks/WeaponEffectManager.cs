using System.Collections.Generic;
using UnityEngine;


public class WeaponEffectManager : MonoBehaviour
{
    [Header("Decorators Disponibles")]
    [SerializeField] private List<GameObject> availableDecorators = new List<GameObject>();
    
    [Header("Configuración")]
    [SerializeField] private bool autoRegisterDecorators = true;
    
    private Dictionary<string, IAttackEffectDecorator> _registeredDecorators = new Dictionary<string, IAttackEffectDecorator>();
    private AttackEffectController _effectController;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("WeaponEffectManager: No se encontró componente Player");
        }
    }

    private void Start()
    {
        if (autoRegisterDecorators)
        {
            RegisterAvailableDecorators();
        }
    }

   
    public void Initialize(AttackEffectController effectController)
    {
        _effectController = effectController;
    }

   
    private void RegisterAvailableDecorators()
    {
        RegisterDecorator("poison", new PoisonEffectDecorator());
        
        Debug.Log($"WeaponEffectManager: Registrados {_registeredDecorators.Count} decorators");
    }

   
    public void RegisterDecorator(string id, IAttackEffectDecorator decorator)
    {
        if (_registeredDecorators.ContainsKey(id))
        {
            Debug.LogWarning($"WeaponEffectManager: Decorator con ID '{id}' ya existe, reemplazando...");
        }
        
        _registeredDecorators[id] = decorator;
        Debug.Log($"WeaponEffectManager: Registrado decorator '{decorator.DecoratorName}' con ID '{id}'");
    }

    
    public void ApplyDecorator(string decoratorID)
    {
        if (_effectController == null)
        {
            Debug.LogError("WeaponEffectManager: EffectController no inicializado");
            return;
        }

        if (_registeredDecorators.TryGetValue(decoratorID, out IAttackEffectDecorator decorator))
        {
            _effectController.RegisterDecorator(decorator);
            Debug.Log($"WeaponEffectManager: Aplicado decorator '{decorator.DecoratorName}'");
        }
        else
        {
            Debug.LogWarning($"WeaponEffectManager: Decorator con ID '{decoratorID}' no encontrado");
        }
    }

    
    public void RemoveDecorator(string decoratorID)
    {
        if (_effectController == null) return;

        if (_registeredDecorators.TryGetValue(decoratorID, out IAttackEffectDecorator decorator))
        {
            _effectController.UnregisterDecorator(decorator);
            Debug.Log($"WeaponEffectManager: Removido decorator '{decorator.DecoratorName}'");
        }
    }

    
    public void ClearAllDecorators()
    {
        if (_effectController != null)
        {
            _effectController.ClearDecorators();
            Debug.Log("WeaponEffectManager: Limpiados todos los decorators");
        }
    }

    
    public Dictionary<string, IAttackEffectDecorator> GetAvailableDecorators()
    {
        return new Dictionary<string, IAttackEffectDecorator>(_registeredDecorators);
    }

    
    public bool IsDecoratorActive(string decoratorID)
    {
        if (_effectController == null) return false;
        
        if (_registeredDecorators.TryGetValue(decoratorID, out IAttackEffectDecorator decorator))
        {
            return _effectController.GetDecorators().Contains(decorator);
        }
        return false;
    }

    public WeaponItem GetModifiedWeaponStats(WeaponItem originalWeapon)
    {
        if (_effectController == null) return originalWeapon;
        
        return _effectController.GetModifiedWeaponStats(originalWeapon);
    }

    public void ApplyStatusEffectsToTarget(IEntity target, WeaponItem weaponData)
    {
        if (_effectController != null)
        {
            _effectController.ApplyStatusEffectsToTarget(target, weaponData);
        }
    }

    #region Métodos Públicos para UI
    public void ActivatePoisonEffect()
    {
        ApplyDecorator("poison");
    }

    public void DeactivateAllEffects()
    {
        ClearAllDecorators();
    }
    #endregion
} 