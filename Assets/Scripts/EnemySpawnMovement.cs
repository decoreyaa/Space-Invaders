using UnityEngine;


public class EnemySpawnMovement : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float speedMultiplier = 2f;
    private float startingCount;
    private float currentCount;
    private float aliveRatio;

    void Start()
    {

        //Gets the number of enemies
        startingCount = transform.childCount;
    }
    // Update is called once per frame
    void Update()
    {
        if (startingCount <= 0f)
        {
            return;
        }

        //Gets current number of enemies
        currentCount = transform.childCount;
        //Gets ratio of enemies
        aliveRatio = currentCount / startingCount;

        float currentSpeed = horizontalSpeed + (horizontalSpeed * (1f - aliveRatio) * speedMultiplier);

        // each enemy moves and bounces itself, so the formation only hands down the speed
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy enemy = transform.GetChild(i).GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.horizontalSpeed = currentSpeed;
            }
        }
    }
}
