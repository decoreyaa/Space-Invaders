using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float verticalSpeed = 0.5f;
    public float verticalDirection = 1f;
    public float horizontalDirection = 1f;
    public EnemyBullet bulletPrefab;
    public float nextShootTime;
    public float shootCooldown;
    public float leftWall = -6f;
    private bool hasEnteredScreen = false;
    public GameObject movement;

    // Bullet.cs owns the kill so one bullet can only ever destroy one enemy

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vertDir = Vector2.up * verticalDirection * verticalSpeed * Time.deltaTime;
        Vector2 horiDir = Vector2.left * horizontalDirection * horizontalSpeed * Time.deltaTime;
        transform.Translate(vertDir);
        transform.Translate(horiDir);
        // checked after moving so the enemy never renders a frame past the wall
        BoundsCheck();

        //Shooting
         if (Time.time >= nextShootTime)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        nextShootTime = Time.time + shootCooldown;
    }
    void BoundsCheck()
    {
        Vector3 pos = transform.position;

        // spawned off the right edge, so the right clamp only arms once it has drifted on screen
        if (!hasEnteredScreen && pos.x <= ScreenBounds.Right)
        {
            hasEnteredScreen = true;
        }

        if (hasEnteredScreen)
        {
            float clampX = Mathf.Clamp(pos.x, leftWall, ScreenBounds.Right);
            if (pos.x != clampX)
            {
                horizontalDirection = -horizontalDirection;
                pos.x = clampX;
            }
        }

        float clampY = Mathf.Clamp(pos.y, ScreenBounds.Bottom, ScreenBounds.Top);
        if (pos.y != clampY)
        {
            verticalDirection = -verticalDirection;
            pos.y = clampY;
        }

        transform.position = pos;
    }
}