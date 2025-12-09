using System;

namespace Core
{
    public static class GameEvents
    {
        // Gameplay Events (Inputs to the systems)
        public static event Action<int> PresentCaught; 
        public static event Action PresentDropped; 
        
        // State Changes (Outputs from the systems)
        public static event Action<int> ScoreUpdated;
        public static event Action<int> LivesUpdated;
        public static event Action<float> TimeUpdated;
        public static event Action GameOver;

        // Invokers
        public static void ReportPresentCaught(int value) => PresentCaught?.Invoke(value);
        public static void ReportPresentDropped() => PresentDropped?.Invoke();
        public static void ReportScoreUpdated(int score) => ScoreUpdated?.Invoke(score);
        public static void ReportLivesUpdated(int lives) => LivesUpdated?.Invoke(lives);
        public static void ReportTimeUpdated(float time) => TimeUpdated?.Invoke(time);
        public static void ReportGameOver() => GameOver?.Invoke();

        public static void Clear()
        {
            // Reset events when scene reloads to prevent memory leaks
            PresentCaught = null;
            PresentDropped = null;
            ScoreUpdated = null;
            LivesUpdated = null;
            TimeUpdated = null;
            GameOver = null;
        }
    }
}