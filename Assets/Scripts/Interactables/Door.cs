using NaughtyAttributes;
using Player;
using Puzzle;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactionName = "Press E to Open Door";
        public AudioClip doorOpen;
        public AudioClip doorClose;
        public string InteractionName => interactionName;
        private Animator      _animator;
        private bool          _isOpen = false;
        private AudioSource   _audioSource;
        private bool          _hasPuzzle;
        private Puzzle.Puzzle _puzzle;
        private GameObject    _puzzleInstance;
        private bool          _isPuzzleSolved;
        private bool          _isLocked;
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
                interactionName = "Press E to Open Door";
            }
            else
            {
                _animator.Play("door_open", 0, 0.0f);
                _audioSource.PlayOneShot(doorOpen);
                _isOpen = true;
                interactionName = "Press E to Close Door";
            }
        }
        public void Interact(PlayerInteraction pi)
        {
            if (_isLocked)
            {
                return;
            }
            if (!_hasPuzzle || (_isPuzzleSolved))
            {
                ToggleDoor();
            }
            else
            {
                if (_puzzle.CanSolve())
                {
                    pi.GetComponent<PlayerState>().PauseGame();
                    _puzzle.Setup();
                }
            }
        }
        private void OnPuzzleDone()
        {
            if (_puzzle.CheckAnswer() == PuzzleStatus.Solved)
            {
                _isPuzzleSolved = true;
                GameManager.GetPlayerState().PuzzleSolve();
            }
            else if (_puzzle.CheckAnswer() == PuzzleStatus.Mistake)
            {
                //@TODO: Solve limit for puzzle implementation 
                GameManager.GetPlayerState().PuzzleMistake();
            }
            GameManager.GetPlayerState().UnpauseGame();
        }

        public void AddPuzzle(GameObject puzzlePrefab)
        {
            _hasPuzzle = true;
            _puzzleInstance = Instantiate(puzzlePrefab);
            _puzzleInstance.transform.position = new Vector3(-200, 0, 0);
            _puzzle = _puzzleInstance.GetComponent<Puzzle.Puzzle>();
            _puzzle.SetCallback(OnPuzzleDone);
        }
    }
}
