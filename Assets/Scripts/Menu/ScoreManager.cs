using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public int highScore = 0;
    public Text scoreText;
    public Text highScoreText;

    private void Start()
    {
        LoadHighScore();
        UpdateHighScoreText();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
            UpdateHighScoreText();
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
