using System;

namespace Core
{
    public class GameSession
    {
        public GameMode GameMode { get; private set; }
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public float TimeRemaining { get; private set; }
        public bool IsGameOver { get; private set; }
        public int HighScore { get; private set; }

        public event Action<int> OnScoreChanged;
        public event Action<int> OnLivesChanged;
        public event Action<float> OnTimeChanged;
        public event Action OnGameOver;

        private readonly int _initialTimeOrLives;

        public GameSession(GameMode gameMode, int initialValue, int currentHighScore)
        {
            GameMode = gameMode;
            Score = 0;
            IsGameOver = false;
            HighScore = currentHighScore;

            switch (gameMode)
            {
                case GameMode.TimeAttack:
                    TimeRemaining = initialValue;
                    Lives = 1;
                    break;
                case GameMode.Arcade:
                    Lives = initialValue;
                    TimeRemaining = 0;
                    break;
            }
        }

        public void Tick(float deltaTime)
        {
            if (IsGameOver || GameMode != GameMode.TimeAttack) return;

            TimeRemaining -= deltaTime;
            OnTimeChanged?.Invoke(TimeRemaining);

            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                EndGame();
            }
        }

        public void AddScore(int points)
        {
            if (IsGameOver) return;

            Score += points;
            OnScoreChanged?.Invoke(Score);
        }

        public void LoseLife()
        {
            if (IsGameOver) return;

            if (GameMode == GameMode.Arcade)
            {
                Lives--;
                OnLivesChanged?.Invoke(Lives);
            }
            else
            {
                TimeRemaining -= _initialTimeOrLives;
                OnTimeChanged?.Invoke(TimeRemaining);
            }

            if (Lives <= 0)
            {
                Lives = 0;
                EndGame();
            }
        }

        private void EndGame()
        {
            IsGameOver = true;
            
            if (Score > HighScore)
            {
                HighScore = Score;
            }
            
            OnGameOver?.Invoke();
        }
    }
}