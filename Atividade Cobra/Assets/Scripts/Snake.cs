using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform bodyPrefab;
    public Transform wallPrefab;
    public GameManager gameManager;
    private Vector2 direction;
    private float changeCellTime = 0;
    public List<Transform> body = new List<Transform>();
    public float speed = 10.0f;
    public float cellSize = 0.3f;
    public Vector2 cellIndex = Vector2.zero;
    private float gameWidth;
    private float gameHeight;
    private bool gameOver = false;
    private int[,] wallGrid;

    private void Start()
    {
        direction = Vector2.up;
    }

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
}
