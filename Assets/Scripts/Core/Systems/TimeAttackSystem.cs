using System;

namespace Core.Systems
{
    public class TimeAttackSystem : IDisposable
    {
        private float _timeRemaining;
        private readonly float _penaltyPerDrop;
        
        public TimeAttackSystem(float initialTime, float penaltyPerDrop = 5f)
        {
            _timeRemaining = initialTime;
            _penaltyPerDrop = penaltyPerDrop;
            
            GameEvents.ReportTimeUpdated(_timeRemaining);
            
            GameEvents.PresentDropped += OnPresentDropped;
        }

        public void Tick(float deltaTime)
        {
            if (_timeRemaining <= 0) return;
            
            _timeRemaining -= deltaTime;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                GameEvents.ReportTimeUpdated(0f);
                GameEvents.ReportGameOver();
            }
            else
            {
                GameEvents.ReportTimeUpdated(_timeRemaining);
            }
        }

        private void OnPresentDropped()
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= _penaltyPerDrop;
            }
        }

        public void Dispose()
        {
            GameEvents.PresentDropped -= OnPresentDropped;
        }
    }
}