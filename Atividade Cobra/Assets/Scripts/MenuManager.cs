using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Snake snake;
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public TMP_InputField speedInput;
    public GameObject startButton;
    public GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

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
