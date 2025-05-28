using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public abstract class Puzzle : MonoBehaviour  
    {
        protected callback OnPuzzleDone;
        [SerializeField] private Camera puzzleCam;
        [SerializeField] private List<GameObject> puzzleVisuals;
        protected bool IsActive;
        private void SetupVisuals()
        {
            puzzleCam.gameObject.SetActive(true);
            foreach (var pv in puzzleVisuals)
            {
                pv.SetActive(true);
            }
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(false);
        }
        private void CleanVisuals()
        {
            puzzleCam.gameObject.SetActive(false);
            foreach (var pv in puzzleVisuals)
            {
                pv.SetActive(false);
            }
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(true);
        }
        public abstract bool CanSolve();
        public void SetCallback(callback c) {
            OnPuzzleDone += c;
        }
        public void Setup()
        {
            SetupVisuals();
            SetupLogic();
            IsActive = true;
        }
        private void Clean()
        {
            CleanVisuals();
            IsActive = false;
        }
        protected abstract void SetupLogic();
        public PuzzleStatus CheckAnswer()
        {
            var status = CheckAnswerLogic();
            Clean();
            return status;
        }
        protected abstract PuzzleStatus CheckAnswerLogic();
        protected abstract void UpdateLogic();
        private void Update()
        {
            if (IsActive)
            {
                UpdateLogic();
            }
        }
    }
}
