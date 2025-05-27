using System;
using UnityEngine;

namespace Puzzle
{
    public abstract class Puzzle : MonoBehaviour  
    {
        protected callback OnPuzzleDone;
        [SerializeField]
        private Camera puzzleCam;
        protected bool IsActive;
        private void SetupVisuals()
        {
            puzzleCam.gameObject.SetActive(true);
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(false);
        }
        private void CleanVisuals()
        {
            puzzleCam.gameObject.SetActive(false);
            GameManager.Instance.Player.transform.GetChild(0).gameObject.SetActive(true);
        }
        public abstract bool CanSolve();
        private void SetCallback(callback c) {
            OnPuzzleDone += c;
        }
        public void Setup(callback c)
        {
            SetCallback(c);
            SetupVisuals();
            SetupLogic();
            IsActive = true;
        }
        protected void Clean()
        {
            CleanVisuals();
            IsActive = false;
        }
        protected abstract void SetupLogic();
        public abstract PuzzleStatus CheckAnswer();
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
