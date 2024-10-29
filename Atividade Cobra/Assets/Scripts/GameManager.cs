using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Snake snake;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;

    private int score = 0;
    private int highScore = 0;

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
        UpdateScore(0);
    }

}
