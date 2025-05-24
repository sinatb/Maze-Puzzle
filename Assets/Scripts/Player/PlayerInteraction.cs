using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private float _interactionPointRadius = 1.0f;
        [SerializeField] private LayerMask _interactionMask;
        private PlayerUI _ui;
        private bool _isWatchingCollider = false;
        private RaycastHit _hit;
        private PlayerState _state;
        private readonly Collider[] _colliders = new Collider[3];
        private void Start()
        {
            _ui = GetComponent<PlayerUI>();
            _state = GetComponent<PlayerState>();
        }
    
        private void Update()
        {
            Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactionMask);   
            if (_colliders[0] != null && _state.IsGameRunning && !_state.IsGamePaused)
            {
                if (_isWatchingCollider) 
                {
                    _ui.SetText(_colliders[0].GetComponent<IInteractable>().InteractionName);
                }
                else
                {
                    _ui.ClearText();
                }
                if (Input.GetKeyUp(KeyCode.E) && _isWatchingCollider)
                {
                    _colliders[0].GetComponent<IInteractable>().Interact(this);
                }
            }
        }
        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out _hit, _interactionMask))
            {
                if (_colliders[0] != null && _hit.transform.gameObject == _colliders[0].gameObject && _hit.distance <= 2.0f)
                {
                    _isWatchingCollider = true;
                }
                else
                {
                    _isWatchingCollider = false;
                }
            }
        }
    }
}
