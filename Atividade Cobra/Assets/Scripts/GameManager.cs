using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Classe GameManager: Controla a l�gica central do jogo, como pontua��o, estado de game over e rein�cio.
public class GameManager : MonoBehaviour
{
    // Vari�vel est�tica para garantir uma �nica inst�ncia do GameManager (padr�o Singleton).
    public static GameManager instance;
    // Refer�ncia para a classe Snake, usada para reiniciar a cobra.
    public Snake snake;

    // Refer�ncias aos componentes de UI para exibir pontua��o, recorde e mensagem de game over.
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;

    // Vari�veis para armazenar a pontua��o atual e a pontua��o mais alta.
    public int score = 0;
    private int highScore = 0;


    // M�todo chamado quando o objeto � criado na cena. Define a inst�ncia do GameManager.
    private void Awake()
    {
        instance = this;
    }

    // M�todo chamado no in�cio do jogo. Configura a UI inicial e reseta o placar.
    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
        UpdateScore(0);
    }


    // M�todo para atualizar a pontua��o e verificar se � um novo recorde.
    public void UpdateScore(int points)
    {
        score += points;
        ScoreText.text = "SCORE: " + score.ToString();
        if (score > highScore)
        {
            highScore = score;
            HighScoreText.text = "HIGH SCORE: " + highScore.ToString();
        }
    }


    // M�todo chamado quando o jogo acaba. Exibe a mensagem de game over.
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }


    // M�todo para reiniciar o jogo, resetando pontua��o e status.
    public void Restart()
    {
        score = 0;
        UpdateScore(0);
        snake.Restart();
        gameOverText.gameObject.SetActive(false);
    }

}
