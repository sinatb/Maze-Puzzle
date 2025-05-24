using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactionRange;
        [SerializeField] private LayerMask interactionMask;
        private PlayerUI              _ui;
        private PlayerState           _state;
        private GameObject            _camera;
        private readonly RaycastHit[] _hits = new RaycastHit[5];
        private void Start()
        {
            _ui = GetComponent<PlayerUI>();
            _state = GetComponent<PlayerState>();
            _camera = transform.Find("Main Camera").gameObject;
        }
        private void FixedUpdate()
        {
            
            // Count of all Interactables the player is looking at
            var count = Physics.RaycastNonAlloc(_camera.transform.position,
                _camera.transform.forward,
                _hits,
                interactionRange,
                interactionMask
                );
            
            // Check if the object is within interaction distance
            if (count > 0 && _hits[0].distance <= interactionRange)
            {
                var interactable = _hits[0].transform.GetComponent<IInteractable>();
                if (_state.IsGameRunning && !_state.IsGamePaused)
                {
                    _ui.SetText(interactable.InteractionName);
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        interactable.Interact(this);
                    }
                }
                else
                {
                    _ui.ClearText();
                }
            }
            else
            {
                _ui.ClearText();
            }
        }
    }
}
