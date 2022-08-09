using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float duration = 0.05f;

    private XRBaseInteractable target;

    private void Awake()
    {
        target = GetComponent<XRBaseInteractable>();
    }

    public void Call()
    {
        if (target == null)
            return;
        if (target.firstInteractorSelecting == null)
            return;
        if (!(target.firstInteractorSelecting is XRBaseControllerInteractor))
            return;

        var interactor = target.firstInteractorSelecting as XRBaseControllerInteractor;
        if (interactor.xrController == null)
            return;

        interactor.xrController.SendHapticImpulse(amplitude, duration);
    }
}
