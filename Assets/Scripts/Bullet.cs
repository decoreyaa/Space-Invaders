using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // move the bullet to the right in this horizontal-oriented game
        transform.position += Vector3.right * speed * Time.deltaTime;

        // destroy the bullet if it flies off the right edge of the screen
        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
     
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // what should happen when the bullet hits something?
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            Destroy(gameObject);
        }
    }
}
