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

    int mediumScore = 0;


    void Start()
    {
        UpdateScoreUI();
        UpdateHighScoreUI();
        mediumScore = PlayerPrefs.GetInt("HighScore", 0);
        CheckHighScore();
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
        int hihghestScore = 0;
        if(highScore > mediumScore)
        {
            hihghestScore = highScore;
        }
        else
        {
            hihghestScore = mediumScore;
        }
        highScoreText.text = "High Score: " + hihghestScore;
    }

    public void CheckHighScore()
    {
        // PlayerPrefs.SetInt("HighScore", mediumScore);

        if (score >= highScore)
        {
            if(mediumScore <= score)
            {   highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
                UpdateHighScoreUI();
            }

        }


    }
}
