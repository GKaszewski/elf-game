using System;

namespace Core.Systems
{
    public class LivesSystem : IDisposable
    {
        private int _currentLives;

        public LivesSystem(int initialLives)
        {
            _currentLives = initialLives;
            GameEvents.PresentDropped += OnPresentDropped;
            
            GameEvents.ReportLivesUpdated(_currentLives);
        }

        private void OnPresentDropped()
        {
            if (_currentLives <= 0) return;
            
            _currentLives--;
            GameEvents.ReportLivesUpdated(_currentLives);
            
            if (_currentLives <= 0)
            {
                GameEvents.ReportGameOver();
            }
        }

        public void Dispose()
        {
            GameEvents.PresentDropped -= OnPresentDropped;
        }
    }
}