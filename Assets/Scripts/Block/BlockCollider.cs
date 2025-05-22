using System.Collections;
using System.Collections.Generic;
using Block;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    [SerializeField] private BlockController _blockController;
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _blockController.IncPlayerCount();
        }
    }
    public BlockController GetBlockController()
    {
        return _blockController;
    }
}
