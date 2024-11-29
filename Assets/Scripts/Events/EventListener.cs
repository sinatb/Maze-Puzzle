using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour, IEventListener
{
    [SerializeField]
    private Event _event;
    [SerializeField]
    private UnityEvent _response;
    private void OnEnable()
    {
        if ( _event != null )
            _event.RegisterListener(this);
    }
    public void OnDisable()
    {
        if ( _event != null )
            _event.RemoveListener(this);
    }

    public void OnRaise()
    {
        _response?.Invoke();
    }
}
