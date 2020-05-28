using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateComponent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent myEvent;
    // Start is called before the first frame update

    public void OnActivate()
    {
        myEvent.Invoke();
    }

}
