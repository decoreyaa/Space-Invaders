using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGridSpawner : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float baseHorizontalSpeed = 1f;
    public float baseShootCooldown = 5f;
    public float minShootCooldown = 0.5f;
    public Enemy enemyPrefab;
    public int rows = 4;
    public int colums = 6;
    public float horizontalSpacing = 1.5f;
    public float verticalSpacing = 1f;
    public Vector2 startPosition = new Vector2(5f, 3f);
    public float spawnOffset = 2f;
    private bool hasSpawned = false;
    public int enemyCount = 12;
    private int spawnedSoFar = 0;
    private static readonly System.Random _rng = new System.Random();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnWave(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEnemyCount()
    {
        return transform.childCount;
    }

    public bool HasSpawned()
    {
        return hasSpawned;
    }

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        
        // Loop backwards through the collection
        for (int i = n - 1; i > 0; i--)
        {
            // Pick a random index from 0 to i (inclusive)
            int k = _rng.Next(i + 1);
            
            // Swap elements using C# tuple deconstruction syntax
            (list[k], list[i]) = (list[i], list[k]);
        }
    }

    public void SpawnWave(int wave)
    {
        hasSpawned = false;

        float startX = ScreenBounds.Right + spawnOffset;
        List<Vector2> allPositions = new List<Vector2>();
        // loop through rows and columns, instantiate one enemy per cell
        for (int row = 0; row < rows; row++)
        {
            for (int colum = 0; colum < colums; colum++)
            {
                // start position plus however many spacings you've moved
                float x = startX + (colum * horizontalSpacing);
                //subtracted because increasing row should move down the scrteen
                float y = startPosition.y - (row * verticalSpacing);

                Vector2 spawnPos = new Vector2(x, y);                
                allPositions.Add(spawnPos);
            }
        }
        Shuffle(allPositions);

        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, allPositions[i], Quaternion.identity, transform);
            float t = wave / (wave + 10f);
            enemy.horizontalSpeed = Mathf.Lerp(baseHorizontalSpeed, maxSpeed, t);
            enemy.shootCooldown = Mathf.Max(minShootCooldown, baseShootCooldown * (1f - wave * 0.1f));
        }
        hasSpawned = true;
    }
}
