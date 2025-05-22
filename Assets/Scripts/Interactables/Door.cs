using NaughtyAttributes;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionName = "Press E to Open Door";
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public string InteractionName => _interactionName;
    private Animator _animator;
    private bool _isOpen = false;
    private AudioSource _audioSource;
    public bool HasPuzzle;
    [ShowIf("HasPuzzle")]
    [SerializeField] private PuzzleData _puzzleData;
    [ShowIf("HasPuzzle")]
    [SerializeField] private GameObject _puzzle;
    private Puzzle _puzzleManager;
    private bool _isPuzzleSolved;
    private bool _isLocked;
    private GameObject _puzzleInstance;
    private void Start()
    {
        _animator = GetComponent<Animator>();  
        _audioSource = GetComponent<AudioSource>();
    }
    private void ToggleDoor()
    {
        if (_isOpen)
        {
            _animator.Play("door_close", 0, 0.0f);
            _audioSource.PlayOneShot(doorClose);
            _isOpen = false;
            _interactionName = "Press E to Open Door";
        }
        else
        {
            _animator.Play("door_open", 0, 0.0f);
            _audioSource.PlayOneShot(doorOpen);
            _isOpen = true;
            _interactionName = "Press E to Close Door";
        }
    }
    public void Interact(PlayerInteraction pi)
    {
        if (_isLocked)
        {
            return;
        }
        if (!HasPuzzle || (HasPuzzle && _isPuzzleSolved))
        {
            ToggleDoor();
        }
        else
        {
            pi.GetComponent<PlayerState>().PauseGame();
            if (_puzzleInstance == null)
            {
                _puzzleInstance = Instantiate(_puzzle);
                _puzzleManager = _puzzleInstance.GetComponent<Puzzle>();
                _puzzleManager.SetCallback(OnPuzzleDone);
                _puzzleManager.Setup(_puzzleData);
            }
            else
            {
                _puzzleInstance.SetActive(true);
            }
        }
    }

    private void OnPuzzleDone()
    {
        if (_puzzleManager.CheckAnswer() == PuzzleStatus.Solved)
        {
            _isPuzzleSolved = true;
            _puzzleInstance.SetActive(false);
            GameManager.GetPlayerState().UnpauseGame();
            GameManager.GetPlayerState().PuzzleSolve();
        }
        else if (_puzzleManager.CheckAnswer() == PuzzleStatus.Mistake)
        {
            int chances = _puzzleManager.ReduceChances();
            if (chances <= 0)
            {
                _isLocked = true;
                _interactionName = "The Door Is Locked";
                Destroy(_puzzleInstance);
            }
            _puzzleInstance.SetActive(false);
            GameManager.GetPlayerState().UnpauseGame();
            GameManager.GetPlayerState().PuzzleMistake();
        }
        else
        {
            _puzzleInstance.SetActive(false);
            GameManager.GetPlayerState().UnpauseGame();
        }
    }
}
