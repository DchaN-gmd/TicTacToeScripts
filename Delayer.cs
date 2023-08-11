using System;
using System.Collections;
using UnityEngine;

public class Delayer : MonoBehaviour
{
    private static Delayer _delayer;
    private static Coroutine _coroutine;

    private void Awake()
    {
        _delayer = this;
    }

    public static void CallMethodWithDelay(float delay, Action method)
    {
        if (_coroutine != null) return;
        _coroutine = _delayer.StartCoroutine(CallingMethod(delay, method));
    }

    private static IEnumerator CallingMethod(float delay, Action method)
    {
        yield return new WaitForSeconds(delay);
        method?.Invoke();
        _coroutine = null;
    }
}
