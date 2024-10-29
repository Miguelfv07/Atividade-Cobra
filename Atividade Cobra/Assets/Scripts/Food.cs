using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Transform foodPrefab;
    public Snake snake;
    private Transform currentFood;

    private void Start()
    {
        SpawnFood();
    }

    void Update()
    {

        Vector2 index = currentFood.position / snake.cellSize;
        if (Mathf.Abs(index.x - snake.cellIndex.x) < 0.5f && Mathf.Abs(index.y - snake.cellIndex.y) < 0.5f)
        {

            SpawnFood();
            snake.GrowBody();
        }
    }
}
