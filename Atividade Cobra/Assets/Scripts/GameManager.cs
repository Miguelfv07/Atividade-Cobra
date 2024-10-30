using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Classe GameManager: Controla a lógica central do jogo, como pontuação, estado de game over e reinício.
public class GameManager : MonoBehaviour
{
    // Variável estática para garantir uma única instância do GameManager (padrão Singleton).
    public static GameManager instance;
    // Referência para a classe Snake, usada para reiniciar a cobra.
    public Snake snake;

    // Referências aos componentes de UI para exibir pontuação, recorde e mensagem de game over.
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;

    // Variáveis para armazenar a pontuação atual e a pontuação mais alta.
    public int score = 0;
    private int highScore = 0;


    // Método chamado quando o objeto é criado na cena. Define a instância do GameManager.
    private void Awake()
    {
        instance = this;
    }

    // Método chamado no início do jogo. Configura a UI inicial e reseta o placar.
    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
        UpdateScore(0);
    }


    // Método para atualizar a pontuação e verificar se é um novo recorde.
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


    // Método chamado quando o jogo acaba. Exibe a mensagem de game over.
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }


    // Método para reiniciar o jogo, resetando pontuação e status.
    public void Restart()
    {
        score = 0;
        UpdateScore(0);
        snake.Restart();
        gameOverText.gameObject.SetActive(false);
    }

}
