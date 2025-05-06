using System.Collections.Generic;
using System;

public class ObjectPool<T>
{
    private List<T> _currentStock;

    private Func<T> _creationMethod;
    private Action<T> _turnOnMethod;
    private Action<T> _turnOffMethod;

    public ObjectPool(Func<T> creationMethod, Action<T> turnOnMethod, Action<T> turnOffMethod, int initialAmount)
    {
        _currentStock = new List<T>();

        _creationMethod = creationMethod;
        _turnOnMethod = turnOnMethod;
        _turnOffMethod = turnOffMethod;

        for (int i = 0; i < initialAmount; i++)
        {
            var newObj = _creationMethod();
            _currentStock.Add(newObj);
            _turnOffMethod(newObj);
        }
    }

    public T GetObject()
    {
        T objectToReturn = default;

        if (_currentStock.Count == 0)
        {
            objectToReturn = _creationMethod();
        }
        else
        {
            objectToReturn = _currentStock[0];
            _currentStock.RemoveAt(0);
        }

        _turnOnMethod(objectToReturn);

        return objectToReturn;
    }

    public void ReturnToPool(T obj)
    {
        _turnOffMethod(obj);
        _currentStock.Add(obj);
    }
}