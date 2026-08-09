using UnityEngine;


public class EnemySpawnMovement : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float verticalSpeed = 0.5f;
    private float verticalDirection = 1f;
    private float startingCount;
    private float currentCount;
    private float aliveRatio;
    public float shootCooldown;

    void Start()
    {

        //Gets the number of enemies
        startingCount = transform.childCount;
    }
    // Update is called once per frame
    void Update()
    {
        //Gets current number of enemies
        currentCount = transform.childCount;
        //Gets ratio of enemies 
        aliveRatio = currentCount / startingCount;
        float multiplier = 2;

        float currentSpeed = horizontalSpeed + (horizontalSpeed * (1f - aliveRatio) * multiplier);
    

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        BoundsCheck();
        Vector2 vertDir = Vector2.up * verticalDirection * verticalSpeed * Time.deltaTime;
        transform.Translate(vertDir);
    }
    void BoundsCheck()
    {
        float topY = float.MinValue;
        float bottomY = float.MaxValue;

        for (int i = 0; i < transform.childCount; i++)
        {
            float childY = transform.GetChild(i).position.y;
            if (topY < childY)
            {
                topY = childY;
            }
            if (bottomY > childY)
            {
                bottomY = childY;
            }
        }

        if (topY > ScreenBounds.Top)
        {
            verticalDirection = -verticalDirection;
        }
        if (bottomY < ScreenBounds.Bottom)
        {
            verticalDirection = -verticalDirection;
        }

        // //Destroys enemy when it goes outside of screen
        // if (transform.position.x > 20f || transform.position.x < -30f)
        // {
        //     Destroy(gameObject);
        // }
    }
 
}
