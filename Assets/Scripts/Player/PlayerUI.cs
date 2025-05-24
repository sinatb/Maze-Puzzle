using System.Collections;
using System.Collections.Generic;
using Player;
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
    [SerializeField] private GameObject _sanityUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameWinUI;
    [SerializeField] private TextMeshProUGUI _gameScoreText;
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _pointText;
    private bool _isLocked = false;
    private PlayerState _state;

    private void Start()
    {
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        _sanitySlider.value = (_state.GetSanity() / 2000.0f);
        _pointText.text = "Points : " + _state.GetScore().ToString();
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
    private void setScoreText()
    {
        int gs = GetComponent<PlayerState>().CalculateWinScore();
        _gameScoreText.text = "Your score is : " + gs;
    }
    private void setRankText()
    {
        string rankText = GetComponent<PlayerState>().CalculateRank();
        _rankText.text = "Rank : " + rankText;
    }
    public void DisplayAlert(string alert)
    {
        _actionText.color = _alertColor;
        SetText(alert);
        _isLocked = true;
        StartCoroutine(alertDisplayDelay());
    }
    public void Pause()
    {
        StopAllCoroutines();
        ClearText();
        _isLocked = true;
    }
    public void UnPause()
    {
        StopAllCoroutines();
        ClearText();
        _isLocked = false;
    }
    public void GameOver()
    {
        StopAllCoroutines();
        ClearText();
        _isLocked = true;
        _sanityUI.SetActive(false);
        _gameOverUI.SetActive(true);
    }
    public void GameWin()
    {
        StopAllCoroutines();
        ClearText();
        _isLocked = true;
        _sanityUI.SetActive(false);
        _gameWinUI.SetActive(true);
        setScoreText();
        setRankText();
    }
}
