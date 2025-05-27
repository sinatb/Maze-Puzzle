using UnityEngine;

namespace Puzzle.Lockpickpuzzle
{
    public class LockPickPuzzle : InventoryPuzzle
    {
        private float   _sweetSpot;
        private float   _acceptanceRange;
        private float   _errorMargin;
        [SerializeField] private float   _lockPickHealth;
        private bool    _solvedCorrect;
        [SerializeField] private float      finalAngle;
        [SerializeField] private float      rotationRate;
        [SerializeField] private GameObject lockPick;
        [SerializeField] private GameObject lockRotationPivot;
        
        protected override void SetupLogic()
        {
            _sweetSpot = Random.Range(0.0f, 360.0f);
            _errorMargin = Random.Range(20.0f, 45.0f);
            _acceptanceRange = Random.Range(3.0f, 10.0f);
            _solvedCorrect = false;
            _lockPickHealth = 100.0f;
        }
        public override PuzzleStatus CheckAnswer()
        {
            Clean();
            return _solvedCorrect ? PuzzleStatus.Solved : PuzzleStatus.Unsolved;
        }

        protected override void UpdateLogic()
        {
            var x = Input.mousePosition.x - Screen.width / 2.0f;
            var y = Input.mousePosition.y - Screen.height / 2.0f;
            var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360.0f;
            }
            var lockAngle = Vector3.Angle(lockRotationPivot.transform.right, Vector3.right);
            lockPick.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            if (_lockPickHealth <= 0.0f)
            {
                _solvedCorrect = false;
                OnPuzzleDone.Invoke();
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Mathf.Abs(angle - _sweetSpot) < _acceptanceRange)
                {
                    if (lockAngle < finalAngle)
                    {
                        lockRotationPivot.transform.Rotate( 0,
                                                            0,
                                                            -rotationRate * Time.deltaTime);
                    }
                    else
                    {
                        _solvedCorrect = true;
                        OnPuzzleDone.Invoke();
                    }
                } 
                else if (Mathf.Abs(angle - _sweetSpot) < _errorMargin)
                {
                    var dynamicRotation = (1 - Mathf.Abs(_sweetSpot - angle) / _errorMargin) * finalAngle;  
                    if (lockAngle < dynamicRotation)
                    {
                        lockRotationPivot.transform.Rotate( 0,
                            0,
                            -rotationRate * Time.deltaTime);
                    }
                    _lockPickHealth -= 15.0f * Time.deltaTime;
                }
                else
                {
                    _lockPickHealth -= 10.0f * Time.deltaTime;
                }
            }
            else
            {
                if (lockAngle > 3f)
                {
                    lockRotationPivot.transform.Rotate( 0,
                                                        0,
                                                        rotationRate * 2 * Time.deltaTime);
                }
                else
                {
                    lockRotationPivot.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }
        }
    }
}