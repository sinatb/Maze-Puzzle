using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionName = "Open Door";
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public string InteractionName => _interactionName;
    private Animator _animator;
    private bool _isOpen = false;
    private AudioSource _audioSource;
    private void Start()
    {
        _animator = GetComponent<Animator>();  
        _audioSource = GetComponent<AudioSource>();
    }
    private void ToggleDoor()
    {
       if (_isOpen) {
            _animator.Play("door_close", 0, 0.0f);
            _audioSource.PlayOneShot(doorClose);
            _isOpen = false;
            _interactionName = "Open Door";
       } else {
            _animator.Play("door_open", 0, 0.0f);
            _audioSource.PlayOneShot(doorOpen);
            _isOpen = true;
            _interactionName = "Close Door";
       }
    }
    public void Interact(PlayerInteraction pi)
    {
        ToggleDoor();
    }
}
