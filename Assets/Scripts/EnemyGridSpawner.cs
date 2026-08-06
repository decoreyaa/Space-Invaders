using UnityEngine;

public class EnemyGridSpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public int rows = 4;
    public int colums = 6;
    public float horizontalSpacing = 1.5f;
    public float verticalSpacing = 1f;
    public Vector2 startPosition = new Vector2(5f, 3f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // loop through rows and columns, instantiate one enemy per cell
        for (int row = 0; row < rows; row++)
        {
            for (int colum = 0; colum < colums; colum++)
            {
                // start position plus however many spacings you've moved
                float x = startPosition.x + (colum * horizontalSpacing);
                //subtracted because increasing row should move down the scrteen
                float y = startPosition.y -(colum * verticalSpacing);
                Vector2 spawnPos = new Vector2(x, y);

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
