namespace Infrastructure
{
    public interface IPersistenceService
    {
        void SaveHighScore(string key, int score);
        int LoadHighScore(string key);
    }
}