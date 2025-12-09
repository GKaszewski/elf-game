using System;

namespace Core.Systems
{
    public class ScoreSystem : IDisposable
    {
        private int _currentScore;

        public ScoreSystem()
        {
            _currentScore = 0;
            GameEvents.PresentCaught += OnPresentCaught;
        }

        private void OnPresentCaught(int value)
        {
            _currentScore += value;
            GameEvents.ReportScoreUpdated(_currentScore);
        }

        public void Dispose()
        {
            GameEvents.PresentCaught -= OnPresentCaught;
        }
    }
}