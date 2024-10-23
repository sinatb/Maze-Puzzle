using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionName = "Open Door";
    public string InteractionName => _interactionName;
    private Animator _animator;
    private bool _isOpen = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();  
    }
    private void ToggleDoor()
    {
       if (_isOpen) {
            _animator.Play("door_close", 0, 0.0f);
            _isOpen = false;
       } else {
            _animator.Play("door_open", 0, 0.0f);
            _isOpen = true;
       }
    }
    public void Interact(PlayerInteraction pi)
    {
        ToggleDoor();
    }
}
