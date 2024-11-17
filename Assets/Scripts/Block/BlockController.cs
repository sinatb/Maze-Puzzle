using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private int _playerCount = 0;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _maxFogNeighbourIntensity;
    [SerializeField] private Light _light;
    [SerializeField] private bool _isFogBlock;
    [SerializeField] private bool _isFogNeighbour;
    [SerializeField] private Material _ceillingMat;
    [SerializeField] private Material _ceillingFogMat;
    [SerializeField] private Material _floorMat;
    [SerializeField] private GameObject _floorCeilling;
    private void Start()
    {
        if (_isFogNeighbour)
            _light.intensity = _maxFogNeighbourIntensity;
        else
        {
            if (!_isFogBlock)
            {
                List<Material> materials = new List<Material>() {
                    _floorMat,
                    _ceillingMat
                };
                _floorCeilling.GetComponent<Renderer>().SetMaterials(materials);                
                _light.intensity = _maxIntensity;        
            }
            else
            {
                List<Material> materials = new List<Material>()
                {
                    _floorMat,
                    _ceillingFogMat
                };
                _floorCeilling.GetComponent<Renderer>().SetMaterials(materials);
                _light.intensity = 0.0f;
            }
        }
    }
    public void IncPlayerCount()
    {
        _playerCount++;

    }

    
}
