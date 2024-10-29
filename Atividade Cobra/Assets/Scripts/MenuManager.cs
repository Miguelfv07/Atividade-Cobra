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
}
