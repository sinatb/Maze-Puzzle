using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.0f;
    [SerializeField] private LayerMask _interactionMask;
    private bool _isWatchingCollider = false;
    private RaycastHit _hit;
    private readonly Collider[] _colldiers = new Collider[3];

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colldiers, _interactionMask);   
        if (_colldiers[0] != null)
        {
            if (Input.GetKeyUp(KeyCode.E) && _isWatchingCollider)
            {
                _colldiers[0].GetComponent<IInteractable>().Interact(this);
            }
        }
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out _hit, _interactionMask))
        {
            if (_colldiers[0] != null && _hit.transform.gameObject == _colldiers[0].gameObject)
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
