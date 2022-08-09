using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public InputActionReference inputActionRef;

    public UnityEvent OnShow;
    public UnityEvent OnHide;

    private void OnEnable()
    {
        inputActionRef.action.performed += OnPerformed;
        inputActionRef.action.canceled += OnCanceled;
    }

    private void OnDisable()
    {
        inputActionRef.action.performed -= OnPerformed;
        inputActionRef.action.canceled -= OnCanceled;
    }

    public void OnPerformed(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnShow));
    }

    public void OnCanceled(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnHide));
    }

    private IEnumerator DelayCall(UnityEvent e)
    {
        yield return null;
        e?.Invoke();
    }
}