using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CodePuzzle : Puzzle
{
    private int _strLen;
    private string _answer;
    private string _playerInput;
    private bool _exitButtonPress;
    [SerializeField] private List<TextMeshProUGUI> _ciphertexts = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _plaintexts = new List<TextMeshProUGUI>();
    private string rndString(string alphabet)
    {
        StringBuilder sb = new StringBuilder();
        for (int i=0; i<_strLen; i++)
        {
            int rnd = Random.Range(0, alphabet.Length);
            sb.Append(alphabet[rnd]);
            alphabet = alphabet.Remove(rnd,1);
        }
        return sb.ToString();
    }
    private string getNewAlphabet(string alphabet, string text)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var c in alphabet)
        {
            if (!text.Contains(c))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    private void createPuzzle()
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        _ciphertexts[0].text = rndString(alphabet);
        _plaintexts[0].text = rndString(alphabet);
        
        alphabet = getNewAlphabet(alphabet, _ciphertexts[0].text);
        alphabet = getNewAlphabet(alphabet, _plaintexts[0].text);
        _ciphertexts[1].text = rndString(alphabet);
        _plaintexts[1].text = rndString(alphabet);

        string cipher = _ciphertexts[0].text + _ciphertexts[1].text;
        string plain = _plaintexts[0].text + _plaintexts[1].text;
        _ciphertexts[2].text = rndString(cipher);

        Dictionary<char,char> dict = new Dictionary<char,char>();
        int i = 0;
        foreach (var c in cipher)
        {
            dict[c] = plain[i];
            i++;
        }
        foreach (var c in _ciphertexts[2].text)
        {
            _answer += dict[c];
        }
    }

    public void SetPlayerInput(string s)
    {
        _playerInput = s;
    }
    public void SubmitButtonClick()
    {
        _exitButtonPress = false;
        OnPuzzleDone?.Invoke();
    }
    public void ExitButtonClick()
    {
        _exitButtonPress = true;
        OnPuzzleDone?.Invoke();
    }
    public override PuzzleStatus CheckAnswer()
    {
        if (_exitButtonPress)
        {
            return PuzzleStatus.Unsolved;
        }
        if (_answer == _playerInput)
        {
            return PuzzleStatus.Solved;
        }
        return PuzzleStatus.Mistake;
    }

    public override void Setup(PuzzleData p)
    {
        CodePuzzleData data = (CodePuzzleData)p;
        _strLen = data.StrLen;
        createPuzzle();
    }
}
