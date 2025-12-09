using UnityEngine;

namespace Infrastructure
{
    public class PlayerPrefsPersistence : IPersistenceService
    {
        public void SaveHighScore(string key, int score)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }

        public int LoadHighScore(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }
    }
}