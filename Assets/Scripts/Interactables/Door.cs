using NaughtyAttributes;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionName = "Open Door";
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
            _interactionName = "Open Door";
        }
        else
        {
            _animator.Play("door_open", 0, 0.0f);
            _audioSource.PlayOneShot(doorOpen);
            _isOpen = true;
            _interactionName = "Close Door";
        }
    }
    public void Interact(PlayerInteraction pi)
    {
        if (!HasPuzzle || (HasPuzzle && _isPuzzleSolved))
        {
            ToggleDoor();
        }
        else
        {
            pi.GetComponent<PlayerState>().PauseGame();
            _puzzleInstance = Instantiate(_puzzle);
            _puzzleManager = _puzzleInstance.GetComponent<Puzzle>();
            _puzzleManager.SetCallback(OnPuzzleDone);
            _puzzleManager.Setup(_puzzleData);
        }
    }

    private void OnPuzzleDone()
    {
        if (_puzzleManager.CheckAnswer() == PuzzleStatus.Solved)
        {
            _isPuzzleSolved = true;
            Destroy(_puzzleInstance);
            GameManager.Instance.PlayerState.UnpauseGame();
            GameManager.Instance.PlayerState.PuzzleSolve();
        }
        else if (_puzzleManager.CheckAnswer() == PuzzleStatus.Mistake)
        {
            Destroy(_puzzleInstance);
            GameManager.Instance.PlayerState.UnpauseGame();
            GameManager.Instance.PlayerState.PuzzleMistake();
        }
        else
        {
            Destroy(_puzzleInstance);
            GameManager.Instance.PlayerState.UnpauseGame();
        }
    }
}
