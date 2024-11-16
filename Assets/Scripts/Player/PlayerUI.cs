using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionText;
    [SerializeField] private float _alertDisplayTime;
    [SerializeField] private Color _displayColor;
    [SerializeField] private Color _alertColor;
    [SerializeField] private Slider _sanitySlider;
    private bool _isLocked = false;
    private PlayerState _state;

    private void Start()
    {
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        _sanitySlider.value =( _state.GetSanity()/2000.0f);
    }
    private IEnumerator alertDisplayDelay()
    {
        yield return new WaitForSeconds(_alertDisplayTime);
        _isLocked = false;
        _actionText.color = _displayColor;
        ClearText();
    }
    public void SetText(string txt)
    {
        if (!_isLocked)
            _actionText.text = txt;
    }
    public void ClearText()
    {
        SetText("");
    }
    
    public void DisplayAlert(string alert)
    {
        _actionText.color = _alertColor;
        SetText(alert);
        _isLocked = true;
        StartCoroutine(alertDisplayDelay());
    }

}
