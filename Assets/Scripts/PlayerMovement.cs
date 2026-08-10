
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float shootCooldown;
    public float nextShootTime;
    public float speed;
    public float topBound;
    public float bottomBound;
    public float leftBound;
    public float rightBound;
    public Bullet bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(leftBound, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float v = 0f;
        float h = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;
        if (Input.GetKey(KeyCode.A)) h = -1f;

        Vector2 movement = new Vector2(h, v);
        transform.Translate(movement *speed * Time.deltaTime);
       

        float posY = transform.position.y;
        float posX = transform.position.x;
        float clampY = Mathf.Clamp(transform.position.y, bottomBound, topBound);
        float clampX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
    
        //Final Clamp
        transform.position = new Vector3(clampX, clampY, 0);

        //Registers SPACE to shoot
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

    

