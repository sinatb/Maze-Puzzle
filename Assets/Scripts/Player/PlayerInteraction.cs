using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.0f;
    [SerializeField] private LayerMask _interactionMask;
    [SerializeField] private TextMeshProUGUI _tmp;
    private bool _isWatchingCollider = false;
    private RaycastHit _hit;
    private readonly Collider[] _colldiers = new Collider[3];

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colldiers, _interactionMask);   
        if (_colldiers[0] != null)
        {
            if (_isWatchingCollider) 
            {
                _tmp.text = "Press E to " + _colldiers[0].GetComponent<IInteractable>().InteractionName;
            }
            else
            {
                _tmp.text = "";
            }
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
            if (_colldiers[0] != null && _hit.transform.gameObject == _colldiers[0].gameObject && _hit.distance <= 2.0f)
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
