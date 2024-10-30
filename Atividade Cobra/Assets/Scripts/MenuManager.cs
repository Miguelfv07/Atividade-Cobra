using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Classe MenuManager: Gerencia a interface do menu inicial e os par�metros para iniciar o jogo.
public class MenuManager : MonoBehaviour
{
    // Inst�ncia �nica da classe, permitindo f�cil acesso de outros scripts.
    public static MenuManager Instance;

    // Refer�ncia ao objeto Snake, para definir a �rea e a velocidade do jogo.
    public Snake snake;

    // Campos de entrada de texto para o usu�rio definir largura, altura e velocidade do jogo.
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public TMP_InputField speedInput;

    // Refer�ncias aos elementos da interface, como o bot�o de in�cio e o painel de configura��es.
    public GameObject startButton;
    public GameObject panel;


    // M�todo Awake: Chamado quando o objeto � criado. Define esta classe como uma inst�ncia singleton.
    private void Awake()
    {
        Instance = this;
    }


    // M�todo StartGame: Inicia o jogo se os valores inseridos pelo usu�rio forem v�lidos.
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
            Debug.LogError(" insira valores v�lidos.");
        }
    }


    // M�todo TryGetInputValues: Tenta converter os valores inseridos pelo usu�rio em floats.
    // Retorna verdadeiro se todos os valores forem v�lidos, e falso caso contr�rio.
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
