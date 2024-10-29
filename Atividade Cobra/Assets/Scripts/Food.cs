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
}
