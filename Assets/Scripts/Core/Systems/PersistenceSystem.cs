using System;
using Infrastructure;

namespace Core.Systems
{
    public class PersistenceSystem : IDisposable
    {
        private readonly IPersistenceService _service;
        private readonly string _saveKey;
        private int _currentRunScore;
        
        public PersistenceSystem(IPersistenceService service, string saveKey)
        {
            _service = service;
            _saveKey = saveKey;
            
            GameEvents.ScoreUpdated += OnScoreUpdated;
            GameEvents.GameOver += OnGameOver;
        }

        public int GetHighScore()
        {
            return _service.LoadHighScore(_saveKey);
        }

        private void OnGameOver()
        {
            var existingHighScore = _service.LoadHighScore(_saveKey);
            if (_currentRunScore > existingHighScore)
            {
                _service.SaveHighScore(_saveKey, _currentRunScore);
            }
        }

        private void OnScoreUpdated(int newScore)
        {
            _currentRunScore = newScore;
        }

        public void Dispose()
        {
            GameEvents.ScoreUpdated -= OnScoreUpdated;
            GameEvents.GameOver -= OnGameOver;
        }
    }
}