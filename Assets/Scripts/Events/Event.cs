using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Events/Event")]
    public class Event : ScriptableObject
    {
        private readonly List<IEventListener> _eventListeners = new List<IEventListener>();


        public void Raise()
        {
            foreach (var l in _eventListeners)
            {
                l.OnRaise();
            }
        }

        public void RegisterListener(IEventListener l)
        {
            if (!_eventListeners.Contains(l))
                _eventListeners.Add(l);
        }

        public void RemoveListener(IEventListener l)
        {
            if (_eventListeners.Contains(l))
                _eventListeners.Remove(l);
        }
    }
}
