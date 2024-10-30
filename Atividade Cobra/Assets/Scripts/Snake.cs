using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe Snake: Gerencia o comportamento da cobra, incluindo movimenta��o, crescimento, colis�es e rein�cio.
public class Snake : MonoBehaviour
{
    // Refer�ncias para os prefabs do corpo e das paredes da cobra.
    public Transform bodyPrefab;
    public Transform wallPrefab;

    // Refer�ncia ao GameManager, para comunica��o entre a cobra e o sistema de pontua��o.
    public GameManager gameManager;
    // Dire��o atual de movimento da cobra.
    private Vector2 direction;
    // Controla o intervalo entre movimentos da cobra.
    private float changeCellTime = 0;
    // Lista que armazena as partes do corpo da cobra.
    public List<Transform> body = new List<Transform>();
    // Velocidade da cobra.
    public float speed = 10.0f;
    // Tamanho de cada c�lula no grid
    public float cellSize = 0.3f;
    // �ndice da c�lula onde a cabe�a da cobra est� atualmente.
    public Vector2 cellIndex = Vector2.zero;
    // Largura e altura da �rea de jogo.
    private float gameWidth;
    private float gameHeight;
    // Indica se o jogo est� no estado de "game over".
    private bool gameOver = false;
    // Grid que define as posi��es das paredes.
    private int[,] wallGrid;


    // M�todo Start: Inicializa a dire��o da cobra apontando para cima.
    private void Start()
    {
        direction = Vector2.up;
    }


    // M�todo Update: Controla as atualiza��es do jogo a cada frame.
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) gameManager.Restart();
            return;
        }

        ChangeDirection();
        Move();
        CheckBodyCollisions();
    }


    // M�todo CheckWallWrapAround: Faz a cabe�a da cobra aparecer do outro lado quando sai dos limites do jogo.
    void CheckWallWrapAround()
    {
        if (transform.position.x > gameWidth / 2)
            transform.position = new Vector3(-gameWidth / 2 + 0.01f, transform.position.y, transform.position.z);
        else if (transform.position.x < -gameWidth / 2)
            transform.position = new Vector3(gameWidth / 2 - 0.01f, transform.position.y, transform.position.z);

        if (transform.position.y > gameHeight / 2)
            transform.position = new Vector3(transform.position.x, -gameHeight / 2 + 0.01f, transform.position.z);
        else if (transform.position.y < -gameHeight / 2)
            transform.position = new Vector3(transform.position.x, gameHeight / 2 - 0.01f, transform.position.z);
    }


    // M�todo CheckBodyCollisions: Verifica se a cabe�a colidiu com alguma parte do corpo.
    void CheckBodyCollisions()
    {
        if (body.Count < 3) return;
        for (int i = 0; i < body.Count; i++)
        {
            Vector2 index = body[i].position / cellSize;
            if (Mathf.Abs(index.x - cellIndex.x) < 0.00001f && Mathf.Abs(index.y - cellIndex.y) < 0.00001f)
            {
                GameOver();
                break;
            }
        }
    }


    // M�todo ChangeDirection: Altera a dire��o da cobra com base nas teclas pressionadas.
    void ChangeDirection()
    {
        Vector2 newdirection = Vector2.zero;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.y == -1) newdirection = Vector2.down;
        else if (input.y == 1) newdirection = Vector2.up;
        else if (input.x == -1) newdirection = Vector2.left;
        else if (input.x == 1) newdirection = Vector2.right;
        if (newdirection + newdirection != Vector2.zero && newdirection != Vector2.zero)
        {
            direction = newdirection;
        }
    }


    // M�todo GrowBody: Adiciona uma nova parte ao corpo da cobra.
    public void GrowBody()
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
            position = body[body.Count - 1].position;

        body.Add(Instantiate(bodyPrefab, position, Quaternion.identity).transform);
       
        
    }


    // M�todo Restart: Reinicia o jogo, removendo o corpo e redefinindo a posi��o da cobra.
    public void Restart()
    {
        gameOver = false;


        for (int i = 0; i < body.Count; ++i)
        {
            Destroy(body[i].gameObject);
        }
        body.Clear();
        GrowBody();


        transform.position = Vector3.zero;
    }

    // M�todo Move: Move a cobra em intervalos de tempo controlados.
    void Move()
    {
        if (Time.time > changeCellTime)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].position = body[i - 1].position;
            }
            if (body.Count > 0) body[0].position = (Vector2)transform.position;

            transform.position += (Vector3)direction * cellSize;

            changeCellTime = Time.time + 1 / speed;
            cellIndex = transform.position / cellSize;

            CheckWallWrapAround();
        }
    }

    // M�todo GameOver: Define o estado de game over e chama o m�todo correspondente no GameManager.
    void GameOver()
    {
        gameOver = true;
        gameManager.GameOver();


    }


    // M�todo GetWidth: Retorna a largura da �rea de jogo.
    public float GetWidth()
    {
        return gameWidth;
    }

    // M�todo GetHeight: Retorna a altura da �rea de jogo.
    public float GetHeight()
    {
        return gameHeight;
    }

    // M�todo SetSpeed: Altera a velocidade da cobra.
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    // M�todo SetGameArea: Define a �rea de jogo e cria as paredes.
    public void SetGameArea(float width, float height)
    {

        CreateWalls(width, height);
    }


    // M�todo CreateWalls: Cria paredes em torno da �rea de jogo.
    void CreateWalls(float width, float height)
    {

        gameWidth = width;
        gameHeight = height;


        int cellX = Mathf.FloorToInt(width / cellSize / 2);
        int cellY = Mathf.FloorToInt(height / cellSize / 2);


        wallGrid = new int[cellX * 2 + 1, cellY * 2 + 1];


        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(wall);
        }


        for (int i = -cellX; i <= cellX; i++)
        {
            Vector2 top = new Vector2(i * cellSize, cellY * cellSize);
            Vector2 bottom = new Vector2(i * cellSize, -cellY * cellSize);

            Instantiate(wallPrefab, top, Quaternion.identity).tag = "Wall";
            Instantiate(wallPrefab, bottom, Quaternion.identity).tag = "Wall";


            wallGrid[i + cellX, cellY] = 1;
            wallGrid[i + cellX, 0] = 1;
        }


        for (int i = -cellY; i <= cellY; i++)
        {
            Vector2 left = new Vector2(-cellX * cellSize, i * cellSize);
            Vector2 right = new Vector2(cellX * cellSize, i * cellSize);

            Instantiate(wallPrefab, left, Quaternion.identity).tag = "Wall";
            Instantiate(wallPrefab, right, Quaternion.identity).tag = "Wall";


            wallGrid[0, i + cellY] = 1;
            wallGrid[cellX * 2, i + cellY] = 1;
        }
    }




}
