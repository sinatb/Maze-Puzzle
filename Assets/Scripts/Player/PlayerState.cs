using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float _Sanity = 2000.0f;
    [SerializeField] private float _Score = 0.0f;
    [SerializeField] private float _BaseLoss = 5.0f;
    [SerializeField] private float _LightPenalty = 0.0f;
    [SerializeField] private float _FlashlightExtra = 3.0f;
    [SerializeField] private float _PuzzleMistakePenalty = -50.0f;
    [SerializeField] private float _PuzzleSolveExtra = 100.0f;
    private PlayerInventory _playerInventory;
    private void updateState() 
    {
        if (_playerInventory.HasItem("flashlight"))
        {
            _Sanity += _FlashlightExtra;
        }
        _Sanity -= _BaseLoss + _LightPenalty;
    }
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        InvokeRepeating("updateState", 0.0f,1.0f);
    }

    private void Update()
    {
        if (_Sanity <= 0.0f)
        {
            //TODO : Game over Logic
        }        
    }

    //TODO : Add room light penalty into effect
    private void getRoomLightPenalty()
    {

    }

    public float GetSanity()
    {
        return _Sanity;
    }
}
