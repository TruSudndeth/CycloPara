using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class InstanceChecker<T> : IEnumerator where T : class
{
    private Action<T> _callback;
    private Action<T> _setInstance;
    private T _otherClassInstance;
    private T instance;

    public InstanceChecker(Action<T> callback, Action<T> setInstance)
    {
        _callback = callback;
        _setInstance = setInstance;
    }
    public object Current => null;

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public  void Reset()
    {
        throw new NotImplementedException();
    }
}