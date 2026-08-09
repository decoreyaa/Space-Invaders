using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBullet bulletPrefab;
    public float nextShootTime;
    public float shootCooldown;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // what should happen when the enemy collide with a "Bullet" tagged object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Hit");
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
