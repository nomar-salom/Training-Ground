using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    void Start()
    {
        UpdateScoreUI();
        UpdateHighScoreUI();
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        return;
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHighScoreUI()
    {
        // highScoreText.text = "High Score: " + highScore;
    }

    public void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreUI();
        }
    }
}
