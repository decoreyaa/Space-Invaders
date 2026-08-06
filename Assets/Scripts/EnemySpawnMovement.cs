using UnityEngine;


public class EnemySpawnMovement : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float verticalSpeed = 0.5f;
    private float verticalDirection = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * horizontalSpeed * Time.deltaTime;

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
    }
}
