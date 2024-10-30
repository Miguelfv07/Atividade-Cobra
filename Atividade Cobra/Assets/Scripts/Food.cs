using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe Food: Gerencia o comportamento dos alimentos no jogo, incluindo geração aleatória e detecção de consumo pela cobra.
public class Food : MonoBehaviour
{
    // Prefab do alimento que será instanciado na cena.
    public Transform foodPrefab;
    // Referência à cobra, para acessar informações como posição e tamanho das células.
    public Snake snake;
    // Guarda a referência do alimento atual na cena.
    private Transform currentFood;


    // Método Start: Executado ao iniciar a cena. Chama o método para gerar um novo alimento.
    private void Start()
    {
        SpawnFood();
    }

    // Método Update: Verifica a cada frame se a cobra comeu o alimento.
    void Update()
    {

        Vector2 index = currentFood.position / snake.cellSize;
        if (Mathf.Abs(index.x - snake.cellIndex.x) < 0.5f && Mathf.Abs(index.y - snake.cellIndex.y) < 0.5f)
        {
            
            GameManager.instance.UpdateScore(1);
            SpawnFood();
            snake.GrowBody();
        }
    }


    // Método SpawnFood: Gera um novo alimento em uma posição aleatória da área de jogo.
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


    // Método IsPositionOccupied: Verifica se a posição gerada está ocupada pela cobra ou seu corpo.
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
