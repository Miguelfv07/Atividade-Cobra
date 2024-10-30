using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Classe MenuManager: Gerencia a interface do menu inicial e os parâmetros para iniciar o jogo.
public class MenuManager : MonoBehaviour
{
    // Instância única da classe, permitindo fácil acesso de outros scripts.
    public static MenuManager Instance;

    // Referência ao objeto Snake, para definir a área e a velocidade do jogo.
    public Snake snake;

    // Campos de entrada de texto para o usuário definir largura, altura e velocidade do jogo.
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public TMP_InputField speedInput;

    // Referências aos elementos da interface, como o botão de início e o painel de configurações.
    public GameObject startButton;
    public GameObject panel;


    // Método Awake: Chamado quando o objeto é criado. Define esta classe como uma instância singleton.
    private void Awake()
    {
        Instance = this;
    }


    // Método StartGame: Inicia o jogo se os valores inseridos pelo usuário forem válidos.
    public void StartGame()
    {
        float width, height, speed;

        if (TryGetInputValues(out width, out height, out speed))
        {

            snake.SetGameArea(width, height);
            snake.SetSpeed(speed);


            panel.SetActive(false);
            widthInput.gameObject.SetActive(false);
            heightInput.gameObject.SetActive(false);
            speedInput.gameObject.SetActive(false);
            startButton.SetActive(false);
        }
        else
        {
            Debug.LogError(" insira valores válidos.");
        }
    }


    // Método TryGetInputValues: Tenta converter os valores inseridos pelo usuário em floats.
    // Retorna verdadeiro se todos os valores forem válidos, e falso caso contrário.
    private bool TryGetInputValues(out float width, out float height, out float speed)
    {
        width = height = speed = 0;


        string[] inputs = { widthInput.text, heightInput.text, speedInput.text };
        float[] values = { width, height, speed };

        for (int i = 0; i < inputs.Length; i++)
        {
            if (!float.TryParse(inputs[i], out values[i]))
            {
                return false;
            }
        }


        width = values[0];
        height = values[1];
        speed = values[2];

        return true;
    }
}
