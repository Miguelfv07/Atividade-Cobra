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

    void SpawnFood()
    {

        float width = snake.GetWidth();
        float height = snake.GetHeight();

        Vector2 randomPosition;


        do
        {
            float x = Random.Range(-width / 2 + snake.cellSize / 2, width / 2 - snake.cellSize / 2);
            float y = Random.Range(-height / 2 + snake.cellSize / 2, height / 2 - snake.cellSize / 2);
            randomPosition = new Vector2(x, y);
        }
        while (IsPositionOccupied(randomPosition));


        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
        }


        currentFood = Instantiate(foodPrefab, randomPosition, Quaternion.identity).transform;
    }

    bool IsPositionOccupied(Vector2 position)
    {

        if ((Vector2)snake.transform.position == position)
        {
            return true;
        }


        foreach (Transform segment in snake.body)
        {
            if ((Vector2)segment.position == position)
            {
                return true;
            }
        }
        return false;
    }
}
