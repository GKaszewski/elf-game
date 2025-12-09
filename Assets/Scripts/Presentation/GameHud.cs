using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presentation
{
    public class GameHud : MonoBehaviour
    {
        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesOrTimeText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        [Header("Panels")]
        [SerializeField] private GameObject gameOverPanel;

        private void Awake()
        {
            if (gameOverPanel) gameOverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.ScoreUpdated += UpdateScore;
            GameEvents.LivesUpdated += UpdateLives;
            GameEvents.TimeUpdated += UpdateTime;
            GameEvents.GameOver += ShowGameOver;
        }

        private void OnDisable()
        {
            GameEvents.ScoreUpdated -= UpdateScore;
            GameEvents.LivesUpdated -= UpdateLives;
            GameEvents.TimeUpdated -= UpdateTime;
            GameEvents.GameOver -= ShowGameOver;
        }
        
        private void UpdateScore(int score)
        {
            if(scoreText) scoreText.text = $"Score: {score}";
        }

        private void UpdateLives(int lives)
        {
            if(livesOrTimeText) livesOrTimeText.text = $"Lives: {lives}";
        }

        private void UpdateTime(float time)
        {
            if(livesOrTimeText) livesOrTimeText.text = $"Time: {time:F0}"; 
            
            if (time <= 10f) livesOrTimeText.color = Color.red;
            else livesOrTimeText.color = Color.white;
        }

        private void ShowGameOver()
        {
            if(gameOverPanel) gameOverPanel.SetActive(true);
        }


        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}