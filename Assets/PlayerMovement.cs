
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float shootCooldown = 0.5f;
    public float nextShootTime = 0f;
    public float speed = 5f;
    public float topBound = 4f;
    public float bottomBound = -4f;
    public Bullet bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;

        Vector2 movement = Vector2.up * v * speed * Time.deltaTime;
        transform.Translate(movement);

        float posY = transform.position.y;
        float clamp = Mathf.Clamp(transform.position.y, -4f, 4f);

        transform.position = new Vector3(transform.position.x, clamp, 0);

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Shoot();

            nextShootTime = Time.time + shootCooldown;

        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }


    }

    

