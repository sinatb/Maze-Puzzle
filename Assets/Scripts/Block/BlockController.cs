using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private int _playerCount = 0;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private Light _light;
    [SerializeField] private bool _isFogBlock;
    [SerializeField] private bool _isFogNeighbour;
    [SerializeField] private bool _isFinishingBlock;
    [SerializeField] private Material _ceillingMat;
    [SerializeField] private Material _ceillingFogMat;
    [SerializeField] private Material _floorMat;
    [SerializeField] private GameObject _floorCeilling;
    
    #region Shader
    private void setMatDark()
    {
        List<Material> materials = new List<Material>()
                {
                    _floorMat,
                    _ceillingFogMat
                };
        _floorCeilling.GetComponent<Renderer>().SetMaterials(materials);
    }
    private void setMatLight()
    {
        List<Material> materials = new List<Material>() {
                    _floorMat,
                    _ceillingMat
                };
        _floorCeilling.GetComponent<Renderer>().SetMaterials(materials);
    }
    #endregion


    private void Start()
    {
        if (!_isFogBlock)
        {
            setMatLight();
            _light.intensity = _maxIntensity;        
        }
        else
        {
            setMatDark();
            _light.intensity = 0.0f;
        }
    }
    public void IncPlayerCount()
    {
        _playerCount++;
        if (_playerCount == 1)
        {
           _light.intensity /= 2;
        }else if (_playerCount == 2)
        {
            _light.intensity /= 1.5f;
        }else if ( _playerCount == 3)
        {
            setMatDark();
            _light.intensity = 0.0f;
        }

    }
    public int GetPlayerCount()
    {
        return _playerCount;
    }
    public bool GetIsFog()
    {
        return _isFogBlock;
    }
    public bool GetIsFinishing()
    {
        return _isFinishingBlock;
    }
    
}
