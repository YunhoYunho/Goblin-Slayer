using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterActionAdapter : MonoBehaviour, IInteractable
{
    public UnityEvent OnInterAction;

    public void InterAction()
    {
        OnInterAction?.Invoke();
    }
}
