using System.Collections.Generic;
using Unity.VisualScripting;

public class FSM<T>
{
    Dictionary<T, IState> _states = new Dictionary<T, IState>();

    IState _currentState;

    private T _currentKey;
    public T CurrentStateKey => _currentKey;

    public void AddState(T newState, IState state)
    {
        if (_states.ContainsKey(newState)) return;

        _states.Add(newState, state);
    }

    public void ArtificialUpdate()
    {
        if (_currentState != null)
            _currentState.OnUpdate();
    }

    public void ChangeState(T newState)
    {
        if (!_states.ContainsKey(newState)) return;

        if (_currentState == _states[newState]) return;

        if (_currentState != null)
            _currentState.OnExit();

        _currentState = _states[newState];
        _currentKey = newState;
        _currentState.OnEnter();
    }
}
