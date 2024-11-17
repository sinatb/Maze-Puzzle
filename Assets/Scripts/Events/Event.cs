using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event", menuName = "Events/Event")]
public class Event : ScriptableObject
{
    private readonly List<IEventListener> eventListeners = new List<IEventListener>();


    public void Raise()
    {
        foreach (var l in eventListeners)
        {
            l.OnRaise();
        }
    }

    public void RegisterListener(IEventListener l)
    {
        if (!eventListeners.Contains(l))
            eventListeners.Add(l);
    }

    public void RemoveListener(IEventListener l)
    {
        if (eventListeners.Contains(l))
            eventListeners.Remove(l);
    }
}
